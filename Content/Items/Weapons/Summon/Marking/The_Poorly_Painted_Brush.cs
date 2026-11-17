using Microsoft.Xna.Framework;
using PrendeckOddments.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Items.Weapons.Summon.Marking
{
    public class The_Poorly_Painted_Brush : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 1;
        }

        public override void SetDefaults()
        {
            Item.autoReuse = true;
            Item.SetWeaponValues(14, 2.3f, 1); //设置伤害，击退，暴击率
            Item.DamageType = DamageClass.SummonMeleeSpeed; //设置伤害类型
            Item.width = 44;
            Item.height = 44;
            Item.noMelee = true;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.shootsEveryUse = true;
            Item.useStyle = ItemUseStyleID.Swing;
            // 这里保留 sellPrice：原来下面的 buyPrice(0,0,0,0) 会把它覆盖成 0，
            // 导致画笔一文不值。写两行只有最后一行生效，所以只能留一行。
            Item.value = Item.sellPrice(0, 0, 7, 0);
            Item.shoot = 1;//设置射弹，保证执行“Shoot”就行
            Item.shootSpeed = 0f;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, Main.MouseWorld, velocity, ModContent.ProjectileType<Painted_Brush_BOOM>(), damage, knockback);
            return false;
        }

        //public override Vector2? HoldoutOffset()//手持贴图偏移
        //{
        //    return new Vector2(-14, 8);
        //}
    }
}
