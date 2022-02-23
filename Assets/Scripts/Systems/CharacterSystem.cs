using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : IGameSystem
{
	private List<ICharacter> m_Enemys = new List<ICharacter>();
	// private List<ICharacter> m_Players = new List<ICharacter>();
	private IPlayer curPlayer = null;
	public GameObject m_PlayerHandle { get; private set; }
	public CharacterSystem(IGameManager game) : base(game)
	{
		Initialize();

	}
	public override void Initialize()
	{
		m_PlayerHandle = GameObject.FindGameObjectWithTag("Player");
		Debug.Log(m_PlayerHandle);
	}

	public void AddEnemy(IEnemy theEnemy)
	{
		m_Enemys.Add(theEnemy);
	}
	public void RemoveEnemy(IEnemy theEnemy)
	{
		m_Enemys.Remove(theEnemy);
	}
	public void AddPlayer(IPlayer thePlayer)
	{
		// m_Players.Add(thePlayer);
		curPlayer = thePlayer;
		Debug.Log("替换角色" + thePlayer.ToString());
		// IFactory.GetAssetFactory().LoadJoytick("JoystickPanel");

	}
	public IPlayer GetCurPlayer()
	{
		return curPlayer;
	}
	// public void RemovePlayer(IPlayer thePlayer)
	// {
	//     m_Players.Remove(thePlayer);
	// }
	public void RemoveCharacter()
	{
		RemoveCharacter(m_Enemys);
	}
	public void RemoveCharacter(List<ICharacter> characters)
	{
		List<ICharacter> canRemoves = new List<ICharacter>();
		ObjectPool pool = IFactory.GetObjectPool();
		foreach (ICharacter character in characters)
		{
			if (character.CanRemove())
				canRemoves.Add(character);
		}
		foreach (ICharacter canRemove in canRemoves)
		{
			canRemove.Release();
			characters.Remove(canRemove);
			curPlayer.RemoveAITarget(canRemove);
			pool.PushObject(canRemove.m_GameObject);
		}
	}
	public void RemoveCharacter(List<ICharacter> characters, List<ICharacter> opponents, ENUM_GameEvent emEvent)
	{
		List<ICharacter> canRemoves = new List<ICharacter>();
		foreach (ICharacter character in characters)
		{
			// // 是否阵亡
			// if (character.IsKilled() == false)
			// 	continue;

			// // 是否确实阵亡事件
			// if (character.CheckKilledEvent() == false)
			// 	m_GM.NotifyGameEvent(emEvent, character);

			if (character.CanRemove())
				canRemoves.Add(character);
		}
		foreach (ICharacter canRemove in canRemoves)
		{
			// // 通知对手移除目标
			// foreach (ICharacter opponent in opponents)
			// 	opponent.RemoveAITarget(canRemove);

			// 释放资源并移除
			canRemove.Release();
			characters.Remove(canRemove);
		}
	}

	public override void Update()
	{
		UpdateCharacter();
		UpdateAI();

		RemoveCharacter();
	}
	public override void FixedUpdate()
	{
	}

	void UpdateCharacter()
	{
		foreach (ICharacter character in m_Enemys)
			character.Update();
		// foreach(ICharacter character in m_Players) 
		//     character.Update();
		if (curPlayer != null)
			curPlayer.Update();
	}
	void UpdateAI()
	{
		// UpdateAI(m_Players);
		if (curPlayer != null)
			curPlayer.UpdateAI();
		UpdateAI(m_Enemys);
	}
	void UpdateAI(List<ICharacter> characters)
	{
		foreach (ICharacter character in characters)
			character.UpdateAI();
	}
	public override void Release()
	{
		m_Enemys.Clear();
		// m_Players.Clear();
	}
}
