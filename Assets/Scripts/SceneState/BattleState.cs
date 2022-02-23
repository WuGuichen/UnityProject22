using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class BattleState : ISceneState
{
	// int characterNum = 0;
	public BattleState(SceneStateController controller) : base(controller)
	{
		this.StateName = "BattleState";
	}
	public override void StateBegin()
	{
		IGameManager.Instance.Initial();
		InitUI();
	}
	// 初始化UI, 可指定UI管理者
	public override void InitUI()
	{
		IAssetFactory factory = IFactory.GetAssetFactory();
		UISystem UIs = IGameManager.Instance.GetUISystem();
		factory.LoadUI("BaseUI", UIs.GetBaseUI());
		ICharacterFactory charFac = IFactory.GetCharacterFactory();
		IFactory.GetCharacterFactory().CreatePlayer(Vector3.zero, ENUM_Player.Sorcerer);
	}
	public override void StateUpdate()
	{
		IGameManager.Instance.Update();
		// if(Input.GetKeyDown(KeyCode.G))
		// 	IFactory.GetCharacterFactory().CreateEnemy(GameObject.FindGameObjectWithTag("EnemyPoint").transform.position, ENUM_Enemy.Slime);
		// if(Input.GetKeyDown(KeyCode.L))
		// 	IFactory.GetCharacterFactory().CreatePlayer(Vector3.zero, ENUM_Player.Sorcerer);
		// if(Input.GetKeyDown(KeyCode.K))
		// 	IFactory.GetCharacterFactory().CreatePlayer(Vector3.zero, ENUM_Player.Witch);
		InputProcess();
	}
	public override void FixedUpdate()
	{
		IGameManager.Instance.FixedUpdate();
	}
	public override void StateEnd()
	{
		IGameManager.Instance.Release();
	}

	void InputProcess()
	{
		if (IGameManager.Instance.ThisGameIsOver())
		{
			MainMenuState state = new MainMenuState(m_Controller);
			m_Controller.SetState(state, state.SceneName);
		}
	}

}
