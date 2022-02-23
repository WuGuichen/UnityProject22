using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAttr
{
    public abstract string GetAttrName();
}

// 公用基本角色数值
public class CharacterBaseAttr : BaseAttr
{
    public float m_MaxHP{get; protected set;}
    public float m_ATK{get; protected set;}
    public float m_DEF{get; protected set;}
    public string m_AttrName{get; protected set;}
    public float m_AttackSpeed{get; protected set;}
    public CharacterBaseAttr(string attrName, float maxHP, float ATK, float DEF)
    {
        m_AttrName = attrName;
        m_MaxHP = maxHP;
        m_ATK = ATK;
        m_DEF = DEF;
    }
    public override string GetAttrName()
    {
        return m_AttrName;
    }
}

public class EnemyBaseAttr : CharacterBaseAttr
{
    public EnemyBaseAttr(string attrName, float maxHP, float ATK, float DEF) : base(attrName, maxHP, ATK, DEF)
    {
    }

    public override string GetAttrName()
    {
        return base.GetAttrName();
    }
}
public class PlayerBaseAttr : CharacterBaseAttr
{
    public PlayerBaseAttr(string attrName, float maxHP, float ATK, float DEF, float AtkSpeed) : base(attrName, maxHP, ATK, DEF)
    {
        m_AttackSpeed = AtkSpeed;
    }
    public override string GetAttrName()
    {
        return base.GetAttrName();
    }
}
