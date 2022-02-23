using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAIState : IAnimState
{
    public JumpAIState() { }
    public override void OnEnter()
    {
        if (m_AI.GetSqrSpeed() <= 0.1f) clipName = "Jump_01";
        else clipName = "Jump_02";

        // m_AI.LockPlanarVec();  // 锁定速度
        lockPlanarVec = true;  // 不立即锁定速度
        m_AI.ThrustUp(4f);
    }

    public override void Update()
    {
        // base.Update();
        if (WaitAnim()) return;
        if (EarlyInputAndTimeCheck(0.7f))
        {
            if (m_AI.IsFalling() && curAnimTime >= 0.95f)
                m_AI.ChangeAIState(new FallingAIState());
            else if (m_AI.IsOnGround() && curAnimTime >= 0.85f)
            {
                if (nextAttack) m_AI.ChangeAIState(new AttackAIState());
                else if (nextRoll)
                {
                    m_AI.ChangeAIState(new WalkToRollAIState());
                }
                else m_AI.ChangeAIState(new IdleAIState());
            }
        }
    }
    public override void OnExit()
    {
        // m_CharacterAI.UnLockPlanarVec();  // 解除锁定
    }
}

// public class Jump2AIState : IAnimState
// {
//     public override void OnEnter()
//     {
//         clipName = "Jump_02";

//         lockPlanarVec = true;
//         m_AI.ThrustUp(4f);
//     }
//     public override void Update()
//     {
//         if(WaitAnim()) return;
//         if (m_AI.IsOnGround() && curAnimTime >= 0.9f)
//         {
//             m_AI.ChangeAIState(new IdleAIState());
//         }
//         if (m_AI.IsFalling() && m_AI.GetCurAnimTime() >= 0.98f)
//             m_AI.ChangeAIState(new FallingAIState());
//     }
// }