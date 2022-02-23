using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAIState : IAnimState
{
    public RunAIState(){}

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
        else if(m_AI.IsRoll()) m_AI.ChangeAIState(new WalkToRollAIState2());
        else if(m_AI.IsAttack()) m_AI.ChangeAIState(new AttackAIState(0));
        else if (m_AI.IsJump()) m_AI.ChangeAIState(new JumpAIState());
        else if(m_AI.IsWalk()) m_AI.ChangeAIState(new WalkAIState());
        else if(m_AI.IsRun() == false) m_AI.ChangeAIState(new IdleAIState());
    }
}
