using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerInfoUI : IUserInterface
{
    // UI元素
    private Button m_CloseBtn = null;

    public PlayerInfoUI(IGameManager game) : base(game)
    {
    }
    public override void OnUILoaded(AsyncOperationHandle<GameObject> obj)
    {
        base.OnUILoaded(obj);
        Initialize();
    }
    public override void Initialize()
    {
        m_CloseBtn = GetButton("CloseBtn");
        m_CloseBtn.onClick.AddListener(OnCloseBtnClick);
    }
    public override void Update()
    {
    }
    void OnCloseBtnClick()
    {
        Hide();
    }
    void OnSettingBtnClick()
    {

    }
}
