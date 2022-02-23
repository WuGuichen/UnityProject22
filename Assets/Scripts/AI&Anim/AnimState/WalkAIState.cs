using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAIState : IAnimState
{
    public WalkAIState(){}

    public override void OnEnter()
    {
        clipName = "Ground";
        m_AI.UnLockPlanarVec();
        // m_AI.PlayAnimationClip(clipName);
    }
    public override void Update()
    {
        if(WaitAnim()) return;
        if(m_AI.IsFalling()) m_AI.ChangeAIState(new FallingAIState());
        else if(m_AI.IsRoll()) m_AI.ChangeAIState(new WalkToRollAIState());
        else if(m_AI.IsAttack()) m_AI.ChangeAIState(new AttackAIState());
        else if(m_AI.IsHeavyAttack()) m_AI.ChangeAIState(new HeavyAttackAIState());
        else if (m_AI.IsJump())
            m_AI.ChangeAIState(new JumpAIState());
        else if(m_AI.IsRun())
            m_AI.ChangeAIState(new RunAIState());
        else if(!m_AI.IsWalk()) m_AI.ChangeAIState(new IdleAIState());
    }
}
