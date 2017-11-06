using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crescent.NPCs
{
	public class CrescentGlobalNPC : GlobalNPC
	{
		public override bool PreNPCLoot(NPC npc)
		{
			if (npc.lastInteraction == npc.FindClosestPlayer())
			{
				npc.value *= 1 + Main.player[npc.lastInteraction].GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[2] / 1000;
			}

			return true;
		}

		public override void NPCLoot(NPC npc)
		{
		}
	}
}