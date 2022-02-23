using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class UnityMonoTool : MonoBehaviour
{
    public static UnityMonoTool Instance;
    private void Awake()
    {
        Instance = this;
    }
    // 延时事件流
    public static Dictionary<Action, float> actionDic = new Dictionary<Action, float>();
    public void RigisterAction(Action action, float time)
    {
        actionDic.Add(action, time);
    }
    public void ClearAcions()
    {
        actionDic.Clear();
    }
    public void CallActions()
    {
        List<Action> theActionList = new List<Action>(actionDic.Keys);
        List<float> theTimeList = new List<float>(actionDic.Values);
        theActionList[0]();
        for (int i = 1; i < theActionList.Count; i++)
        {
            StartCoroutine(DelayToCallEnumerator(theActionList[i], theTimeList[i]));
        }
    }
    public void CallActions(bool isClear)
    {
        CallActions();
        if (isClear)
        {
            ClearAcions();
        }
    }
    float Sum(List<float> list, int index)
    {
        float x = 0;
        for (int i = 0; i < list.Count; i++)
        {
            if (i <= index)
            {
                x += list[i];
            }
        }
        return x;
    }
    public void DelayToCall(Action action, float time)
    {
        StartCoroutine(DelayToCallEnumerator(action, time));
    }
    IEnumerator DelayToCallEnumerator(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        action();
    }
    // public Coroutine MyStartCoroutine(Action routine, bool persistent = false)
    // {
    //     StartCoroutine();
    // }
}
