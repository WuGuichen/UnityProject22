using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UITool
{
	private static GameObject m_CanvasObj;
    public static GameObject CanvasObj
    {
        get{
			if(m_CanvasObj == null)
				return UnityTool.FindGameObject("Canvas");
			else
				return m_CanvasObj;
			;}
        private set{m_CanvasObj = value;}
    }  // 场景上的2D画布物体

    public static void ReleaseCanvas()
    {
        m_CanvasObj = null;
    }

    // 寻找Canva下的UI界面
    public static GameObject FindUIGameObject(string UIName)
    {
        if (m_CanvasObj == null)
            m_CanvasObj = UnityTool.FindGameObject("Canvas");
        if (m_CanvasObj == null)
            return null;
        return UnityTool.FindChildGameObject(m_CanvasObj, UIName);
    }

    // 获取UI元件
    public static T GetUIComponent<T>(GameObject Container, string UIName) where T : UnityEngine.Component
    {
        // 找出子物件 
        GameObject ChildGameObject = UnityTool.FindChildGameObject(Container, UIName);
        if (ChildGameObject == null)
            return null;

        T tempObj = ChildGameObject.GetComponent<T>();
        if (tempObj == null)
        {
            Debug.LogWarning("元件[" + UIName + "]不是[" + typeof(T) + "]");
            return null;
        }
        return tempObj;
    }
}
