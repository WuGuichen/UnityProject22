using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MainMenuUI : IUserInterface
{
    // UI元素
    private Button m_StartBtn = null;
    private Button m_SettingBtn = null;
    private InputField m_InputField;
    private GameObject m_InputText;

    public MainMenuUI(IGameManager game) : base(game)
    {
    }
    public override void OnUILoaded(AsyncOperationHandle<GameObject> obj)
    {
        base.OnUILoaded(obj);
        Initialize();
    }
    public override void Initialize()
    {
        m_StartBtn = GetButton("StartBtn");
        m_StartBtn.onClick.AddListener(OnStartBtnClick);

        m_SettingBtn = GetButton("SettingBtn");
        m_SettingBtn.onClick.AddListener(OnSettingBtnClick);

        m_InputField = UITool.GetUIComponent<InputField>(m_RootUI, "InputField");
        m_InputField.onEndEdit.AddListener(OnInputEnd);
        m_InputField.gameObject.SetActive(false);
    }
    public override void Show()
    {
    }
    public override void Update()
    {
    }
    public override void Release()
    {
        base.Release();
    }
    void OnStartBtnClick()
    {
        m_Game.EnterBattleState();
    }
    void OnInputEnd(string info)
    {
        Debug.Log("End: " + info);
    }
    void OnSettingBtnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
