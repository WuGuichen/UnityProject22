using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ISceneState
{
    private string m_StateName = "ISceneState";
    protected SceneStateController m_Controller = null;
    public ISceneState(SceneStateController controller)
    {
        m_Controller = controller;
    }
    public string StateName{
        get{return m_StateName;}
        set{m_StateName = value;}
    }
    public string SceneName{
        get{return m_StateName.Replace("State", "Scene");}
    }
    // ========= 三个接口方法： =============

    /// <summary>
    /// 开始:场景转换成功后通知类对象。可以实现场景中资源加载和游戏参数配置等。
    /// </summary>
    public virtual void StateBegin() { }
    public virtual void InitUI(){}

    /// <summary>
    /// 结束：场景将要被释放时通知类对象。可以释放游戏资源，或重新设置游戏场景状态。
    /// </summary>
    public virtual void StateEnd() { }

    /// <summary>
    /// 更新：游戏定时更新时通知类对象。可以调用U3D“定时更新功能”，同时让其他游戏系统也定期更新。可以让游戏系统类不必继承MonoBehaviour。
    /// </summary>
    public virtual void StateUpdate() { }
    public virtual void FixedUpdate(){}

    // =====================================
}
