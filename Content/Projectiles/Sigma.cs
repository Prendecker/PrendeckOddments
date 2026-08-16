using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles
{

	public class Sigma : ModProjectile
	{
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 28;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 60;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2;
            Lighting.AddLight(Projectile.Center, new Vector3(12.7f * 0.033f, 3.3f * 0.033f, 19.1f * 0.033f));//设置灯光

            if (Projectile.timeLeft <= 45)
            {
                Projectile.velocity *= 0.97f;
            }

        }

        public override bool PreKill(int timeLeft)
        {
            return base.PreKill(timeLeft);
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.UndergroundHallowedEnemies, Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f);
            }            
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Projectile.damage = (int)(Projectile.damage * 0.5f);
            SoundEngine.PlaySound(new SoundStyle($"PrendeckOddments/Assets/Sounds/Projectiles/short_punch") { Volume = 1f, PitchVariance = 0.4f, MaxInstances = 1 }, Projectile.position);
            for (int i = 0; i < 20; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.UndergroundHallowedEnemies, Projectile.velocity.X * 0.25f, Projectile.velocity.Y * 0.25f);
            }
        }
    }
}
