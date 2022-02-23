using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingAIState : IAnimState
{
    bool roll = false;
    bool landing = false;
    public FallingAIState() { }

    public override void OnEnter()
    {
        clipName = "FallingIdle";
        m_AI.LockPlanarVec();  // 锁定速度
        Debug.Log("falling");

    }
    public override void Update()
    {
        if(WaitAnim()) return;
        float sqrSpeed = m_AI.GetSqrSpeed();
        if (!roll && sqrSpeed >= 26f)
        {
            landing = true;
            if (sqrSpeed >= 110f)
            {
                roll = true;
                landing = false;
            }
        }
        if (m_AI.IsOnGround())
        {
            if (roll)
                m_AI.ChangeAIState(new FallingToRollAIState());
            else if (landing)
                m_AI.ChangeAIState(new FallingToLandingAIState());
            else m_AI.ChangeAIState(new IdleAIState());
        }
    }
}

public class FallingToRollAIState : IAnimState
{
    public FallingToRollAIState() { }

    public override void OnEnter()
    {
        clipName = "Landing_02";
        speed = 3f;
        rootMotion = true;
    }
    public override void Update()
    {
        if(WaitAnim()) return;
        // base.Update();
        if (curAnimTime >= 0.95f)
            m_AI.ChangeAIState(new IdleAIState());
        else if (curAnimTime >= 0.5f)
            m_AI.UnLockPlanarVec();
    }
}

public class FallingToLandingAIState : IAnimState
{
    public FallingToLandingAIState() { }
    public override void OnEnter()
    {
        clipName = "Landing_01";
        speed = 2f;
        m_AI.LockPlanarVec(true);
        m_AI.SetInputEnable(false);
    }
    public override void Update()
    {
        if(WaitAnim()) return;
        // base.Update();
        if (curAnimTime >= 0.95f)
            m_AI.ChangeAIState(new IdleAIState());
    }
}