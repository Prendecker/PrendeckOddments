using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles
{
    public class Shadow_Gute : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            Projectile.width = 40;
            Projectile.height = 40;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.tileCollide = false;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 240;
            Projectile.usesIDStaticNPCImmunity = true;//局部无敌帧
            Projectile.idStaticNPCHitCooldown = 5;//局部无敌帧冷却
            Projectile.scale = 0.9f;
        }

        public override void OnSpawn(IEntitySource source)
        {
            bool Note_Change = Main.rand.NextBool();
            if (Note_Change == true)
            {
                Projectile.frame = 1;
            }
            else
            {
                Projectile.frame = 0;
            }
        }

        public override void AI()
        {
            Projectile.ai[0] += 1;
            Lighting.AddLight(Projectile.Center, 94f / 255f, 18f / 255f, 149f / 255f);
            float Time = Projectile.ai[0];//计时器
            float W_A = 3f;//摆动幅度
            float W_S = 0.175f;//摆动速度

            //波浪轨迹的计算，用cos
            float W_V_X = Projectile.velocity.X;
            float W_V_Y = Projectile.velocity.Y + (float)(Math.Cos(Time * W_S) * W_S * W_A);

            //旋转角度和朝向
            Projectile.spriteDirection = (Projectile.velocity.X > 0).ToDirectionInt();//确认帧图方向
            Vector2 W_V = new Vector2(W_V_X, W_V_Y) * 25;//新建角度向量
            Projectile.rotation = W_V.ToRotation() + (Projectile.spriteDirection == 1f ? 0f : MathHelper.Pi);//旋转角度，但是用了不同的逻辑（话说这特么啥）

            //波浪运动
            Vector2 P = Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(MathHelper.PiOver2);
            float W_O = (float)(Math.Sin(Time * W_S) * W_A);
            Projectile.position += P * W_O;
        }
    }
}
