using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BaseUI : IUserInterface
{
    // UI元素
    private Button m_HeroInfoBtn = null;
    private Button m_MainMenuBtn = null;
    private Button m_InventoryBtn = null;
    private Button m_HappyBtn = null;
    private Button m_SwitchBtn = null;

    private Text m_MsgTxt = null;
    private Image m_MsgBG = null;

    public BaseUI(IGameManager game) : base(game)
    {
    }
    public override void OnUILoaded(AsyncOperationHandle<GameObject> obj)
    {
        base.OnUILoaded(obj);
        Initialize();
    }
    public override void Initialize()
    {
        m_HeroInfoBtn = GetButton("HeroInfoBtn");
        m_HeroInfoBtn.onClick.AddListener(OnHeroInfoBtnClick);

        m_InventoryBtn = GetButton("InventoryBtn");
        m_InventoryBtn.onClick.AddListener(OnInventoryBtnClick);

        m_HappyBtn = GetButton("HappyUIBtn");
        m_HappyBtn.onClick.AddListener(OnHappyBtnClick);

        m_MainMenuBtn = GetButton("MainMenuBtn");
        // m_MainMenuBtn.onClick.AddListener(OnMainMenuBtnClick);
        m_MainMenuBtn.onClick.AddListener(()=>OnMainMenuBtnClick());

        m_SwitchBtn = GetButton("SwitchBtn");
        m_SwitchBtn.onClick.AddListener(() => OnSwitchBtnClick());

        m_MsgBG = GetImage("MessageBG");
        m_MsgTxt = m_MsgBG.GetComponentInChildren<Text>();
        m_MsgTxt.text = string.Empty;
        m_MsgBG.color = Color.clear;
    }
    public override void Show()
    {
        base.Show();
    }
    public override void Hide()
    {
        base.Hide();
    }
    public override void Update()
    {
    }
    public override void Release()
    {
        base.Release();
    }
    void OnHeroInfoBtnClick()
    {
        PlayerInfoUI theUI = m_UIs.GetPlayerInfoUI();
        if(theUI.IsLoaded()) theUI.Show();
        else IFactory.GetAssetFactory().LoadUI("HeroInfoUI", m_Game.GetUISystem().GetPlayerInfoUI());
    }
    void OnInventoryBtnClick()
    { 
        if(m_Game.GetCurPlayer() == null) return;
        IFactory.GetAssetFactory().LoadJoytick("JoystickPanel");
        // IGameManager.Instance.GetCurPlayer().ChangeInput(null);
    }
    void OnMainMenuBtnClick()
    {
        Debug.Log("结束");
        m_Game.ChangeToMainMenu();
    }
    void OnSwitchBtnClick()
    {
        m_Game.SwitchPlayer();
    }
    void OnHappyBtnClick()
    {
		IFactory.GetCharacterFactory().CreateEnemy(GameObject.FindGameObjectWithTag("EnemyPoint").transform.position, ENUM_Enemy.Slime);
    }
}
