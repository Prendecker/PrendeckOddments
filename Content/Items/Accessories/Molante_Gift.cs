using PrendeckOddments.Content.Mod_Player.Accessories;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Accessories
{
    public class Molante_Gift : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToAccessory(36, 40);
            Item.SetShopValues(ItemRarityColor.Green2, Item.sellPrice(0,3,35,0));
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetJumpState<Molante_Gift_Jump>().Enable();
            player.GetModPlayer<Molante_Gift_Jump_Player>().Have_Molante_Gift = true;
        }
    }

    public class Molante_Gift_Jump : ExtraJump
    {
        public override Position GetDefaultPosition() => new After(BlizzardInABottle);
        public override float GetDurationMultiplier(Player player) => 1.25f;
    }
}
