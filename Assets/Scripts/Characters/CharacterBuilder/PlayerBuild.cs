using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerBuildParam : ICharacterBuildeParam
{
}

public class PlayerBuilder : ICharacterBuilder
{
    private PlayerBuildParam m_BuildParam = null;
    public override void SetBuildParam(ICharacterBuildeParam theParam)
    {
        m_BuildParam = theParam as PlayerBuildParam;
    }
    public override void AddAI()
    {
        PlayerAI theAI = new PlayerAI(m_BuildParam.NewCharacter);

        m_BuildParam.NewCharacter.SetAI(theAI);
    }
    public override void AddToCharacterSystem(IGameManager gameManager)
    {
        gameManager.AddPlayer(m_BuildParam.NewCharacter as IPlayer);
    }
    public override void Construct()
    {
        IAssetFactory assetFactory = IFactory.GetAssetFactory();
        GameObject playerHandle = IGameManager.Instance.GetPlayerHandle();
        playerHandle.transform.position = m_BuildParam.SpawnPos;
        GameObject playerObj = IFactory.GetObjectPool().LoadPlayerModle(m_BuildParam.NewCharacter.m_AssetName);
        PlayerHandler ph = playerHandle.GetComponent<PlayerHandler>();

        if (ph == null) // 第一个
        {
            playerHandle.AddComponent<KeyboardController>();
            ph = playerHandle.AddComponent<PlayerHandler>();
            GameObject sensor = UnityTool.FindChildGameObject(playerHandle, "Sensors");
            sensor.AddComponent<OnGroundSensor>();
            sensor.AddComponent<BattleManager>();
        }
        ph.SetModel(m_BuildParam.NewCharacter.m_AssetName, playerObj);
        m_BuildParam.NewCharacter.SetGameObject(playerHandle);

        SetCharacterAttr();
        // 加入AI
        AddAI();

        // 添加到管理器
        AddToCharacterSystem(IGameManager.Instance);
    }
    public override void SetCharacterAttr()
    {
        IAttrFactory theAttrFactory = IFactory.GetAttrFactory();
        PlayerAttr thePlayerAttr = theAttrFactory.GetPlayerAttr(m_BuildParam.NewCharacter.m_AttrID);
        thePlayerAttr.SetAttrStrategy(new PlayerAttrStrategy());
        m_BuildParam.NewCharacter.SetCharacterAttr(thePlayerAttr);
    }
}
