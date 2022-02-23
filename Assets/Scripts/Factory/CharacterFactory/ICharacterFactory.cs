using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharacterFactory
{
    public abstract IPlayer CreatePlayer(Vector3 spawnPos, ENUM_Player emPlayer);
    public abstract IEnemy CreateEnemy(Vector3 spawnPos, ENUM_Enemy emEnemy);
}
