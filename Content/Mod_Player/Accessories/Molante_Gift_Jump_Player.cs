using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Mod_Player.Accessories
{
    internal class Molante_Gift_Jump_Player : ModPlayer
    {

        public bool Have_Molante_Gift = false;//是否佩戴莫兰特礼物
        private const float Molante_velocity = 8f;//速度速度，
        private const float Max_Speed = 16f;//最大速度
        private float Wallet = 0f;//当前速度
        private float Last_Speed = 0f;//上一帧的速度

        public override void ResetEffects()
        {
            Have_Molante_Gift = false;
        }

        public override void OnExtraJumpStarted(ExtraJump jump, ref bool playSound)
        {
            if (!Have_Molante_Gift)
                return;

            int Dir = 0;//玩家方向
            if (Player.controlLeft && !Player.controlRight) Dir = -1;
            else if (Player.controlRight && !Player.controlLeft) Dir = 1;

            if (Dir != 0 && Wallet < Max_Speed)
            {
                Player.velocity.X += Dir * Molante_velocity;
                Wallet += Molante_velocity;
            }
            if (Player.velocity.X * Dir < 0) Player.velocity.X = -Player.velocity.X;

            //Main.NewText("叠加速度" + Wallet.ToString("0.0") + "/" + "上一帧速度" + Last_Speed.ToString("0.0"));

            //这是一个不完美的特效后面会换
            for (int i = 0; i < 45; i++)
            {
                if (Main.rand.NextBool(2))
                {
                    Dust.NewDustPerfect(
                        Player.Center,
                        DustID.UnholyWater,
                        new Vector2(
                        Player.velocity.X * -0.45f * Main.rand.NextFloat(1f, 3f),
                        Player.velocity.Y * -0.45f * Main.rand.NextFloat(1f, 3f)),
                    0,
                    default,
                    3f);
                }
            }
        }

        public override void PostUpdate()
        {
            if (!Have_Molante_Gift)
                return;
            float Speed = Math.Abs(Player.velocity.X);
            if (Last_Speed > float.Epsilon)
            {
                Wallet *= Math.Min(1f, Speed/Last_Speed);
            }
            else
            {
                Wallet = 0f;
            } 
            Last_Speed = Speed;
        }
    }
}
//TODO: 搁置中...特效未完善 