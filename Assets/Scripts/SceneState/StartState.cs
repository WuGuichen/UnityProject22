using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 是起始场景，不要回到这个场景！！
public class StartState : ISceneState
{
    public StartState(SceneStateController Controller) : base(Controller)
    {
        this.StateName = "StartState";
    }
    public override void StateBegin()
    {
        MainMenuState state = new MainMenuState(m_Controller);
        m_Controller.SetState(state,state.SceneName);
        Debug.Log("Start");
        
    }

    public override void StateUpdate()
    {
    }
}
