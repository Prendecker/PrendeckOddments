using Microsoft.Xna.Framework;
using PrendeckOddments.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Melee
{ 

	public class Sigkin : ModItem
	{

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
		{
			Item.autoReuse = true;
			Item.SetWeaponValues(114514, 1000, 100); //设置伤害，暴击率，射速
            Item.DamageType = DamageClass.Melee; //设置伤害类型
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.shootsEveryUse = true;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(1,0,0,0);
			Item.value = Item.buyPrice(1,0,0,0);
			Item.shoot = ModContent.ProjectileType<Sigma>();
            Item.shootSpeed = 15f;
			Item.rare = ItemRarityID.Pink;
			Item.UseSound = SoundID.Item71;

		}

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo Source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(Source, position, velocity, type, damage, knockback);
            return false;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(6.5f));
        }

        //public override void AddRecipes()
		//{
		//	Recipe recipe = CreateRecipe();
		//	recipe.AddIngredient(ItemID.DirtBlock, 10);
		//	recipe.AddTile(TileID.WorkBenches);
		//	recipe.Register();
		//}
	}
}
