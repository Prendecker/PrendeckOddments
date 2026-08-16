using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles.Ammo_Proj
{
    public class Rose_Bullet_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.timeLeft = 1200;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath13, Projectile.position);
        }

        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2 ;
            Projectile.velocity.Y += 0.026f;
            Lighting.AddLight(Projectile.Center, new Vector3(1.39f * 0.2f, 0.11f * 0.2f, 0.29f * 0.2f));
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Crimson, Projectile.velocity.X * -0.2f, Projectile.velocity.Y * -0.2f);
            }
            SoundEngine.PlaySound(SoundID.NPCDeath1, Projectile.position);

        }

    }
}