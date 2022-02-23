using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterAttr
{
    protected BaseAttr m_BaseAttr = null;   // 基础属性
    public float m_MaxHP{get; protected set;}
    public float m_CurHP{get; protected set;}
    public float m_ATK{get; protected set;}
    public float m_DEF{get; protected set;}
    public int m_Lv{get; protected set;}
    public int m_AttackTimes{get; protected set;}
    public float m_AttackSpeed;
    protected IAttrStrategy m_AttrStrategy = null; // 属性计算策略

    public ICharacterAttr(){}
    public void InitAttr(int Lv =1)
    {
        m_Lv = Lv;
        m_AttrStrategy.InitAttr(this);
        m_CurHP = m_MaxHP; // 加满血
    }

    public void SetBaseAttr(BaseAttr baseAttr)
    {
        m_BaseAttr = baseAttr;
    }
    public BaseAttr GetBaseAttr()
    {
        return m_BaseAttr;
    }
    public void SetAttrStrategy(IAttrStrategy theAttrStrategy)
    {
        m_AttrStrategy = theAttrStrategy;
    }
    public void SetMaxHP(float value)
    {
        m_MaxHP = value;
    }
    public void ReduceHP(float value)
    {
        m_CurHP -= value;
    }
    public float GetHPRate()
    {
        return m_CurHP/m_MaxHP;
    }
    public float GetDamageValue()
    {
        return m_ATK*2;
    }
    public float CalDamage(float dmgValue)
    {
        float value = dmgValue - m_DEF;
        ReduceHP(value);
        return value;
    }
}
