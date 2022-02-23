using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterBuildeParam
{
    public ICharacter NewCharacter = null;
    public Vector3 SpawnPos;
    // public string AssetName;
}
public abstract class ICharacterBuilder
{
    public abstract void SetBuildParam(ICharacterBuildeParam theParam);
    public abstract void Construct();
    public abstract void AddAI();
    public abstract void SetCharacterAttr();
    // 加入管理器
    public abstract void AddToCharacterSystem(IGameManager gameManager);
}
