using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkToRollAIState : IAnimState
{
    // private bool rollFlag;  // 标记下次翻滚
    public WalkToRollAIState() { }


    public override void OnEnter()
    {
        clipName = "Roll_01";
        speed = 3;
        rootMotion = true;

        lockPlanarVec = true;

        // rollFlag = false;

        // Debug.Log("roll" + curAnimTime + "name:" + m_AI.GetCurAnimName());

        // m_AI.LockPlanarVec();
        // m_CharacterAI.ThrustForward(25f);
        // m_AI.PlayAnimationClip(clipName, speed: 3f, duration: 0f, rootMotion: true);
    }
    public override void Update()
    {
        if (WaitAnim()) return;
        // base.Update();
        // m_CharacterAI.ThrustForward(2.5f);
        // if(exitTime.state == MyTimer.STATE.RUN) return;
        if (EarlyInputAndTimeCheck(0.65f))
        {
            if (curAnimTime >= 0.9f)
            {
                // m_AI.ChangeAIState(new RunToRollAIState());
                // if(curAnimTime>1) m_AI.ChangeAIState(new WalkToRollAIState());
                if (nextRoll)
                {
                    m_AI.ChangeAIState(new RunToRollAIState());
                }
                else if(nextJump) m_AI.ChangeAIState(new JumpAIState());
                else if(nextAttack) m_AI.ChangeAIState(new AttackAIState());
                else
                {
                    m_AI.ChangeAIState(new IdleAIState());
                }
            }
        }
        if (m_AI.IsFalling())
            m_AI.ChangeAIState(new FallingAIState());
    }
    public override void OnExit()
    {
        m_AI.UnLockPlanarVec();
    }
}

public class WalkToRollAIState2 : IAnimState
{
    public WalkToRollAIState2() { }

    public override void OnEnter()
    {
        clipName = "Roll_01";
        Debug.Log("跑起来");

        m_AI.LockPlanarVec();
        // m_CharacterAI.ThrustForward(20f);
        m_AI.PlayAnimationClip(clipName, speed: 3f, duration: 0f, rootMotion: true);
    }
    public override void Update()
    {
        // base.Update();
        if (WaitAnim()) return;
        // exitTime.Tick();
        // m_CharacterAI.ThrustForward(5f);
        // if(exitTime.state == MyTimer.STATE.RUN) return;
        // if (curAnimTime >= 0.95f)
        //     m_AI.ChangeAIState(new IdleAIState());
        if (curAnimTime >= 0.95f)
            m_AI.ChangeAIState(new IdleAIState());
        if (m_AI.IsFalling())
            m_AI.ChangeAIState(new FallingAIState());
    }
}
public class RunToRollAIState : IAnimState
{
    public RunToRollAIState() { }

    public override void OnEnter()
    {
        clipName = "Roll_02";
        speed = 3;
        rootMotion = true;

        m_AI.LockPlanarVec();
        // m_CharacterAI.ThrustForward(20f);
        // m_AI.PlayAnimationClip(clipName, speed: 3f, duration: 0f, rootMotion: true);
    }
    public override void Update()
    {
        // base.Update();
        if (WaitAnim()) return;
        // exitTime.Tick();
        // m_CharacterAI.ThrustForward(5f);
        // if(exitTime.state == MyTimer.STATE.RUN) return;
        if (curAnimTime >= 0.95f)
            m_AI.ChangeAIState(new IdleAIState());
        // if(curAnimTime >= 0.95f)
        //     m_AI.ChangeAIState(new WalkToRollAIState());
        if (m_AI.IsFalling())
            m_AI.ChangeAIState(new FallingAIState());
    }
}