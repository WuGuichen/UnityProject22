using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class MyMono : MonoBehaviour
{
    public Coroutine DestroyWhenComplete(IEnumerator routine, bool persistent)
    {
        if (persistent)
            DontDestroyOnLoad(this.gameObject);
        return StartCoroutine(DestroyObjHandler(routine));
    }
    IEnumerator DestroyObjHandler(IEnumerator routine)
    {
        yield return StartCoroutine(routine);
        Destroy(this.gameObject);
    }
}
// 可能会用到的工具
public static class UnityTool
{
    // private static TaskBehaviour m_Task;
	public static MyMono m_Task;

    //等待
    static public Coroutine WaitTimeAction(float time, UnityAction callback)
    {
        return m_Task.StartCoroutine(Coroutine(time, callback));
    }

    //取消等待
    static public void CancelWait(ref Coroutine coroutine)
    {
        if (coroutine != null)
        {
            m_Task.StopCoroutine(coroutine);
            coroutine = null;
        }
    }

    static IEnumerator Coroutine(float time, UnityAction callback)
    {
        yield return new WaitForSeconds(time);
        if (callback != null)
        {
            callback();
        }
    }

    // 开启协程
    // public static Coroutine StartCoroutine(IEnumerator routine, bool persistent = false)
    // {
    //     GameObject newObj = new GameObject("Coroutine");
    //     GameObject.DontDestroyOnLoad(newObj);
    //     MyMono MonoHelper = newObj.AddComponent<MyMono>();

    //     return MonoHelper.DestroyWhenComplete(routine, persistent);
    // }
    /// <summary>
    /// 字符串转换为任意类型数组
    /// </summary>
    /// <returns>指定类型数组.</returns>
    /// <param name="str">要转换的字符串.</param>
    /// <param name="split">分割字符.</param>
    /// <typeparam name="T">任意类型.</typeparam>
    public static T[] StringToAnyTypeArray<T>(string str, char split)
    {
        if (string.IsNullOrEmpty(str))
            return null;
        str = str.Replace("[", "");
        str = str.Replace("]", "");
        // Debug.Log(str);
        string[] strArray = str.Split(split);
        T[] convertArray = new T[strArray.Length];
        for (int i = 0; i < strArray.Length; i++)
        {
            convertArray[i] = (T)Convert.ChangeType(strArray[i], typeof(T));
        }
        return convertArray;
    }
    // 取得Prefab todo..

    // 附加GameObject
    public static void Attach(GameObject parentObj, GameObject childObj, Vector3 Pos)
    {
        childObj.transform.parent = parentObj.transform;
        childObj.transform.localPosition = Pos;
        childObj.transform.localScale = Vector3.one;
        childObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
    // 找到场景上的物件
    public static GameObject FindGameObject(string objName)
    {
        GameObject tmpGameObj = GameObject.Find(objName);
        if (tmpGameObj == null)
        {
            Debug.LogWarning("场景中没有找到GameObject[" + objName + "]物件");
            return null;
        }
        return tmpGameObj;
    }
    public static Transform DeepFind(Transform parent,string targetName)
    {
        Transform target = parent.Find(targetName);
        //如果找到了直接返回
        if (target != null)
            return target;
        //如果没有没有找到，说明没有在该子层级，则先遍历该层级所有transform，然后通过递归继续查找----再次调用该方法
        for (int i = 0; i < parent.childCount; i++)
        {
            //通过再次调用该方法递归下一层级子物体
            target = DeepFind(parent.GetChild(i), targetName);
 
            if (target!=null)
                return target;
        }
        return target;
    }

    // 取得子物件
    public static GameObject FindChildGameObject(GameObject Container, string objName)
    {
        if (Container == null)
        {
            Debug.LogError("Container = null");
            return null;
        }
        Transform objTF = null;
        if (Container.name == objName)
            objTF = Container.transform;
        else
        {
            Transform[] allChildren = Container.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                if (child.name == objName)
                {
                    if (objTF == null)
                        objTF = child;
                    else
                        Debug.LogWarning("Container[" + Container.name + "]下找到重复元件名称[" + objName + "]");
                }
            }
        }
        // 都没有找到
        if (objTF == null)
        {
            Debug.LogError("元件[" + Container.name + "]找不到子元件[" + objName + "]");
            return null;
        }
        return objTF.gameObject;
    }
}
