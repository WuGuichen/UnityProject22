using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : CharacterHandler
{
    // 属性参数
    public float walkSpeed = 2.4f;
    public float runMultify = 3.5f;
    public float turnSpeed = 0.3f;
    public bool lockState = false;

    // 事件
    public delegate void IDelegate();
    public event IDelegate CallChangeInput;
    private void Awake()
    {
        pi = GetComponent<IUserInput>();
        rigid = GetComponent<Rigidbody>();

    }
    public void SetModel(string theName, GameObject _model)
    {
        if (model)
            IFactory.GetObjectPool().PushPlayer(model);
        model = _model;

        anim = model.GetComponent<Animator>();
        RootMotionController rM = anim.GetComponent<RootMotionController>();
        if(rM == null)  // 限制执行次数
        {
            rM = anim.gameObject.AddComponent<RootMotionController>();
            model.AddComponent<PlayerAnimEvent>().enabled = true;
            anim.updateMode = AnimatorUpdateMode.AnimatePhysics;
        }
        rM.Init(this);
    }
    void Start()
    {
        Debug.Log(Screen.width);
    }

    void Update()
    {
        UpdateMyModel();
    }
    void UpdateMyModel()
    {
        if (lockState == false)
        {
            float tarRunMulti = (pi.run ? 2.0f : 1.0f);
            anim.SetFloat("Forward", pi.Dmag * Mathf.Lerp(anim.GetFloat("Forward"), tarRunMulti, turnSpeed));
            anim.SetFloat("Right", 0);

            if (pi.Dmag >= 0.1f && lockPlanarVec == false)
            {
                Vector3 tarForward = Vector3.Slerp(model.transform.forward, pi.Dvec, turnSpeed);
                model.transform.forward = tarForward;
            }
            // Debug.Log("isGound" + isGround);
            // 为物理模拟提供数据
            if (lockPlanarVec == false)  // 否则锁定值
                planarVec = pi.Dmag * model.transform.forward * walkSpeed * (pi.run ? runMultify : 1.0f);
        }
        else
        {
            Vector3 localVec = transform.InverseTransformVector(pi.Dvec); // 转换为本地坐标
            anim.SetFloat("Forward", localVec.z * (pi.run ? runMultify : 1.0f));
            anim.SetFloat("Right", localVec.x * (pi.run ? runMultify : 1.0f));

            if (traceDirector)
                model.transform.forward = planarVec.normalized;
            else
                model.transform.forward = transform.forward;
            if (lockPlanarVec == false)  // 否则锁定值
                planarVec = pi.Dvec * walkSpeed * (pi.run ? runMultify : 1.0f);
        }

    }
    // 物理模拟
    void FixedUpdate()
    {
        if (isRootMotion)
        {
            rigid.position += animDeltaPos;
            animDeltaPos = Vector3.zero;
        }
        else animDeltaPos = Vector3.zero;  // 防止它的值乱来
        rigid.velocity = new Vector3(planarVec.x, rigid.velocity.y, planarVec.z) + thrustVec;
        thrustVec = Vector3.zero;                    // 添加一次后归零(即使得当前帧获得增量)
        if (rigid.velocity.y <= -0.05f)
            isFalling = false;
        else
            isFalling = true;
    }
    public void SetPlayerInput(IUserInput input)
    {
        pi = input;
        CallChangeInput(); // 通知我换Input了
    }
}
