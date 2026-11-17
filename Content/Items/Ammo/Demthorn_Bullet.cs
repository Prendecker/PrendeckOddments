using PrendeckOddments.Content.Projectiles.Ammo_Proj;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Ammo
{
    public class Demthorn_Bullet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 9999;
        }

        public override void SetDefaults()
        {
            Item.damage = 6;//设置伤害
            Item.DamageType = DamageClass.Ranged;
            Item.crit = 10;//设置暴击率
            Item.ammo = AmmoID.Bullet;//设置弹药类型
            Item.knockBack = 1f;//设置击退
            Item.rare = ItemRarityID.Blue;//设置稀有度
            Item.width = 12;
            Item.height = 20;//设置宽和高
            Item.shoot = ModContent.ProjectileType<Demthorn_Bullet_Proj>();//设置发射的弹幕
            Item.shootSpeed = 5f ;//设置弹幕速度
            Item.maxStack = Item.CommonMaxStack;
            Item.consumable = true;
            Item.value = Item.sellPrice(0, 0, 0, 7);//售卖价格
        }

        public override void AddRecipes()
        {
            CreateRecipe(70)
                .AddIngredient(ItemID.MusketBall, 70)
                .AddIngredient(ItemID.DemoniteBar)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}