using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttrStrategy : IAttrStrategy
{
    public override void InitAttr(ICharacterAttr characterAttr)
    {
        PlayerAttr thePlayerAttr = characterAttr as PlayerAttr;
        PlayerBaseAttr baseAttr = thePlayerAttr.GetBaseAttr() as PlayerBaseAttr;

        int Lv = thePlayerAttr.GetPlayerLv();
        thePlayerAttr.SetATK(baseAttr.m_ATK + 20*Lv);
        thePlayerAttr.SetDEF(baseAttr.m_DEF + 10*Lv);
        thePlayerAttr.SetMaxHP(CalMaxHP(Lv, baseAttr));
        thePlayerAttr.m_AttackSpeed = baseAttr.m_AttackSpeed;
    }
    private float CalMaxHP(int Lv, PlayerBaseAttr baseAttr)
    {
        float value = baseAttr.m_MaxHP *(1+0.5f*Lv);
        return value;
    }
}
