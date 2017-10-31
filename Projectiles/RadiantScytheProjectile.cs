using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Crescent.Projectiles
{
	public class RadiantScytheProjectile : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Radiant Scythe");
		}

		public override void SetDefaults()
		{
			projectile.width = 96;
			projectile.height = 96;
			projectile.scale = 1.5f;
			drawOffsetX = 16 + 24;
			drawOriginOffsetY = 16 + 24;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.ownerHitCheck = true;
			projectile.minion = true;
			projectile.alpha = 255;
			projectile.timeLeft = 25;
		}


		public override void AI()
		{
			Player projOwner = Main.player[projectile.owner];
			projectile.direction = projOwner.direction;
			projectile.spriteDirection = projOwner.direction;
			projectile.position.X = projOwner.position.X + projOwner.width/2 - projectile.width/2; 
			projectile.position.Y = projOwner.position.Y + projOwner.height/2 - projectile.height/2;
			if(projectile.direction == 1)
			{
				projectile.rotation += MathHelper.ToRadians(18F);
				int dustIndex = Dust.NewDust(new Vector2(
					projectile.position.X + projectile.width / 2 - 5 + (float)Math.Sin(MathHelper.ToRadians(18F * projectile.timeLeft + 120)) * projectile.width / 2.5F,
					projectile.position.Y + projectile.height / 2 - 5 + (float)Math.Cos(MathHelper.ToRadians(18F * projectile.timeLeft + 120)) * projectile.height / 2.5F),
					0, 0, mod.DustType<Dusts.RadiantParticle>(), 0, 0, 127, default(Color), 1f);
				Main.dust[dustIndex].velocity = new Vector2(0, 0);
				dustIndex = Dust.NewDust(new Vector2(
					projectile.position.X + projectile.width / 2 - 5 + (float)Math.Sin(MathHelper.ToRadians(18F * projectile.timeLeft - 60)) * projectile.width / 2.5F,
					projectile.position.Y + projectile.height / 2 - 5 + (float)Math.Cos(MathHelper.ToRadians(18F * projectile.timeLeft - 60)) * projectile.height / 2.5F),
					0, 0, mod.DustType<Dusts.RadiantParticle>(), 0, 0, 127, default(Color), 1f);
				Main.dust[dustIndex].velocity = new Vector2(0, 0);
			}
			else
			{
				projectile.rotation -= MathHelper.ToRadians(18F);
				int dustIndex = Dust.NewDust(new Vector2(
					projectile.position.X + projectile.width / 2 - (float)Math.Sin(MathHelper.ToRadians(18F * projectile.timeLeft + 120)) * projectile.width / 2.5F,
					projectile.position.Y + projectile.height / 2 + (float)Math.Cos(MathHelper.ToRadians(18F * projectile.timeLeft + 120)) * projectile.height / 2.5F),
					0, 0, mod.DustType<Dusts.RadiantParticle>(), 0, 0, 127, default(Color), 1f);
				Main.dust[dustIndex].velocity = new Vector2(0, 0);
				dustIndex = Dust.NewDust(new Vector2(
					projectile.position.X + projectile.width / 2 - (float)Math.Sin(MathHelper.ToRadians(18F * projectile.timeLeft - 60)) * projectile.width / 2.5F,
					projectile.position.Y + projectile.height / 2 + (float)Math.Cos(MathHelper.ToRadians(18F * projectile.timeLeft - 60)) * projectile.height / 2.5F),
					0, 0, mod.DustType<Dusts.RadiantParticle>(), 0, 0, 127, default(Color), 1f);
				Main.dust[dustIndex].velocity = new Vector2(0, 0);
			}
			if (projectile.timeLeft > 20)
			{
				projectile.alpha = projectile.alpha - 256 / 5;
			}
			if (projectile.timeLeft < 5)
			{
				projectile.alpha = projectile.alpha + 256 / 5;
			}
		}
	}
}