 
using UnityEngine;

public class TestRobSceneLoad : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("RobLoad/RobLoadCanvas"));

            go.GetComponent<RobSceneLoad>().TargetSceneName = "TestScene";
        }
    }
}
