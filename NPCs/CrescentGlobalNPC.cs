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
				int n = rng.Next(Main.hardMode ? NPC.downedPlantBoss ? 4 : 1 : 0, Main.hardMode ? NPC.downedPlantBoss ? Main.dayTime ? 50 : 125 : Main.dayTime ? 25 : 62 : Main.dayTime ? 10 : 25);
				npc.GivenName = ("Lv. " + (1 + n) + " " + npc.TypeName);
				npc.lifeMax = (int)(npc.lifeMax * (1 + (n * 0.005)));
				npc.life = npc.lifeMax;
				npc.defense = (int)(npc.defense * (1 + (n * 0.001)));
				npc.damage = (int)(npc.damage * (1 + (n * 0.005)));
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