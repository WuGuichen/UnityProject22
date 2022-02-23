using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAnimState
{
    // protected MyTimer exitTime = new MyTimer();
    // protected float exitTime = 0;
    protected string clipName = "";
    protected float speed = 1;
    protected bool rootMotion = false;
    protected bool mirror = false;
    protected bool lockPlanarVec = false;
    protected bool resetVec = false;
    protected float curAnimTime;
    protected ICharacterAI m_AI = null;
    protected bool transFlag;
    public bool unHitable { get; protected set; }


    protected bool nextAttack = false;
    protected bool nextRoll = false;
    protected bool nextJump = false;
    public IAnimState() { }

    public void SetCharacterAI(ICharacterAI characterAI)
    {
        m_AI = characterAI;
        transFlag = false;
        curAnimTime = 0;
        unHitable = false;
    }
    public virtual void OnEnter() { }
    protected bool WaitAnim()
    {
        // 任何时间都可进行判断
        if (m_AI.IsDead()) m_AI.ChangeAIState(new DeathAIState());

        if (!transFlag)  // 等待动画转换完成
        {
            OnTransition();
            m_AI.PlayAnimationClip(clipName, speed: speed, rootMotion: rootMotion, mirror: mirror);

            if (m_AI.CheckCurAnimName(clipName))
            {
                // Debug.Log(clipName + "转入");
                if (lockPlanarVec) m_AI.LockPlanarVec(resetVec);
                else m_AI.UnLockPlanarVec();
                transFlag = true;  // 减少条件判断开销(成功转到clipName)
            }
            return true;  // 未转到clipName, 不执行接下来的Update
        }
        curAnimTime = m_AI.GetCurAnimTime();
        return false;
    }
    // 放在除Ground状态下首先执行
    public virtual void Update()
    {
    }
    public virtual void OnExit() { }
    public virtual void OnTransition() { }

    public void SetAnimAttr(string clipName, float speed, bool rootMotion)
    {
        this.clipName = clipName;
        this.speed = speed;
        this.rootMotion = rootMotion;
    }

    protected bool EarlyInputAndTimeCheck(float time)
    {
        if(curAnimTime < time) return false;
        if (m_AI.IsAttack()) nextAttack = true;
        if (m_AI.IsJump()) nextJump = true;
        if (m_AI.IsRoll()) nextRoll = true;
        return true;
    }
}
