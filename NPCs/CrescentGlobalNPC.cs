using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crescent.NPCs
{
	public class CrescentGlobalNPC : GlobalNPC
	{
		public static Random rng = new Random();
		public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
		{
			if (Config.MonsterLeveling)
			{
				double n = Main.hardMode ? NPC.downedPlantBoss ? 5 : 2.5 : 1;
				n = rng.Next((int)Math.Round(n), (int)Math.Round(n*12.5));
				npc.GivenName = ("Lv. " + (1 + n) + " " + npc.TypeName);
				npc.lifeMax = (int)(npc.lifeMax * (1 + (n * 0.005)));
				npc.life = npc.lifeMax;
				npc.defense = (int)(npc.defense * (1 + (n * 0.001)));
				npc.damage = (int)(npc.damage * (1 + (n * 0.004)));
			}
		}

		public override bool PreNPCLoot(NPC npc)
		{
			if (npc.lastInteraction == npc.FindClosestPlayer())
			{
				npc.value *= 1 + Main.player[npc.lastInteraction].GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[2] / 1000;
			}

			return true;
		}
	}
}