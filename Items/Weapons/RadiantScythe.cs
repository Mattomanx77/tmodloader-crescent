using Microsoft.Xna.Framework;
using Crescent.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crescent.Items.Weapons
{
	public class RadiantScythe : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radiant Scythe");
			Tooltip.SetDefault("");	//The (English) text shown below your weapon's name
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.crit = 7;
			item.summon = true;
			item.width = 32;
			item.height = 32;
			item.scale = 0;
			item.shoot = mod.ProjectileType<RadiantScytheProjectile>();
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;			//The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 4;			//The force of knockback of the weapon. Maxium is 20
			item.value = 10000;			//The value of the weapon
			item.rare = 0;				//The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item1;		//The sound when the weapon is using
			item.autoReuse = true;			//Whether the weapon can use automaticly by pressing mousebutton
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");
			if(ThoriumMod != null)
			{
				recipe.AddIngredient(ThoriumMod, "ThoriumBar", 20);
				recipe.AddIngredient(ThoriumMod, "LifeQuartz", 10);
				recipe.AddTile(ThoriumMod, "ThoriumAnvil");
			}
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
