using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_INPUT
{
    Null = 0,
    Attack = 1,
    Jump = 2,
    Run = 3,
    Walk = 4,
    Idle = 5,
    Rush = 6,
    Lock = 7,
    HeavyAttack = 8,
}
public class ICharacterAI
{
    public ICharacter m_Character{get; protected set;}
    public IAnimState m_AIState {get; protected set;}

    public int hitCount{get; protected set;}

    public ICharacterAI(ICharacter character)
    {
        m_Character = character;
    }
    public virtual void ChangeAIState(IAnimState newAIState)
    {
        if(m_AIState!= null)
            m_AIState.OnExit();
        newAIState.SetCharacterAI(this);
        newAIState.OnEnter();
        m_AIState = newAIState;
        
    }

    public void Killed()
    {
    }
    // 更新AI
    public void  Update()
    {
        m_AIState.Update();
    }
    public void RemoveAITarget(ICharacter target)
    {
        // m_AIState.RemoveTarget(target);
    }
    // 是否状态转换
    public virtual bool IsAttack()
    {
        return m_Character.CheckInput(ENUM_INPUT.Attack);
    }
    public virtual bool IsHeavyAttack()
    {
        return m_Character.CheckInput(ENUM_INPUT.HeavyAttack);
    }
    public virtual bool IsLock()
    {
        return m_Character.CheckInput(ENUM_INPUT.Lock);
    }
    public virtual bool IsJump()
    {
        return m_Character.CheckInput(ENUM_INPUT.Jump);
    }
    public virtual bool IsWalk()
    {
        return m_Character.CheckInput(ENUM_INPUT.Walk);
    }
    public virtual bool IsRun()
    {
        return m_Character.CheckInput(ENUM_INPUT.Run);
    }
    public virtual bool IsRoll()
    {
        return m_Character.CheckInput(ENUM_INPUT.Rush);
    }
    public virtual bool IsIdle()
    {
        return m_Character.CheckInput(ENUM_INPUT.Idle);
    }
    public virtual bool IsDead()
    {
        return m_Character.IsKilled();
    }
    public void TryToGetHit(ICharacter attacker, Vector3 pos)
    {
        if(m_AIState.unHitable) return;
        hitCount++;
        ChangeAIState(new GetHitAIState(hitCount%2));
        m_Character.UnderAttack(attacker, pos);
    }
    public void LockPlanarVec(bool resetVec = false)
    {
        m_Character.LockOrUnlockPlanarVec(true, resetVec);
    }
    public void UnLockPlanarVec(bool resetVec = false)
    {
        m_Character.LockOrUnlockPlanarVec(false, resetVec);
    }
    public void SetInputEnable(bool value)
    {
        m_Character.SetInputEnable(value);
    }
    public void PlayAnimationClip(string clipName, float offetTime = float.NegativeInfinity, float duration = 0.15f, int layer = 0,
     float speed = 1, bool rootMotion =false, bool mirror = false)
    {
        m_Character.PlayAnimationClip(clipName, offetTime, duration, layer, speed, rootMotion, mirror);
    }
    public void PlayAnimationClip(int hash, float offetTime = float.NegativeInfinity)
    {
        m_Character.PlayAnimationClip(hash, offetTime);
    }
    // public void ChangeIdleType()
    // {
    //     m_Character.PlayAnimationClip("Idle2", float.NegativeInfinity, 0.2f,0,1, false);
    // }
    public float GetCurAnimTime(int layerIndex = 0)
    {
        return m_Character.GetCurAnimTime(layerIndex);
    }
    public string GetCurAnimName(int layerIndex = 0)
    {
        return m_Character.GetAnimatorClipInfo(layerIndex)[0].clip.name;
    }
    public bool CheckCurAnimName(string name, int layerIndex = 0)
    {
        bool isName = m_Character.GetAnimatorStateInfo(layerIndex).IsName(name);
        return isName;
    }
    public float GetCurStateHash(int layerIndex = 0)
    {
        return m_Character.GetAnimatorStateInfo(layerIndex).fullPathHash;
    }
    public void ThrustUp(float value)
    {
        m_Character.SetThrust(new Vector3( 0,value,0));
    }
    public void ThrustForward(float value)
    {
        m_Character.ThrustForward(value);
    }
    public float GetSqrSpeed()
    {
        return m_Character.SqrSpeed;
    }

    public virtual bool IsOnGround()
    {
        return m_Character.IsGround;
    }
    public virtual bool IsFalling()
    {
        return m_Character.IsFalling;
    }
    public int GetAttackTimes()
    {
        return m_Character.m_Attribute.m_AttackTimes;
    }
    public float GetAttackSpeed()
    {
        return m_Character.m_Attribute.m_AttackSpeed;
    }
}
