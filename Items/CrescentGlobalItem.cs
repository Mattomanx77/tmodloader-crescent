using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crescent.Items
{
	class CrescentGlobalItem : GlobalItem
	{
		//Thanks DarkLight.
		public override bool UseItem(Item item, Player player)
		{
			if (item.healLife > 0 && player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[6] > 0)
			{
				int heals = (int)(item.healLife * (player.GetModPlayer<CrescentPlayer>(Crescent.mod).Pos + player.GetModPlayer<CrescentPlayer>(Crescent.mod).Lnum[6] / player.GetModPlayer<CrescentPlayer>(Crescent.mod).Use) - 1);
				player.statLife += heals;
				if (Main.myPlayer == player.whoAmI) player.HealEffect(heals, true);
			}

			return base.UseItem(item, player);
		}
	}
}
