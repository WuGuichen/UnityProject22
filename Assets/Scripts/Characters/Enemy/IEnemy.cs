using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_Enemy
{
    Null = 0,
    Slime = 1,
    Bat = 2,
}

public abstract class IEnemy : ICharacter
{
    protected bool m_bHit = false;
    protected float m_attackRange = 5f;       // 角色攻击范围
    protected string hitEffectName = "";
    protected StateBar m_StateBar;

    public IEnemy() { }
    public override void SetGameObject(GameObject theObj)
    {
        base.SetGameObject(theObj);
        m_StateBar = theObj.GetComponentInChildren<StateBar>();
        // Debug.Log(m_StateBar);
        m_StateBar.SetHPBarValue(1);
    }
    public override void UnderAttack(ICharacter attacker, Vector3 pos)
    {
        DoShowHitEffect(pos);
        float dmgValue = attacker.m_Attribute.GetDamageValue();
        Debug.Log("受到了:[" + m_Attribute.CalDamage(dmgValue) + "]点伤害");
        DoShowDamageValue((int)m_Attribute.CalDamage(dmgValue));
        if(m_Attribute.m_CurHP <= 0) Killed();
    }
    // public abstract void DoPlayHitSound();
    public abstract void DoShowHitEffect(Vector3 pos);
    public abstract void DoShowDamageValue(int value);
}
