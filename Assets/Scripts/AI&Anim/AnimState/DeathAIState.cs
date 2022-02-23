using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAIState : IAnimState
{
    public DeathAIState(){}

    public override void OnEnter()
    {
        clipName = "Death";
        lockPlanarVec = true;
        resetVec = true;
    }
    public override void Update()
    {
        if(WaitAnim()) return;
    }
}
