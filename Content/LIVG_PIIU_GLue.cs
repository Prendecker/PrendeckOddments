using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace PrendeckOddments.Content
{
    ///<summary>
    ///修改原版3个骗伤弹幕并注入一些咒骂re的话 
    ///算了QTMD直接把骗上弹幕直接改成独立无敌帧算了 
    ///</summary>
    public class LIVG_PIIU_GLue : GlobalProjectile
    {
        private static readonly HashSet<int> Rocket_Proj = new() 
        { 338, 793, 796,
          339, 340, 341,
          803, 804, 808,
          809, 135, 138,
          141, 144, 778,
          782, 795, 798,
          134, 137, 140,
          143, 776, 780,
          862, 863, 779,
          783};

        //private static readonly HashSet<int> Cluster_Fragments = new() {862, 863, 779, 783 };

        public override void SetDefaults(Projectile entity)
        {
            if (entity.type == ProjectileID.UnholyArrow ||
                entity.type == ProjectileID.JestersArrow ||
                entity.type == ProjectileID.HellfireArrow) 
            {
                entity.usesLocalNPCImmunity = true;
                entity.localNPCHitCooldown = 25;
            }//3个骗伤箭矢的修改 

            if(entity.type == ProjectileID.StarCannonStar)
            {
                entity.timeLeft = 900;
                entity.usesLocalNPCImmunity = true;
                entity.localNPCHitCooldown = 10;
            }//让星星炮不再骗伤，改存在时间是为了防止电脑卡到爆

            if(Rocket_Proj.Contains(entity.type))
            {
                entity.usesLocalNPCImmunity = true;
                entity.localNPCHitCooldown = -1;
            }//你妈到底有多少个火箭炮啊 

            //if (Cluster_Fragments.Contains(entity.type))
            //{
            //    entity.friendly = false;
            //    entity.timeLeft = 300;
            //}//RE说真的你改的还是挺对的，尼玛的我雪人炮用集束火箭TMD裸装能达到2000的伤害WOCHAO

        }
        ///<summary>
        ///笑点解析：用的局部无敌帧所以会骗伤
        ///还有感谢 哦盆扣得 的代码指导
        ///</summary>

        //public override void AI(Projectile entity)
        //{
        //    if (Cluster_Fragments.Contains(entity.type))
        //    {
        //        if (entity.timeLeft <= 290)
        //        {
        //            entity.friendly = true;
        //        }
        //        else
        //        {
        //            entity.friendly = false;
        //        }
        //    }
        //}
        //恐怕救不了抬棺，等未来再弄吧 
    }
}
