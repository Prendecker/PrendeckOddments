using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles
{
    public class Toy_Star : ModProjectile
    {
        private bool HIT_NPC = true;//判断是否击中敌人

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = false;//拜月教徒追踪弹幕抗性
        }
        
        public override void SetDefaults()
        {
            Projectile.width = 82;
            Projectile.height = 82;
            Projectile.penetrate = -1;//穿透数量
            Projectile.timeLeft = 120;//弹幕存在时间
            Projectile.DamageType = DamageClass.Melee;
            Projectile.friendly = true;//是否是友方弹幕
            Projectile.ignoreWater = false;//是否忽略水
            Projectile.tileCollide = true;//是否撞墙
            Projectile.usesLocalNPCImmunity = true;//独立无敌帧是否启用
            Projectile.localNPCHitCooldown = 30;//独立无敌帧冷却
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + Main.rand.NextFloat(30f, -30f);
        }

        public override void AI()
        {
            Lighting.AddLight(Projectile.Center, 230f / 255f, 203f / 255f, 87f / 255f);//设置光源

            Projectile.rotation += Projectile.direction * 0.05f;//设置最低旋转角度
            Projectile.rotation += (float)Projectile.direction * 0.25f * ((float)Projectile.timeLeft / 480f);//设置旋转角度，会随射弹存在时间变慢
            if (Projectile.timeLeft <= 40f)
            {
                Projectile.velocity *= 0.95f;
            }
            else
            {
                Projectile.velocity *= 0.977f;
            }

        }


        //碰撞反弹的代码（算了等会再写注释） 
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item56, Projectile.Center);

            if(Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (HIT_NPC == true)
            {
                Projectile.velocity *= 0.70f;
            }

            HIT_NPC = false;

        }

        public override void OnKill(int timeLeft)
        {
            float r = Projectile.rotation;
            float Fori = 5;
            for (int i = 0; i < 5; i++)
            {
                float nf = MathHelper.TwoPi / Fori * i + r;// 把圆5等分，然后设置弹幕的角度

                Vector2 n = new Vector2((float)Math.Cos(nf), (float)Math.Sin(nf)) * 1.3f;
                Projectile.NewProjectile(Projectile.GetSource_Death(),
                    Projectile.Center,
                    n,
                    ModContent.ProjectileType<Toy_Star_Fragments>(),
                    (int)(Projectile.damage * 0.45f),
                    Projectile.knockBack * 0.25f,
                    Projectile.owner);
            }

            SoundEngine.PlaySound(SoundID.Item110, Projectile.Center);
            for (int i = 0; i < 15; i++)
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Pink);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold);

            }
        }

    }
}
