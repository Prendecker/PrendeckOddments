using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Ranged
{

    public class B_Gun : ModItem
    {

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.SetWeaponValues(131313, 1000, 96);
            Item.DefaultToRangedWeapon(1,AmmoID.Bullet,6,20f,true);
            Item.width = 40;
            Item.height = 40;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.value = Item.buyPrice(1, 0, 0, 0);
            Item.rare = ItemRarityID.Red;
            Item.UseSound = new SoundStyle($"PrendeckOddments/Assets/Sounds/Items/shoot_freesound_community") { Volume = 1f, PitchVariance = 0.8f, MaxInstances = 1 };

        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo Source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i <= 5; i += 1)
            {
                velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5f));// 设置随机角度
                float speedScale = Main.rand.NextFloat(0.75f, 1.4f);// 设置随机速度
                velocity *= speedScale;// 乘以随机速度
                Projectile.NewProjectile(Source, position, velocity, ProjectileID.FairyQueenRangedItemShot, damage, knockback);
            }
            return false;
        }

        //public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        //{
        //    velocity = velocity.RotatedByRandom(MathHelper.ToRadians(10f));
        //}

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.75f;//有75%的概率不消耗弹药
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3,3);//物品贴图手持偏移
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