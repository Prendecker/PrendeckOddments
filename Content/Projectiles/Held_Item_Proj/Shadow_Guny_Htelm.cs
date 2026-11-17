using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Projectiles.Held_Item_Proj
{
    public class Shadow_Guny_Htelm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 10;
            ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.hide = true;
            Projectile.ignoreWater = false;
            Projectile.DamageType = DamageClass.Ranged;

            //用来微调位置的
            DrawOriginOffsetY = 2;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];//获取玩家数据（应该？）
            Vector2 P_C = player.RotatedRelativePoint(player.MountedCenter);

            //右键AI逻辑
            if (Projectile.owner == Main.myPlayer && Main.mouseRight && Projectile.ai[0] == 1)
            {
                Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    P_C,
                    Projectile.velocity,
                    ModContent.ProjectileType<Shadow_Gute>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner);

                //使用傻豆盖的语音
                SoundEngine.PlaySound
                (new SoundStyle
                ($"PrendeckOddments/Assets/Sounds/Items/Shadowguy_say")
                { Volume = 1f, PitchVariance = 0.5f, MaxInstances = 1 },
                player.Center);
            }
            else if (Projectile.owner == Main.myPlayer && !Main.mouseRight && Projectile.ai[0] == 1)
            {
                Projectile.Kill();
                return;
            }

            //左键AI逻辑
            if (Projectile.owner == Main.myPlayer && Main.mouseLeft && Projectile.ai[0] == 0)
            {
                int N_S = Main.rand.Next(1, 4);
                if (N_S == 1)
                {
                    SoundEngine.PlaySound(SoundID.Item140, player.position);
                }

                if (N_S == 2)
                {
                    SoundEngine.PlaySound(SoundID.Item141, player.position);
                }

                if (N_S == 3)
                {
                    SoundEngine.PlaySound(SoundID.Item142, player.position);
                }
            }
            else if (Projectile.owner == Main.myPlayer && !Main.mouseLeft && Projectile.ai[0] == 0)
            {
                Projectile.Kill();
                return;
            }

            Projectile.direction = Projectile.velocity.X < 0 ? -1 : 1;
            Projectile.spriteDirection = Projectile.direction;
            player.ChangeDir(Projectile.direction);
            player.heldProj = Projectile.whoAmI;
            player.SetDummyItemTime(2);
            Projectile.Center = Vector2.Lerp(player.Center, P_C, 0.25f);
            float Rotataon_OFFset = Projectile.spriteDirection == -1 ? MathHelper.Pi : 0;
            DrawOffsetX = Projectile.spriteDirection == 1 ? -32 : -25;//动态x轴绘制偏移，左边会有响朝右时的偏移 右边会影响朝左时的偏移 
            Projectile.rotation = Projectile.velocity.ToRotation() + Rotataon_OFFset;
            player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
            Projectile.timeLeft = 2;
        }

        public override bool? CanDamage() => false;
        
    }
}
//TODO: 搁置中 机制未完成 