using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crescent.Items.Weapons
{
	public class StrengthSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Strenght Sword");
			Tooltip.SetDefault("");	//The (English) text shown below your weapon's name
		}

		public override void SetDefaults()
		{
			item.damage = 20;			//The damage of your weapon
			item.melee = true;			//Is your weapon a melee weapon?
			item.width = 32;			//Weapon's texture's width
			item.height = 32;           //Weapon's texture's height
			item.scale = 1;
			item.useTime = 20;			//The time span of using the weapon. Remember in terraria, 60 frames is a second.
			item.useAnimation = 20;			//The time span of the using animation of the weapon, suggest set it the same as useTime.
			item.useStyle = 1;			//The use style of weapon, 1 for swinging, 2 for drinking, 3 act like shortsword, 4 for use like life crystal, 5 for use staffs or guns
			item.knockBack = 5;			//The force of knockback of the weapon. Maxium is 20
			item.value = 0;			//The value of the weapon
			item.rare = 13;				//The rarity of the weapon, from -1 to 13
			item.UseSound = SoundID.Item1;		//The sound when the weapon is using
			item.autoReuse = true;			//Whether the weapon can use automaticly by pressing mousebutton
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			Mod ThoriumMod = ModLoader.GetMod("ThoriumMod");
			if (ThoriumMod != null)
			{
				recipe.AddIngredient(ThoriumMod, "ThoriumBar", 20);
				recipe.AddIngredient(ThoriumMod, "LifeQuartz", 10);
				recipe.AddTile(ThoriumMod, "ThoriumAnvil");
			}
			recipe.SetResult(this);
			//recipe.AddRecipe();
		}

		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
		}

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
		}
	}
}
