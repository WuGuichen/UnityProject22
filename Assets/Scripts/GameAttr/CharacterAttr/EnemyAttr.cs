using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttr : ICharacterAttr
{
    public EnemyAttr(){}

    public void SetEnemyAttr(BaseAttr baseAttr)
    {
        base.SetBaseAttr(baseAttr);
        m_AttackTimes = 2;
        m_AttackSpeed = 1;
    }
    public void SetEnemyLv(int Lv)
    {
        m_Lv = Lv;
    }
}
