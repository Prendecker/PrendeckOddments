using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Hi : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.PrendeckOddments.hjson' file.
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.damage = 114514;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 1000;
			Item.value = Item.sellPrice(1,0,0,0);
			Item.value = Item.buyPrice(1,0,0,0);
			Item.shoot = ProjectileID.DemonScythe;
			Item.shootSpeed = 15f;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item71;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
