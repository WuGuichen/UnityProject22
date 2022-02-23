using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuState : ISceneState
{
    public MainMenuState(SceneStateController controller):base(controller)
    {
        this.StateName = "MainMenuState";
    }
    public override void StateBegin()
    {
        InitUI();
    }
    public override void InitUI()
    {
        IAssetFactory factory = IFactory.GetAssetFactory();
        UISystem UIs = IGameManager.Instance.GetUISystem();
        MainMenuUI ui = UIs.GetMainMenuUI();
        factory.LoadUI("MainMenuUI", ui);
        // Debug.Log("Init");
        
    }
    public override void StateUpdate()
    {
        if(IGameManager.Instance.ThisGameIsStart())
        {
            BattleState state = new BattleState(m_Controller);
            m_Controller.SetState(state,state.SceneName);
        }
    }
    public override void StateEnd()
    {
    }
}
