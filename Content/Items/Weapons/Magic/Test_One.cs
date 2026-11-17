using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Magic
{

    public class Test_One : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.SetWeaponValues(5000, 1000, 100); //设置伤害、击退、暴击率
            Item.DefaultToMagicWeapon(ProjectileID.VenomFang,15,0f,true);
            Item.UseSound = SoundID.Item43;
            Item.mana = 5;
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            // 只留一行：buyPrice 是买入价，sellPrice 是卖出价，写两行后者会覆盖前者
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Pink;


        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo Source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 PlrToMoUse = Main.MouseWorld - player.Center;
            float r = (float)Math.Atan2(PlrToMoUse.Y, PlrToMoUse.X);
            
            float Foi = 8;
            for (int i = 0; i <= Foi; i += 1)// 把圆8等分，然后设置每个弹幕的量向
            {
                float nf = MathHelper.TwoPi / Foi * i + r;// 把圆8等分，然后设置弹幕的角度
                float v = nf + r;
                Vector2 n = new Vector2((float)Math.Cos(nf), (float)Math.Sin(nf)) * 15f;
                Projectile.NewProjectile(Source, position, n, type, damage, 1000f ,player.whoAmI);
            }
            return false;
            
        }

        //public override void AddRecipes()
        //{
        //    Recipe recipe = CreateRecipe();
        //    recipe.AddIngredient(ItemID.DirtBlock, 10);
        //    recipe.AddTile(TileID.WorkBenches);
        //    recipe.Register();
        //}
    }
}
