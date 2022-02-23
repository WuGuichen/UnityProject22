using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    SceneStateController m_SceneStateController = null;

    private void Awake()
    {
        Debug.Log("Awake");

        GameObject.DontDestroyOnLoad(this.gameObject);
        GameObject sceneStateControllerObj = new GameObject("SceneStateControll");
        m_SceneStateController = sceneStateControllerObj.AddComponent<SceneStateController>();
        UnityTool.m_Task = sceneStateControllerObj.AddComponent<MyMono>();  // 帮UnityTool挂载
        GameObject.DontDestroyOnLoad(sceneStateControllerObj);
        // UnityTool.WaitTimeAction(5f, () => { Debug.Log("Hello"); });
        UnityEngine.Random.InitState((int)System.DateTime.Now.Ticks);
    }
    private void Start()
    {
        m_SceneStateController.SetState(new StartState(m_SceneStateController), "");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            GameObject tt = IFactory.GetObjectPool().GetPrefabObject("DamageText", "Others");
            tt.transform.position = Vector3.zero;
        }
        m_SceneStateController.StateUpdate();
    }
    private void FixedUpdate()
    {
        m_SceneStateController.StateFixedUpdate();
    }
}
