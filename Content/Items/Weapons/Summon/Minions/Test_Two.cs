using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Summon.Minions
{
    public class Test_Two : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }    
        public override void SetDefaults()   
        {        
            Item.SetWeaponValues(5000, 1000, 100); //设置伤害、击退、暴击率
            //Item.DefaultToS
            Item.UseSound = SoundID.Item25;
            Item.mana = 5;
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            // 只留一行：buyPrice 是买入价，sellPrice 是卖出价，写两行后者会覆盖前者
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Pink;
        }

    }
    
}
