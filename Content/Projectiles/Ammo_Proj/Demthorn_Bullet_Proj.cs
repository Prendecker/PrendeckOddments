using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles.Ammo_Proj
{
    internal class Demthorn_Bullet_Proj : ModProjectile
    {
        public override void SetStaticDefaults()
        {

        }

        public override void SetDefaults()
        {
            Projectile.width = 4;
            Projectile.height = 4;
            Projectile.ArmorPenetration = 13;//护甲穿透
            Projectile.penetrate = 5;//穿透数量
            Projectile.timeLeft = 900;//弹幕存在时间
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.friendly = true;//是否是友方弹幕
            Projectile.ignoreWater = false;//是否忽略水
            Projectile.tileCollide = true;//是否撞墙
            Projectile.extraUpdates = 3;//弹幕更新速度
            Projectile.usesLocalNPCImmunity = true;//独立无敌帧是否启用
            Projectile.localNPCHitCooldown = -1;//独立无敌帧冷却
            Projectile.alpha = 255;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, Projectile.velocity.X * 0.15f, Projectile.velocity.Y * 0.15f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 4; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Demonite, Projectile.velocity.X * -0.15f, Projectile.velocity.Y * -0.15f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            return true;
        }

        public override void AI()
        {
            Projectile.alpha -= 11;
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2;
            Lighting.AddLight(Projectile.Center, new Vector3(1.31f * 0.2f, 0.3f * 0.2f, 2.25f * 0.2f));
        }
    }
}
