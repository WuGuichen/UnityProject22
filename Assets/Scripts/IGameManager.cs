using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;

public class IGameManager
{
    #region 单例
    private static IGameManager _instance;
    public static IGameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new IGameManager();
            return _instance;
        }
    }
    #endregion

    public IGameManager() { }

    // 游戏系统
    private GameEventSystem m_GameEventSystem = null;
    private CharacterSystem m_CharacterSystem = null;
    private UISystem m_UISystem = null;

    // 场景状态控制
    private bool m_bGameOver = false;
    private bool m_GameStart = false;
    public void Initial()
    {
        m_bGameOver = false;
        m_GameStart = false;
        m_GameEventSystem = new GameEventSystem(this);
        m_CharacterSystem = new CharacterSystem(this);
    }
    public void Update()
    {
        m_GameEventSystem.Update();
        m_CharacterSystem.Update();
        m_UISystem.Update();
    }
    public void FixedUpdate()
    {
        // m_CharacterSystem.FixedUpdate();
    }
    public void Release()
    {
        m_GameEventSystem.Release();
        m_CharacterSystem.Release();
        m_UISystem.Release();
        IFactory.GetObjectPool().Release();
        (IFactory.GetAssetFactory() as AddressableAssetFactory).Release();
    }

    public bool ThisGameIsOver() => m_bGameOver;
    public bool ThisGameIsStart() => m_GameStart;
    public void ChangeToMainMenu()
    {
        m_bGameOver = true;
    }
    public void SwitchPlayer()
    {
        IPlayer curPlayer = GetCurPlayer();
        Debug.Log("curp " +curPlayer);
        if(curPlayer == null)
		    IFactory.GetCharacterFactory().CreatePlayer(Vector3.zero, ENUM_Player.Sorcerer);
        else
        {
            if(curPlayer as PlayerOne == null)
		        IFactory.GetCharacterFactory().CreatePlayer(curPlayer.m_GameObject.transform.position, ENUM_Player.Sorcerer);
            else IFactory.GetCharacterFactory().CreatePlayer(curPlayer.m_GameObject.transform.position, ENUM_Player.Witch);
        }
    }
    public void EnterBattleState()
    {
        m_GameStart = true;
    }
    public void RegisterUI(GameObject UIObj, IUserInterface UIManager)
    {
        string name = UIObj.name.Replace("(Clone)", string.Empty);
        UIObj.name = name;
        m_UISystem.RegisterUI(UIObj, UIManager);
    }
    public UISystem GetUISystem()
    {
        if (m_UISystem != null)
            return m_UISystem;
        else
        {
            m_UISystem = new UISystem(this);
            return m_UISystem;
        }
    }

    public void NotifyGameEvent(ENUM_GameEvent emGameEvent, System.Object Param)
    {

    }

    public void AddEnemy(IEnemy theEnemy)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.AddEnemy(theEnemy);
    }
    public void AddPlayer(IPlayer thePlayer)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.AddPlayer(thePlayer);
    }
    public void RemoveEnemy(IEnemy theEnemy)
    {
        if (m_CharacterSystem != null)
            m_CharacterSystem.RemoveEnemy(theEnemy);
    }
    // UI加载完成回调
    public void OnUILoaded(AsyncOperationHandle<GameObject> obj)
    {

        if (obj.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("加载UI失败!");
            return;
        }
        GameObject UIObj = GameObject.Instantiate(obj.Result, UITool.CanvasObj.transform);
        string name = UIObj.name.Replace("(Clone)", string.Empty);
        UIObj.name = name;
        // 无需记录的UI
    }
    public void JoystickLoaded(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Failed)
        {
            Debug.LogWarning("加载UI失败!");
            return;
        }
        m_CharacterSystem.GetCurPlayer().ChangeInput(GameObject.Instantiate(obj.Result, UITool.CanvasObj.transform));
    }
    public void BattleStart(ICharacter attacker, ICharacter reciver, Vector3 hitPos)
    {
        Debug.Log("attack:" + attacker.m_Name);
        Debug.Log("reciver:" + reciver.m_Name);
        reciver.TryGetHit(attacker, hitPos);
    }
    public GameObject GetPlayerHandle()
    {
        return m_CharacterSystem.m_PlayerHandle;
    }
    public IPlayer GetCurPlayer()
    {
        return m_CharacterSystem.GetCurPlayer();
    }
}
