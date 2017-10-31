using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Crescent.Dusts
{
	public class RadiantParticle : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			
			dust.alpha = 0;
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 10, 10);
			dust.scale = 1f;
		}

		public override bool Update(Dust dust)
		{
			dust.position += dust.velocity;
			dust.rotation += MathHelper.ToRadians(Main.rand.Next(-36, 37));
			dust.alpha = (int)(dust.alpha + 255F*0.1/12.5F);
			//float light = 0.5F-(0.5F*dust.alpha/255F);
			//Lighting.AddLight(dust.position, light, light, light);
			if (dust.alpha >= 255f)
			{
				dust.active = false;
			}
			return false;
		}
	}
}