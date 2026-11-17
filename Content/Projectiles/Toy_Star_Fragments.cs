using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles
{
    public class Toy_Star_Fragments : ModProjectile
    {
        private NPC HomingTarget//定义追踪目标
        {
            get => Projectile.ai[1] == 0 ? null : Main.npc[(int)Projectile.ai[1] - 1];
            set
            {
                Projectile.ai[1] = value == null ? 0 : value.whoAmI + 1;
            }
        }

        int UpSpeed = 0;//加速次数

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.CultistIsResistantTo[Type] = true;//拜月教徒追踪弹幕抗性
        }

        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.timeLeft = 1200;//弹幕存在时间
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;//弹幕穿透
            Projectile.friendly = true;//是否是友方弹幕
            Projectile.ignoreWater = false;//是否忽略水
            Projectile.tileCollide = true;//是否撞墙
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;//独立无敌帧是否启用
            Projectile.localNPCHitCooldown = 40;//独立无敌帧冷却
        }

        public override void AI()
        {
            float MaximumTrackingRange = float.MaxValue;//追踪距离，默认为最大值

            Lighting.AddLight(Projectile.Center, 230f / 255f, 203f / 255f, 87f / 255f);//设置光源，使用“XXX / 255”来避免太亮的问题

            Projectile.ai[0] += 1;//计时器，嗯对

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.Pi / 2;

            if (HomingTarget == null)
            {
                HomingTarget = FindClosestNPC(MaximumTrackingRange);
            }//如果没有NPC，就去寻找一个

            if (HomingTarget != null && !IsValidTarget(HomingTarget))
            {
                HomingTarget = null;
            }//而且还要确保有效，否则就无效（如目标NPC死了，跑远了，黄名了）

            //如果计时器到了，就执行以下内容
            if (Projectile.ai[0] >= 90)
            {
                Projectile.penetrate = 1;//限制无限穿透

                //如果有怪就追踪
                if (HomingTarget != null)
                {
                    float distance = Vector2.Distance(Projectile.Center, HomingTarget.Center);//计算弹幕与目标的距离
                    float TurnAngle = MathHelper.Clamp(130f / distance, 5f, 45f);//旋转角度，Clamp限制最大和最小角度--这就不会出现转圈圈的现象了--

                    float length = Projectile.velocity.Length();//弹幕的向量的长度
                    float targetAngle = Projectile.AngleTo(HomingTarget.Center);//朝目标旋转
                    
                    //用来实现平滑追踪的（--其实这个我不太懂--）
                    Projectile.velocity = Projectile.velocity.ToRotation().AngleTowards(targetAngle, MathHelper.ToRadians(TurnAngle)).ToRotationVector2() * length;
                }

                //如果没怪就逐渐隐藏并删除自身
                if (HomingTarget == null)
                {
                    Projectile.alpha += 10;

                    if (Projectile.alpha >= 255)
                    {
                        Projectile.Kill();
                    }
                }
                else//否则就 藏 隐
                {
                    Projectile.alpha -= 10;
                }

            }

            //在“90”计时器到之前就执行以下内容
            if (Projectile.ai[0] >= 70)
            {
                //如果有怪且“UpSpeed”未到最大值就加速并加1
                //否则就减速并减1
                if (UpSpeed <= 45 && HomingTarget != null)
                {
                    Projectile.velocity *= 1.04f;
                    UpSpeed += 1;
                }
                else if (UpSpeed != 0 && HomingTarget == null)
                {
                    Projectile.velocity *= 0.96f;
                    UpSpeed -= 1;
                }
            }

        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item56, Projectile.Center);

            if (Math.Abs(Projectile.velocity.X - oldVelocity.X) > float.Epsilon)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }

            if (Math.Abs(Projectile.velocity.Y - oldVelocity.Y) > float.Epsilon)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }

            return false;
        }

        //在有效范围里寻找NPC并追击，否则就返回“null”
        public NPC FindClosestNPC(float maxDetectDistance)
        {
            NPC closestNPC = null;

            //使用平方值而非平方根以节省性能开销（--话说平方根是什么？--）
            float sqrMaxDetectDistance = maxDetectDistance * maxDetectDistance;

            //遍历所有NPC
            foreach (var target in Main.ActiveNPCs)
            {
                //检测这个NPC是否最为可选目标
                if (IsValidTarget(target))
                {
                    //使用“DistanceSquared”函数检测平方距离避免开根号（--话说平方根是什么？--）
                    float sqrDistanceToTarget = Vector2.DistanceSquared(target.Center, Projectile.Center);

                    //检测是否在有效范围里，嗯对
                    if (sqrDistanceToTarget < sqrMaxDetectDistance)
                    {
                        sqrMaxDetectDistance = sqrDistanceToTarget;
                        closestNPC = target;
                    }
                }
            }

            return closestNPC;
        }

        //就是用来检测NPC类型的，没了
        public bool IsValidTarget(NPC target)
        {
            return target.CanBeChasedBy() 
                && !target.friendly //不是黄名的
                && !target.dontTakeDamage//不是无敌的
                && Collision.CanHit(Projectile.Center, 1, 1, target.position, target.width, target.height);//视线检测
        }
    }
}