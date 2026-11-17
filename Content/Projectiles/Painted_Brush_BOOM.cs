using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles
{
    public class Painted_Brush_BOOM : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 10;//确认该弹幕的帧图
        }

        public override void SetDefaults()
        {
            Projectile.width = 44;
            Projectile.height = 72;
            Projectile.penetrate = -1;//穿透数量
            Projectile.timeLeft = 60;//弹幕存在时间
            Projectile.DamageType = DamageClass.Summon;
            Projectile.friendly = true;//是否是友方弹幕
            Projectile.ignoreWater = false;//是否忽略水
            Projectile.tileCollide = false;//是否撞墙
            Projectile.usesLocalNPCImmunity = true;//独立无敌帧是否启用
            Projectile.localNPCHitCooldown = -1;//独立无敌帧冷却
        }

        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            for (int i = 0; i <= 35; i++)
            {
                Dust dust = Dust.NewDustPerfect(
                    Projectile.Center, 
                    DustID.SparkForLightDisc,
                    new Vector2(Main.rand.NextFloat(3f, -3f), Main.rand.NextFloat(3f, -3f)), 
                    0,
                    new Color(Main.rand.NextFloat(0f, 1f), Main.rand.NextFloat(0f, 1f), Main.rand.NextFloat(0f, 1f)),
                    Main.rand.NextFloat(1.2f, 2.5f));
                dust.noGravity = true;

            }
        }

        public override void AI()
        {
            Projectile.frameCounter++;
            if(Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
            }

            if (Projectile.frame >= 5)
            {
                Projectile.Kill();
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
        }
    }
}
