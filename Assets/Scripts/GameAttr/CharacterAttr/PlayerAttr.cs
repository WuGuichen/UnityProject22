using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttr : ICharacterAttr
{
    protected int m_PlayerLv;
    
    public PlayerAttr(){}

    public void SetPlayerAttr(BaseAttr baseAttr)
    {
        base.SetBaseAttr(baseAttr);
        m_AttackTimes = 4;
        m_AttackSpeed = 2f;
    }
    public int GetPlayerLv()
    {
        return m_PlayerLv;
    }
    public void SetPlayerLv(int Lv)
    {
        m_PlayerLv = Lv;
    }
    public void SetATK(float value)
    {
        m_ATK = value;
    }
    public void SetDEF(float vlaue)
    {
        m_DEF = vlaue;
    }
}
