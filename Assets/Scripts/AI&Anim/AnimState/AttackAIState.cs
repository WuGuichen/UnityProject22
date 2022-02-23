using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : IAnimState
{
    private int num = 1;
    public AttackAIState(int n = 1)
    {
        num = n;
    }

    public override void OnEnter()
    {
        if (num > m_AI.GetAttackTimes()) num = 1;
        clipName = "Attack_0" + num.ToString();
        // mirror = true;
        if (num == 0) speed = 1f;
        else speed = m_AI.GetAttackSpeed();;
        // Debug.Log(clipName + m_AI);

        rootMotion = true;

        lockPlanarVec = true;
        resetVec = true;
    }
    public override void Update()
    {
        // base.Update();
        if (WaitAnim()) return;
        if (EarlyInputAndTimeCheck(0.5f))
        {
            if (curAnimTime >= 0.95f)
            {
                // Debug.Log(curAnimTime);
                if(m_AI.IsHeavyAttack()) m_AI.ChangeAIState(new HeavyAttackAIState());
                if (nextRoll) m_AI.ChangeAIState(new WalkToRollAIState());
                else if (nextJump) m_AI.ChangeAIState(new JumpAIState());
                else if (nextAttack) m_AI.ChangeAIState(new AttackAIState(++num));
                else m_AI.ChangeAIState(new IdleAIState());
            }
        }
    }
    public override void OnExit()
    {
        m_AI.UnLockPlanarVec();
    }
}

public class HeavyAttackAIState : IAnimState
{
    public HeavyAttackAIState()
    {
    }
    public override void OnEnter()
    {
        clipName = "HeavyAttack";
        rootMotion = true;
        speed = 2f;
        lockPlanarVec = true;
        resetVec = true;
    }
    public override void Update()
    {
        // base.Update();
        if (WaitAnim()) return;
        if (EarlyInputAndTimeCheck(0.5f))
        {
            if (curAnimTime >= 0.95f)
            {
                // Debug.Log(curAnimTime);
                if (nextRoll) m_AI.ChangeAIState(new WalkToRollAIState());
                else if (nextJump) m_AI.ChangeAIState(new JumpAIState());
                else if (nextAttack) m_AI.ChangeAIState(new AttackAIState());
                else m_AI.ChangeAIState(new IdleAIState());
            }
        }
    }
    public override void OnExit()
    {
        m_AI.UnLockPlanarVec();
    }
}