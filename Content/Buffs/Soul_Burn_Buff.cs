using Microsoft.Xna.Framework;
using PrendeckOddments.Content.Projectiles;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content.Buffs
{
    internal class Soul_Burn_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
            Main.buffNoTimeDisplay[Type] = false;
            Main.persistentBuff[Type] = false;
            Main.vanityPet[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Generic) *= 1.65f;
            player.GetCritChance(DamageClass.Generic) += 45;

            if (player.lifeRegen >= 0)
            {
                player.lifeRegen = 0;
            }
            player.lifeRegenTime = 0;
            player.lifeRegen -= 30;

            if (Main.rand.NextBool(2))
            {
                Dust d = Dust.NewDustDirect(player.position, player.width, player.height, DustID.LifeDrain);
                d.velocity *= 0.75f;
            }
        }
    }
}
