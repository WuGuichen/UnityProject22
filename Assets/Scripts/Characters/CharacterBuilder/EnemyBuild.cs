using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemyBuildParam : ICharacterBuildeParam
{
}

public class EnemyBuilder:ICharacterBuilder
{
    private EnemyBuildParam m_BuildParam = null;
    public override void SetBuildParam(ICharacterBuildeParam theParam)
    {
        m_BuildParam = theParam as EnemyBuildParam;
    }
    public override void AddAI()
    {
        EnemyAI theAI = new EnemyAI(m_BuildParam.NewCharacter);
        m_BuildParam.NewCharacter.SetAI(theAI);
    }
    public override void AddToCharacterSystem(IGameManager gameManager)
    {
        gameManager.AddEnemy(m_BuildParam.NewCharacter as IEnemy);
    }
    public override void Construct()
    {
        LoadAsset();
        SetCharacterAttr();
        AddAI();
        
        AddToCharacterSystem(IGameManager.Instance);
    }
    private void LoadAsset()
    {
        IAssetFactory assetFactory = IFactory.GetAssetFactory();
        ObjectPool objectPool = IFactory.GetObjectPool();
        GameObject enemyObj = objectPool.GetPrefabObject(m_BuildParam.NewCharacter.m_AssetName, m_BuildParam.NewCharacter.m_AssetLable);
        enemyObj.transform.position = m_BuildParam.SpawnPos;
        enemyObj.transform.rotation = Quaternion.identity;
        enemyObj.AddComponent<EnemyInput>();
        enemyObj.AddComponent<EnemyHandler>();
        GameObject sensor = UnityTool.FindChildGameObject(enemyObj, "Sensor");
        sensor.AddComponent<BattleManager>();
        m_BuildParam.NewCharacter.SetGameObject(enemyObj);
    }
    public override void SetCharacterAttr()
    {
        IAttrFactory theAttrFactory = IFactory.GetAttrFactory();
        EnemyAttr theEnemyAttr = theAttrFactory.GetEnemyAttr(m_BuildParam.NewCharacter.m_AttrID);
        theEnemyAttr.SetAttrStrategy(new EnemyAttrStrategy());
        m_BuildParam.NewCharacter.SetCharacterAttr(theEnemyAttr);
    }
}