using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotionController : MonoBehaviour
{
    private Animator anim;
    private Vector3 animDeltaPos;
    private CharacterHandler handler;
    public void Init(CharacterHandler handler)
    {
        this.handler = handler;
        anim = handler.anim;
        // Debug.Log("handler:" + handler);
    }

    private void OnAnimatorMove()
    {
        handler.AddRootMotion(anim.deltaPosition);
    }
}
