using System;
using Terraria;
using Terraria.ID;
using Terraria.GameInput;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Crescent.UI;

namespace Crescent
{
    public class CrescentPlayer : ModPlayer
	{
		private const int saveVersion = 1;
        public int Lexp = 0;
        public int Llvl = 0;
        public int Llxp = 0;
        public int Lstt = 0;
        public float[] Lnum = new float[Crescent.NUMSTATS];
        public float Pos = 1.0f;
		public float Pos2 = 1.0f;
		public float Use = 1000f;
		public int[] Perk = new int[Crescent.NUMPERKS];
		public int[] Skill = new int[Crescent.NUMSKILLS];
		public int selectedSkill = 1;
		public int secondWindTimer = 0;

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
				{"Lnum7", Lnum[7]},
				{"Perk", Perk},
				{"Skill", Skill},
				{"SWTimer", secondWindTimer}
			};
		}

		public override void Initialize()
		{
			Lnum = new float[16];
			Perk = new int[32];
			Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.25));
		}

		public override void Load(TagCompound tag)
		{
			Lexp = tag.GetInt("Lexp");
			Llvl = tag.GetInt("Llvl");
			Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.25));
			Lstt = tag.GetInt("Lstt");
			Lnum[0] = tag.GetFloat("Lnum0");
			Lnum[1] = tag.GetFloat("Lnum1");
			Lnum[2] = tag.GetFloat("Lnum2");
			Lnum[3] = tag.GetFloat("Lnum3");
			Lnum[4] = tag.GetFloat("Lnum4");
			Lnum[5] = tag.GetFloat("Lnum5");
			Lnum[6] = tag.GetFloat("Lnum6");
			Lnum[7] = tag.GetFloat("Lnum7");
			Perk = tag.GetIntArray("Perk");
			Skill = tag.GetIntArray("Skill");
			secondWindTimer = tag.GetInt("SWTimer");
		}

		public override void PreUpdate()
		{
			if (secondWindTimer > 0) secondWindTimer--;
		}

		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (Crescent.keyConfig.JustPressed)
			{
				if (Crescent.mod.SettingsUI.HasChild(Crescent.mod.SettingsUI.Settings)) Crescent.mod.SettingsUI.SettingsToggle(false);
				else Crescent.mod.SettingsUI.SettingsToggle(true);
			}

			if (Crescent.keySkill.JustPressed)
			{
				switch (selectedSkill)
				{
					case 1:
						if (player.statMana > 10)
						{
							Projectile.NewProjectile(player.Center, (Main.MouseWorld - player.Center).SafeNormalize(Vector2.UnitX) * 5f, 504, (int)(5 * player.magicDamage), 10, player.whoAmI);
							player.statMana -= 10;
							player.manaRegenDelay = 50;
						}
						break;
					default:
						break;
				}
			}
		}

		public override void PreUpdateBuffs()
		{
			player.statLifeMax2 = (int)(player.statLifeMax * (Pos + Lnum[6] / Use));
			player.statManaMax2 = (int)(player.statManaMax * (Pos + Lnum[5] / Use));
			player.meleeDamage += Lnum[0] / Use;
			player.meleeCrit = LuckFunction(player.meleeCrit);
			player.thrownDamage += Lnum[0] / Use;
			player.thrownCrit = LuckFunction(player.thrownCrit);
			player.thrownVelocity += Lnum[0] / Use;
			player.rangedDamage += Lnum[3] / Use;
			player.rangedCrit = LuckFunction(player.rangedCrit);
			player.magicDamage += Lnum[5] / Use;
			player.magicCrit = LuckFunction(player.magicCrit);
			player.minionDamage += Lnum[7] / Use;
			player.maxMinions = player.maxMinions + Perk[1];
			if (Crescent.mod.thoriumLoaded)
			{
				ThoriumDamage();
			}
			if (Crescent.mod.tremorLoaded)
			{
				TremorDamage();
			}
		}

		private int LuckFunction(int num)
		{
			return (int)((Lnum[2] * (100f - num)) / (Lnum[2] + 500f)) + num;
		}

		private void ThoriumDamage()
		{
			player.GetModPlayer<ThoriumMod.ThoriumPlayer>().symphonicDamage += Lnum[3] / Use;
			player.GetModPlayer<ThoriumMod.ThoriumPlayer>().symphonicCrit = LuckFunction(player.GetModPlayer<ThoriumMod.ThoriumPlayer>().symphonicCrit);
			player.GetModPlayer<ThoriumMod.ThoriumPlayer>().bardResourceMax = (int)(player.GetModPlayer<ThoriumMod.ThoriumPlayer>().bardResourceMax * (Pos + Lnum[3] / Use));
			player.GetModPlayer<ThoriumMod.ThoriumPlayer>().radiantBoost += Lnum[7] / Use;
			player.GetModPlayer<ThoriumMod.ThoriumPlayer>().radiantCrit = LuckFunction(player.GetModPlayer<ThoriumMod.ThoriumPlayer>().radiantCrit);
		}

		private void TremorDamage()
		{
			player.GetModPlayer<Tremor.MPlayer>().alchemicalDamage += Lnum[5] / Use;
			player.GetModPlayer<Tremor.MPlayer>().alchemicalCrit = LuckFunction(player.GetModPlayer<Tremor.MPlayer>().alchemicalCrit);
		}

		public override void PostUpdateEquips()
		{
			player.statDefense += (int)(Lnum[4] / (Use / 100));
			player.statLifeMax2 += (Perk[5] * player.statDefense);
			Player.jumpHeight += Perk[2]*4;
			//Player.jumpSpeed *= 1 + Perk[2]*0.01f;
		}

		public override void PostUpdateRunSpeeds()
		{
			player.runAcceleration = player.runAcceleration * (Pos2 + Lnum[1] / Use*2);
			player.maxRunSpeed = player.maxRunSpeed * (Pos2 + Lnum[1] / Use*2);
			player.wingTimeMax = (int)(player.wingTimeMax * (1+Perk[0]/ 5F));
		}

		public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
		{
			player.statMana += Perk[3];

			if (target.lifeMax > 5) Lexp += (int)(damage * (1 + Lnum[2] / Use));
			if (target.boss && target.life < 0) { Lexp += Llxp/10; }
			CheckLifeforce(Lexp);
		}

		public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			player.statMana += Perk[3];

			if (target.lifeMax > 5) Lexp += (int)(damage * (1 + Lnum[2] / Use));
			if (target.boss && target.life < 0) { Lexp += Llxp/10; }
			CheckLifeforce(Lexp);
		}

		public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if (secondWindTimer <= 0 && Perk[4] > 0)
			{
				secondWindTimer = 86400;
				player.statLife = player.statLifeMax2/10;
				return false;
			}
			else
			{
				secondWindTimer = 0;
				return true;
			}
		}

		private void CheckLifeforce(int lexp)
		{
			if (Lexp >= Llxp)
			{
				Llvl++;
				Lexp -= Llxp;
				Lstt += 10;
				Llxp = (int)(Math.Pow((Llvl + 1) * 333, 1.25));
				Main.PlaySound(2, -1, -1, 4);
			}
		}
	}
}
