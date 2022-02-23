using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateController:MonoBehaviour
{
    private bool isLoading = false;
    private ISceneState m_State;
    private bool m_bRunBegin = false;
    public SceneStateController()
    {

    }
    public void SetState(ISceneState state, string loadSceneName)
    {
        m_bRunBegin = false;
        // Debug.Log("Set:"+state.SceneName);
        
        LoadScene(loadSceneName);
        if (m_State != null)
            m_State.StateEnd();
        m_State = state;
    }
    private void LoadScene(string loadSceneName)
    {
        if (loadSceneName == null || loadSceneName.Length == 0)
            return;
        // Debug.Log("载入场景....");
        GameObject go = Instantiate(Resources.Load<GameObject>("RobLoad/RobLoadCanvas"));
        RobSceneLoad rob = go.GetComponent<RobSceneLoad>();
        rob.TargetSceneName = loadSceneName;
        rob.CallLoadCompleted += LoadCompleted;
        isLoading = true;
        // Debug.Log("载入"+loadSceneName+"场景成功");
    }
    public void LoadCompleted()
    {
        isLoading = false;

        m_bRunBegin = false;
        // Debug.Log("加载完毕！！！！！");
        
    }
    public void StateUpdate()
    {
        // 是否还在加载
        if(isLoading) return;
        // 通知新的State开始
        if(m_State != null && m_bRunBegin == false)
        {
            m_State.StateBegin();
            m_bRunBegin = true;
        }
        if(m_State!=null)
            m_State.StateUpdate();
    }

    public void StateFixedUpdate()
    {
        // 是否还在加载
        if(isLoading) return;
        if(m_State != null)
            m_State.FixedUpdate();
    }
}