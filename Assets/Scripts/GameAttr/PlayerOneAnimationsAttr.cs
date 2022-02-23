
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneAnimationsAttr:IAnimationsAttr
{
    public override void SetJump(IAnimState state, bool idle)
    {
        if(idle) state.SetAnimAttr("Jump", 1, false);
        else state.SetAnimAttr("Jump2", 1, false);
    }
}