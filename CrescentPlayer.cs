using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Crescent
{
    public class CrescentPlayer : ModPlayer
	{
		private const int saveVersion = 1;
        public int Lexp = 0;
        public int Llvl = 0;
        public int Llxp = 0;
        public int Lstt = 0;
        public float[] Lnum = new float[7];
        public float Pos = 1.0f;
		public float Pos2 = 1.0f;
		public float Use = 200f;
		public int[] Perk = new int[32];

		public override TagCompound Save()
		{
			return new TagCompound {
				{"Lexp", Lexp},
				{"Llvl", Llvl},
				{"Lstt", Lstt},
				{"Lnum0", Lnum[0]},
				{"Lnum1", Lnum[1]},
				{"Lnum2", Lnum[2]},
				{"Lnum3", Lnum[3]},
				{"Lnum4", Lnum[4]},
				{"Lnum5", Lnum[5]},
				{"Lnum6", Lnum[6]},
				{"Perk", Perk}
			};
		}

		public override void Initialize()
		{
			Lnum = new float[16];
			Perk = new int[32];
			Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.2));
		}

		public override void Load(TagCompound tag)
		{
			Lexp = tag.GetInt("Lexp");
			Llvl = tag.GetInt("Llvl");
			Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.2));
			Lstt = tag.GetInt("Lstt");
			Lnum[0] = tag.GetFloat("Lnum0");
			Lnum[1] = tag.GetFloat("Lnum1");
			Lnum[2] = tag.GetFloat("Lnum2");
			Lnum[3] = tag.GetFloat("Lnum3");
			Lnum[4] = tag.GetFloat("Lnum4");
			Lnum[5] = tag.GetFloat("Lnum5");
			Lnum[6] = tag.GetFloat("Lnum6");
			Perk = tag.GetIntArray("Perk");
		}

		public override void PreUpdateBuffs()
		{
			player.statLifeMax2 = (int)(player.statLifeMax * (Pos + Lnum[5] / Use));
			player.statManaMax2 = (int)(player.statManaMax * (Pos + Lnum[4] / Use));
			player.meleeDamage = player.meleeDamage * (Pos + Lnum[0] / Use);
			player.thrownDamage = player.thrownDamage * (Pos + Lnum[1] / Use);
			player.rangedDamage = player.rangedDamage * (Pos + Lnum[1] / Use);
			player.magicDamage = player.magicDamage * (Pos + Lnum[4] / Use);
			player.minionDamage = player.minionDamage * (Pos + Lnum[6] / Use);
			player.maxMinions = player.maxMinions + Perk[2];
		}

		public override void PostUpdateEquips()
		{
			player.statDefense = (int)(player.statDefense * (Pos + Lnum[3] / Use));
		}

		public override void PostUpdateRunSpeeds()
		{
			player.runAcceleration = player.runAcceleration * (Pos2 + Lnum[2] / Use*2);
			player.maxRunSpeed = player.maxRunSpeed * (Pos2 + Lnum[2] / Use*2);
			player.wingTimeMax = (int)(player.wingTimeMax * (1+Perk[1]/ 5F));
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			Lexp += damage;
			CheckLifeforce(Lexp);
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			Lexp += damage;
			CheckLifeforce(Lexp);
		}

		private void CheckLifeforce(int lexp)
		{
			if (Lexp >= Llxp)
			{
				Llvl++;
				Lexp -= Llxp;
				Lstt += 7;
				Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.2));
				Main.PlaySound(2, -1, -1, 4);
			}
		}
	}
}
