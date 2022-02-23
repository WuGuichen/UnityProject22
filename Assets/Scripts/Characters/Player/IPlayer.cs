using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_Player
{
	Null = 0,
	Sorcerer = 1,
	Witch = 2,
}

public abstract class IPlayer : ICharacter
{
	protected PlayerHandler handler;
	protected KeyboardController keyboard;
	protected GameObject joystick;
    private CameraController camCon;
	public IPlayer()
	{
		handler = m_Handler as PlayerHandler;
	}

	public override void SetGameObject(GameObject theObj)
	{
		base.SetGameObject(theObj);
        PlayerHandler ph = m_Handler as PlayerHandler;
        if (ph != null)
        {
            GameObject camHandler = UnityTool.FindChildGameObject(theObj, "CameraHandle");
            camCon = camHandler.transform.GetChild(0).gameObject.AddComponent<CameraController>();
            camCon.UpdateModel(m_Handler.model);
        }
	}

	public override void RemoveAITarget(ICharacter target)
	{
		camCon.RemoveLockedTarget(target.m_GameObject);
	}

	public override void Update()
	{
		// // base.Update();
		// if (Input.GetKeyDown(KeyCode.X))
		// {
		// 	IFactory.GetAssetFactory().LoadJoytick("JoystickPanel");
		// }
	}
	public void ChangeInput(GameObject joys)  // 暂时这样写
	{
		if (m_Input != null) // 关闭该Input
		{
			m_Input.enabled = false;
		}
		PlayerHandler player = m_Handler as PlayerHandler;
		if (joys == null) // 切换到键盘输入
		{
			if (keyboard == null) keyboard = m_Handler.gameObject.AddComponent<KeyboardController>();
            joystick.SetActive(false);
            keyboard.enabled = true;
			m_Input = keyboard;
		    player.SetPlayerInput(m_Input);
			return;
		}
		if (joystick == null)
		{
			joystick = joys;
            joystick.AddComponent<JoystickController>();
		}
        else joystick.SetActive(true);
		m_Input = joystick.GetComponent<JoystickController>();
		player.SetPlayerInput(m_Input);
	}
	public override void UnderAttack(ICharacter attacker, Vector3 pos)
	{
		Debug.Log("I am Hurted");
	}
}
