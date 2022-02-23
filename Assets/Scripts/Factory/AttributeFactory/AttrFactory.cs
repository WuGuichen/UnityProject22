using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttrFactory : IAttrFactory
{
    // private Dictionary<int, BaseAttr> m_
    private Dictionary<int, EnemyBaseAttr> m_EnemyAttrDB = null;
    private Dictionary<int, PlayerBaseAttr> m_PlayerAttrDB = null;
    public AttrFactory()
    {
        InitEnemyAttr();
        InitPlayerAttr();
    }
    private void InitEnemyAttr()
    {
        m_EnemyAttrDB = new Dictionary<int, EnemyBaseAttr>();
        m_EnemyAttrDB.Add(100, new EnemyBaseAttr("SlimeAttr", 1000, 100, 70));
    }
    private void InitPlayerAttr()
    {
        m_PlayerAttrDB = new Dictionary<int, PlayerBaseAttr>();
        m_PlayerAttrDB.Add(1, new PlayerBaseAttr("PlayerOneAttr", 1000, 100, 70, 2));
        m_PlayerAttrDB.Add(2, new PlayerBaseAttr("PlayerTwoAttr", 1000, 100, 70, 1));
    }

    public override EnemyAttr GetEnemyAttr(int attrID)
    {
        if (m_EnemyAttrDB.ContainsKey(attrID) == false)
        {
            Debug.LogWarning("GetEnemyAttr:AttrID[" + attrID + "]数值不存在");
            return null;
        }
        EnemyAttr newAttr = new EnemyAttr();
        newAttr.SetEnemyAttr(m_EnemyAttrDB[attrID]);
        return newAttr;
    }

    public override PlayerAttr GetPlayerAttr(int attrID)
    {
        if (m_PlayerAttrDB.ContainsKey(attrID) == false)
        {
            Debug.LogWarning("GetPlayerAttr:AttrID[" + attrID + "]数值不存在");
            return null;
        }
        PlayerAttr newAttr = new PlayerAttr();
        newAttr.SetPlayerAttr(m_PlayerAttrDB[attrID]);
        return newAttr;
    }
}
