using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public abstract class IUserInterface
{
    protected IGameManager m_Game = null;
    private UISystem m_UISystem = null;
    protected UISystem m_UIs
    {
        get{
            if(m_UISystem!=null)
                return m_UISystem;
            else
            {
                m_UISystem = m_Game.GetUISystem();
                return m_UISystem;
            }
        }
    }
    protected GameObject m_RootUI = null;
    private bool m_bActive = true;
    protected bool m_bLoaded = false;
    public IUserInterface(IGameManager game)
    {
        m_Game = game;
        m_bLoaded = false;
    }
    public bool IsLoaded()
    {
        return m_bLoaded;
    }
    // UI加载完成回调
    public virtual void OnUILoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Failed)
		{
			Debug.LogWarning("加载UI失败!");
			return;
		}
        m_RootUI = GameObject.Instantiate( obj.Result,UITool.CanvasObj.transform);
        m_Game.RegisterUI(m_RootUI, this);
        m_bLoaded = true; // 加载过资源的记号
    }
    public bool IsVisible()
    {
        return m_bActive;
    }
    public virtual void Show()
    {
        if (m_bActive == true) return;
        m_RootUI.SetActive(true);
        m_bActive = true;
        // Time.timeScale = 0;
    }
    public virtual void Hide()
    {
        m_RootUI.SetActive(false);
        m_bActive = false;
    }
    public virtual void Initialize() { }
    public virtual void Release() {
        m_bLoaded = false;
     }
    public virtual void Update() { }
    protected Button GetButton(string btnName)
    {
        return UITool.GetUIComponent<Button>(m_RootUI, btnName);
    }
    protected Image GetImage(string imgName)
    {
        return UITool.GetUIComponent<Image>(m_RootUI,imgName);
    }
    protected GameObject GetGrid(string gridName)
    {
        return UITool.GetUIComponent<GridLayoutGroup>(m_RootUI, gridName).gameObject;
    }
    protected Text GetText(string txtName)
    {
        return UITool.GetUIComponent<Text>(m_RootUI, txtName);
    }
}
