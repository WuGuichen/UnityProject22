using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UISystem : IGameSystem
{
    private BaseUI m_BaseUI = null;
    private MainMenuUI m_MainMenuUI = null;
    private PlayerInfoUI m_PlayerInfoUI = null;

    public UISystem(IGameManager game) : base(game)
    {
        Initialize();
    }
    public override void Initialize()
    {
    }

    public void RegisterUI(GameObject UIObj, IUserInterface UIManager)
    {

    }
    public BaseUI GetBaseUI()
    {
        if (m_BaseUI != null)
            return m_BaseUI;
        m_BaseUI = new BaseUI(m_GM);
        return m_BaseUI;
    }
    public MainMenuUI GetMainMenuUI()
    {
        if (m_MainMenuUI != null)
            return m_MainMenuUI;
        m_MainMenuUI = new MainMenuUI(m_GM);
        return m_MainMenuUI;
    }
    public PlayerInfoUI GetPlayerInfoUI()
    {
        if (m_PlayerInfoUI != null)
            return m_PlayerInfoUI;
        m_PlayerInfoUI = new PlayerInfoUI(m_GM);
        return m_PlayerInfoUI;
    }
    public override void Release()
    {
        if (m_BaseUI != null) m_BaseUI.Release();
        if (m_MainMenuUI != null) m_MainMenuUI.Release();
        if (m_PlayerInfoUI != null) m_PlayerInfoUI.Release();
    }
}