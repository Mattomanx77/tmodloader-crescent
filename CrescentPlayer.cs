using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace Crescent
{
    public class CrescentPlayer : ModPlayer
	{
		private const int saveVersion = 0;
        public int Lexp = 0;
        public int Llvl = 0;
        public int Llxp = 0;
        public int Lstt = 0;
        public float Lstr = 0;
        public float Ldex = 0;
        public float Lagi = 0;
        public float Lfor = 0;
        public float Lint = 0;
        public float Lvit = 0;
        public float Lrad = 0;
        public float Pos = 1.0f;
		public float Pos2 = 1.0f;
		public float Use = 200f;
		public int WingMuscle = 0;

		public override TagCompound Save()
		{
			return new TagCompound {
				{"Lexp", Lexp},
				{"Llvl", Llvl},
				{"Lstt", Lstt},
				{"Lstr", Lstr},
				{"Ldex", Ldex},
				{"Lagi", Lagi},
				{"Lfor", Lfor},
				{"Lint", Lint},
				{"Lvit", Lvit},
				{"Lrad", Lrad},
				{"Perk1", WingMuscle}
			};
		}

		public override void Load(TagCompound tag)
		{
			Lexp = tag.GetInt("Lexp");
			Llvl = tag.GetInt("Llvl");
			Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.2));
			Lstt = tag.GetInt("Lstt");
			Lstr = tag.GetFloat("Lstr");
			Ldex = tag.GetFloat("Ldex");
			Lagi = tag.GetFloat("Lagi");
			Lfor = tag.GetFloat("Lfor");	
			Lint = tag.GetFloat("Lint");
			Lvit = tag.GetFloat("Lvit");
			Lrad = tag.GetFloat("Lrad");
			WingMuscle = tag.GetInt("Perk1");
		}

		public override void Initialize()
		{
			Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.2));
		}

		public override void PreUpdateBuffs()
		{
			player.statLifeMax2 = (int)(player.statLifeMax * (Pos + Lvit / Use));
			player.statManaMax2 = (int)(player.statManaMax * (Pos + Lint / Use));
			player.meleeDamage = player.meleeDamage * (Pos + Lstr / Use);
			player.thrownDamage = player.thrownDamage * (Pos + Ldex / Use);
			player.rangedDamage = player.rangedDamage * (Pos + Ldex / Use);
			player.magicDamage = player.magicDamage * (Pos + Lint / Use);
			player.minionDamage = player.minionDamage * (Pos + Lrad / Use);
		}

		public override void PostUpdateEquips()
		{
			player.statDefense = (int)(player.statDefense * (Pos + Lfor / Use));
		}

		public override void PostUpdateRunSpeeds()
		{
			player.runAcceleration = player.runAcceleration * (Pos2 + Lagi / Use*2);
			player.maxRunSpeed = player.maxRunSpeed * (Pos2 + Lagi / Use*2);
			player.wingTimeMax = (int)(player.wingTimeMax * (1+WingMuscle/5F));
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
				Lstt = Llvl * 7 - (int)(Lstr + Lagi + Ldex + Lfor + Lint + Lvit + Lrad);
				Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.2));
				Main.PlaySound(2, -1, -1, 4);
			}
		}
	}
}
