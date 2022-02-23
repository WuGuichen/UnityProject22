using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitAIState : IAnimState
{
    int num;
    public GetHitAIState(int n = 1){
        num = n;
    }

    public override void OnEnter()
    {
        clipName = "GetHit_" + num;
        Debug.Log(clipName);
        m_AI.LockPlanarVec(true);
    }
    public override void Update()
    {
        if(WaitAnim()) return;
        if(curAnimTime >= 0.95f)
            m_AI.ChangeAIState(new IdleAIState());
    }
}
