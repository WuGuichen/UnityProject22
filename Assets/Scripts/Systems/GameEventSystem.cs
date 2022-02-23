using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENUM_GameEvent
{
    Null = 0,
    EnemyKilled = 1,
    NewStage = 2, // 新关卡
}

public class GameEventSystem : IGameSystem
{
    public GameEventSystem(IGameManager game):base(game)
    {
        Initialize();
    }
    public override void Release()
    {

    }
}
