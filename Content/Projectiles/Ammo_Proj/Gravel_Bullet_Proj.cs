using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles.Ammo_Proj
{
    internal class Gravel_Bullet_Proj : ModProjectile
    {
        public override string Texture => "PrendeckOddments/Content/Items/Ammo/Gravel_Bullet";

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.timeLeft = 1200;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.aiStyle = ProjAIStyleID.ThrownProjectile; 
            Projectile.alpha = 255;
        }

        public override void AI()
        {
            Projectile.alpha -= 33;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Tink, Projectile.position);
            for (int N = 0; N < 5; N++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Stone);
            }
            return true;
        }
    }
}
