using Microsoft.Xna.Framework;
using PrendeckOddments.Content.Projectiles;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Melee
{
    public class Toy_Knife : ModItem
    {
        public override void SetStaticDefaults() 
        {
            Item.ResearchUnlockCount = 1;
        }
        private bool MANA_Use = false;//定义魔力是否消耗
        public static readonly int MANA = 30;//魔力消耗
        public float MANA_Regen = MANA / 5;//魔力回复
        private int MANA_Counting = 0;//物品使用次数
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MANA_Regen);//把“MANA_Regen”注入到文本提示那里

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;//获取玩家魔力消耗减免
            int R_MANA = (int)(MANA * player.manaCost);//计算实际消耗

            //遍历文本，然后把“[0]”替换
            foreach (var item in tooltips)
            {
                if (item.Text.Contains("[0]"))
                {
                    item.Text = item.Text.Replace("[0]", R_MANA.ToString());
                }
            }
        }//去你妈的我甚至还得专门写一套文本同步，“ITEM.MANA”FUCK YOU
        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.SetWeaponValues(25, 5, 11); //设置伤害，击退，暴击率
            Item.DamageType = DamageClass.Melee; //设置伤害类型
            Item.width = 58;
            Item.height = 62;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.shootsEveryUse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = Item.sellPrice(0, 0, 12, 5);
            Item.shoot = 1;//设置射弹，保证执行“Shoot”就行
            Item.shootSpeed = 8f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)//当玩家挥舞时调用
        {
            if (Main.rand.NextBool(2))//挥舞时每帧由1/2的概率生成粒子
            {
                Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Pink);
                Dust.NewDustDirect(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, DustID.Enchanted_Gold);
            }
        }

        public override bool CanUseItem(Player player)
        {
            
            if (MANA_Counting <= 3)//如果使用次数小于3则加1
            {
                MANA_Use = false;
                MANA_Counting += 1;
                if (player.statMana>= MANA)
                {
                    player.manaRegenDelay = (float)(0.5 * ((1 - player.statMana / player.statManaMax) * 160 + 45));
                }
            }
            else//否则就发射弹幕并重置计数
            {
                if (player.CheckMana(MANA, false, false))//检查魔力是否足够
                {
                    MANA_Use = true;
                    MANA_Counting = 0;
                    SoundEngine.PlaySound(SoundID.Item82);
                    player.CheckMana(MANA, true, false);
                    player.manaRegenDelay = (float)(0.5 * ((1 - player.statMana / player.statManaMax) * 160 + 45));
                }
            }

            return true;
        }

        public override Vector2? HoldoutOffset()//手持贴图偏移
        {
            return new Vector2(2, 2);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 R_Pos = new Vector2(position.X, position.Y - 20f);//发射位置偏移，不然会很鬼畜
            if (MANA_Use == true)
            {
                Projectile.NewProjectile(source, R_Pos, velocity, ModContent.ProjectileType<Toy_Star>(), (int)(damage * 2.5), knockback, player.whoAmI);//发射弹幕并让伤害X2.5
            }
            return false;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            player.statMana += (int)MANA_Regen;//击中敌人时回复“MANA_Regen”魔力
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddIngredient(ItemID.GoldBar, 15);
            recipe.AddTile(TileID.Anvils);// 铁砧
            recipe.Register();

            //再加一个，因为有的世界不是黄金
            recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FallenStar, 10);
            recipe.AddIngredient(ItemID.Ruby, 3);
            recipe.AddIngredient(ItemID.PlatinumBar, 15);
            recipe.AddTile(TileID.Anvils);// 铁砧
            recipe.Register();
        }
    }
}