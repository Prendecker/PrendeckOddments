using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrendeckOddments.Content.Projectiles;
using PrendeckOddments.Content.Projectiles.Held_Item_Proj;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Ranged
{
    public class Shadow_Guny : ModItem
    {
        public const int Hold_D = 15;

        public float R_Dam_Che = 0.75f;//右键伤害倍率

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //遍历文本，然后把“[0]”替换
            foreach (var item in tooltips)
            {
                if (item.Text.Contains("[0]"))
                {
                    item.Text = item.Text.Replace("[0]", (R_Dam_Che * 100).ToString());
                }
            }
        }//去你妈的我甚至还得专门写一套文本同步

        private static Asset<Texture2D> L_Texture;

        public override void Load()
        {
            L_Texture = ModContent.Request<Texture2D>(Texture + "_Glow");
        }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;//是否能让你的武器右键连射
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.SetWeaponValues(28, 2.5f, 2);
            Item.DefaultToRangedWeapon(1, AmmoID.Bullet, 30, 12f, true);
            Item.shoot = 1;//发射弹幕，由于是枪械类武器所以随便填什么就行了 
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(0, 0, 12, 25);
            Item.rare = ItemRarityID.Red;
            Item.channel = true;
            Item.noUseGraphic = true;
        }

        //散射效果（暂时）
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10f));
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-25, 3);//物品贴图手持偏移
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            //检测是否右键，若果是就发射傻豆盖，如果不是就发射常规子弹
            type = ModContent.ProjectileType<Shadow_Guny_Htelm>();
            Vector2 Hold_Vel = velocity.SafeNormalize(velocity) * Hold_D;

            if (player.altFunctionUse == 2)
            {
                Projectile.NewProjectile(
                    source,
                    position,
                    Hold_Vel,
                    type,
                    (int)(damage * R_Dam_Che),
                    knockback,
                    Main.myPlayer,1);//右键手持弹幕

                return false;
            }
            else
            {
                Projectile.NewProjectile(source, position, Hold_Vel, type, damage, knockback, Main.myPlayer,0);//左键手持弹幕

                return false;
            }

            //if(player.altFunctionUse == 2 )
            //{
            //    Vector2 Hold_Vel = velocity.SafeNormalize(velocity) * Hold_D;

            //    type = ModContent.ProjectileType<Shadow_Guny_Htelm>();

            //    for (int i = 0; i < 3; i++)
            //    {
            //        if (Main.rand.NextBool(3))
            //        {
            //            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<Shadow_Gute>(), (int)(damage * R_Dam_Che), knockback, player.whoAmI);
            //        }
            //    }

            //    Projectile.NewProjectile(source, position, Hold_Vel, type, (int)(damage * R_Dam_Che), knockback, Main.myPlayer);
            //    //使用傻豆盖的语音
            //    SoundEngine.PlaySound
            //    (new SoundStyle
            //    ($"PrendeckOddments/Assets/Sounds/Items/Shadowguy_say")
            //    { Volume = 1f, PitchVariance = 0.5f, MaxInstances = 1 },
            //    position);
            //    return false;
            //}
            //else
            //{
            //    
            //    return true;
            //}

        }

        //绘制发光蒙版
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Texture2D texture = L_Texture.Value;//（暂时不知道这个有什么用）
            //绘制发光蒙版，但具体是干什么的我不太懂 
            spriteBatch.Draw
            (
                texture,
                new Vector2
                (
                    Item.position.X - Main.screenPosition.X + Item.width * 0.5f,
                    Item.position.Y - Main.screenPosition.Y + Item.height - texture.Height * 0.5f
                ),
                new Rectangle(0, 0, texture.Width, texture.Height),
                Color.White,
                rotation,
                texture.Size() * 0.5f,
                scale,
                SpriteEffects.None,
                0f
            );
        }

        //是否开启右键使用
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
    }
}
//TODO: 搁置中 机制未完成 