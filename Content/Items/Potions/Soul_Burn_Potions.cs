using Microsoft.Xna.Framework;
using PrendeckOddments.Content.Buffs;
using PrendeckOddments.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Potions
{

    public class Soul_Burn_Potions : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 28;
            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(1, 0, 0, 0);//
            Item.value = Item.buyPrice(0, 5, 0, 0);//
            Item.useTime = 15;//物品使用间隔时间
            Item.useAnimation = 15;//物品使用动画时间
            Item.useStyle = ItemUseStyleID.DrinkLong;
            Item.shootSpeed = 15f;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = Item.CommonMaxStack;//物品堆叠没什么好说的除某些东西外直接‘Item.CommonMaxStack’
            Item.consumable = true;//确认这是否为消耗品
            Item.buffType = ModContent.BuffType<Soul_Burn_Buff>();//给予的Buff
            Item.buffTime = 660;//Buff时间，单位为tick1秒=60tick，所以是11秒
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Deathweed, 1);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddIngredient(ItemID.FlarefinKoi, 1);
            recipe.AddTile(TileID.Bottles);// 其实只用玻璃瓶就行了
            recipe.Register();
        }
    }
}
