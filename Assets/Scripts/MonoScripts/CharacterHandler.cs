using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public ICharacter m_Character{get; protected set;}
    [SerializeField]
    public IUserInput pi{get; protected set;}
    public GameObject model{get; protected set;}
    protected Rigidbody rigid;
    public Animator anim{get; protected set;}
    protected Vector3 planarVec = Vector3.zero; // 给物理模拟提供当前平面速度
    protected bool lockPlanarVec = false; // 锁定平面速度
    protected bool traceDirector = false; // 将前方设为输入方向
    protected Vector3 thrustVec = Vector3.zero;  // 一个冲量

    protected bool isFalling;
    protected bool isGround;
    protected bool isLock;

    protected bool isRootMotion = false;
    protected Vector3 animDeltaPos = new Vector3(0,0,0);

    // 控制平面速度
    public void LockVec(bool resetVec)
    {
        if(resetVec) planarVec = new Vector3(0, planarVec.y, 0);
        else traceDirector = true;
        pi.inputEnable = false;
        lockPlanarVec = true;
    }
    public void UnlockVec(bool resetVec)
    {
        if(resetVec) planarVec = new Vector3(0, planarVec.y, 0);
        pi.inputEnable = true;
        lockPlanarVec = false;
        traceDirector = false;
    }
    public void SetThrustVec(Vector3 vec)
    {
        if(thrustVec.y == 0)
            thrustVec += vec;
        else
            thrustVec = vec;
    }
    public void ThrustForward(float value)
    {
        thrustVec = model.transform.forward * value;
    }
    public bool IsFalling()
    {
        if(IsGround()) return false;
        return isFalling;
    }
    public bool IsLock()
    {
        return isLock;
    }
    public bool IsGround()
    {
        return isGround;
    }
    public void SetIsOnGound(bool value)
    {
        isGround = value;
    }
    public void SetTraceDir(bool value)
    {
        traceDirector = value;
    }
    public float GetSqrSpeed()
    {
        return rigid.velocity.sqrMagnitude;
    }
    // 是否允许输入
    public void SetInputEnable(bool value)
    {
        pi.SetInputEnable(value);
    }
    public void SetRootMotion(bool value)
    {
        isRootMotion = value;
    }
    public void AddRootMotion(Vector3 deltaPos)
    {
        // animDeltaPos += (0.2f*animDeltaPos+ 0.8f*deltaPos);
        animDeltaPos += deltaPos;
    }
    public void SetCharacter(ICharacter character)
    {
        m_Character = character;
    }
}
