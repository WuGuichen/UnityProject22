using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacter
{
    // 角色信息
    public string m_Name { get; protected set; }
    public string m_AssetName { get; protected set; }
    public string m_AssetLable { get; protected set; }
    public int m_AttrID { get; protected set; }
    public GameObject m_GameObject{get; protected set;}
    protected AudioSource m_Audio = null;
    protected Rigidbody m_Rigidbody = null;
    protected CharacterHandler m_Handler = null;
    protected IUserInput m_Input = null;
    protected Animator m_Anim = null;
    protected bool transCoolDown = false;
    private float transCoolDownTime = 0.2f;

    // 角色死亡处理信息
    protected bool m_bKilled = false;  // 是否阵亡
    protected bool m_bCheckKilled = false;  // 是否确认阵亡事件
    protected float m_RemoveTimer = 1.5f;   // 阵亡多久移除
    // protected MyTimer m_RemoveTimer = new MyTimer();
    protected bool m_bCanRemove = false;    // 是否可以移除

    protected ICharacterAI m_AI = null;
    public ICharacterAttr m_Attribute { get; protected set; }


    private StateBar m_StateBar;

    public ICharacter() { }
    public virtual void SetGameObject(GameObject theObj)
    {
        m_GameObject = theObj;
        m_Rigidbody = theObj.GetComponent<Rigidbody>();
        m_Handler = theObj.GetComponent<CharacterHandler>();
        m_Handler.SetCharacter(this);
        m_Input = m_Handler.pi;
        m_Anim = m_Handler.anim;
    }
    public void Killed() => m_bKilled = true;
    public virtual void Update()
    {
        if (m_bKilled)
        {
            m_RemoveTimer -= Time.deltaTime;
            if (m_RemoveTimer <= 0)
                m_bCanRemove = true;
        }
        // if(m_bCanRemove)
        // {
        //     IGameManager.Instance.RemoveEnemy(this as IEnemy);
        //     IFactory.GetObjectPool().PushObject(this.m_GameObject);
        // }
    }
    public virtual void FixedUpdate()
    {

    }
    public void Release()
    {

    }
    public void SetCharacterAttr(ICharacterAttr characterAttr)
    {
        m_Attribute = characterAttr;
        m_Attribute.InitAttr();  // 初始化原始属性
        // m_Name = m_Attribute.
    }

    public bool IsKilled() => m_bKilled;
    public void TryGetHit(ICharacter attacker, Vector3 pos) => m_AI.TryToGetHit(attacker, pos);
    public bool CheckKilledEvent()
    {
        if (m_bCheckKilled)
            return true;
        m_bCheckKilled = true;
        return false;
    }
    public bool CanRemove() => m_bCanRemove;

    public void SetAI(ICharacterAI characterAI)
    {
        m_AI = characterAI;
        Debug.Log(m_AI);
    }
    public void UpdateAI()
    {
        m_AI.Update();
        if (transCoolDown)
        {
            transCoolDownTime -= Time.deltaTime;
            if (transCoolDownTime <= 0)
            {
                transCoolDown = false;
                transCoolDownTime = 0.2f;
            }
        }
        // Debug.Log("speed:"+ GetSpeed());

    }
    public virtual void RemoveAITarget(ICharacter target)
    {
        
    }
    public bool CheckInput(ENUM_INPUT input)
    {
        switch (input)
        {
            case ENUM_INPUT.Null:
                return true;
            case ENUM_INPUT.Idle:
                return m_Input.idle;
            case ENUM_INPUT.Rush:
                return m_Input.rush;
            case ENUM_INPUT.Attack:
                return m_Input.attack;
            case ENUM_INPUT.Jump:
                return m_Input.jump;
            case ENUM_INPUT.Run:
                return m_Input.run;
            case ENUM_INPUT.Walk:
                return m_Input.walk;
            case ENUM_INPUT.Lock:
                return m_Input.lockOn;
            case ENUM_INPUT.HeavyAttack:
                return m_Input.heavyAttack;
            default:
                return true;
        }
    }
    public void LockOrUnlockPlanarVec(bool isLock, bool resetVec)
    {
        if (m_Handler == null) Debug.LogError("没有MHandle");
        if (isLock)
            m_Handler.LockVec(resetVec);
        else
            m_Handler.UnlockVec(resetVec);
    }
    public void SetInputEnable(bool value)
    {
        m_Handler.SetInputEnable(value);
    }
    public void PlayAnimationClip(string clipName, float offsetTime, float duration, int layer, float speed, bool rootMotion, bool mirror)
    {
        // m_Anim.Play(clipName, 0, offsetTime);
        // ForceCrossFade(clipName, duration, layer, offsetTime);
        if (transCoolDown)
        {
            // Debug.Log("冷却中。。。"+clipName);

            return;
        }
        m_Anim.speed = speed;
        m_Anim.SetBool("Mirror", mirror);
        m_Anim.CrossFadeInFixedTime(clipName, duration, layer, offsetTime);
        transCoolDown = true;

        m_Handler.SetRootMotion(rootMotion);
    }
    public void PlayAnimationClip(int clipHash, float offsetTime)
    {
        m_Anim.Play(clipHash, 0, offsetTime);
    }
    public AnimatorStateInfo GetAnimatorStateInfo(int layerIndex)
    {
        return m_Anim.GetCurrentAnimatorStateInfo(layerIndex);
    }
    public float GetCurAnimTime(int layerIndex)
    {
        return m_Anim.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime;
    }
    public AnimatorClipInfo[] GetAnimatorClipInfo(int lalyerIndex)
    {
        return m_Anim.GetCurrentAnimatorClipInfo(lalyerIndex);
    }

    public virtual void UnderAttack(ICharacter attacker, Vector3 pos)
    {

    }

    public void SetThrust(Vector3 vec) => m_Handler.SetThrustVec(vec);
    public void ThrustForward(float value) => m_Handler.ThrustForward(value);
    public bool IsGround => m_Handler.IsGround();
    public bool IsFalling => m_Handler.IsFalling();
    public float SqrSpeed => m_Handler.GetSqrSpeed();
}