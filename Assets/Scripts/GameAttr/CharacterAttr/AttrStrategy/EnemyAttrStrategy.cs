using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttrStrategy : IAttrStrategy
{
    // 初始化属性(不做计算)
    public override void InitAttr(ICharacterAttr characterAttr)
    {
        EnemyAttr theEnemyAttr = characterAttr as EnemyAttr;
        EnemyBaseAttr baseAttr = theEnemyAttr.GetBaseAttr() as EnemyBaseAttr;

        int Lv = theEnemyAttr.m_Lv;

        // 根据等级和基础属性设置实际属性
        theEnemyAttr.SetMaxHP(baseAttr.m_MaxHP * (1+ Lv*0.5f));

        // int Lv = theEnemyAttr.GetPlayerLv();
    }
    // public float GetMaxHP()
    // {
    // }
}
