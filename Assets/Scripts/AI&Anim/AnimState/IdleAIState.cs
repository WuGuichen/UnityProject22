using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAIState : IAnimState
{
    // private float changeIdleTimer;
    public IdleAIState() { }

    public override void OnEnter()
    {
        clipName = "Ground";
        m_AI.UnLockPlanarVec();
        // m_AI.PlayAnimationClip(clipName);
        // Debug.Log("Ground");
        
        // changeIdleTimer = 3f;
    }
    // public override void OnTransition()
    // {
    //     if(m_AI.IsAttack()) m_AI.ChangeAIState(new AttackAIState());
    //     else if(m_AI.IsRoll()) m_AI.ChangeAIState(new WalkToRollAIState());
    //     else if (m_AI.IsJump()) m_AI.ChangeAIState(new JumpAIState());
    //     if(m_AI.IsWalk()) m_AI.ChangeAIState(new WalkAIState());
    //     if(m_AI.IsRun()) m_AI.ChangeAIState(new RunAIState());
    //     else if(m_AI.IsFalling()) m_AI.ChangeAIState(new FallingAIState());
    // }
    public override void Update()
    {
        if(WaitAnim()) return;
        // if(m_AI.IsLock()) m_AI.LockUnLock();
        if(m_AI.IsAttack()) m_AI.ChangeAIState(new AttackAIState());
        else if(m_AI.IsHeavyAttack()) m_AI.ChangeAIState(new HeavyAttackAIState());
        else if (m_AI.IsJump()) m_AI.ChangeAIState(new JumpAIState());
        else if(m_AI.IsRoll()) m_AI.ChangeAIState(new WalkToRollAIState());
        else if(m_AI.IsWalk()) m_AI.ChangeAIState(new WalkAIState());
        else if(m_AI.IsRun()) m_AI.ChangeAIState(new RunAIState());
        else if(m_AI.IsFalling()) m_AI.ChangeAIState(new FallingAIState());
    }
}
