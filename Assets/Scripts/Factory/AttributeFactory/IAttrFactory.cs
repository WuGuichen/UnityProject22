using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAttrFactory
{
    public abstract PlayerAttr GetPlayerAttr(int playerID);
    public abstract EnemyAttr GetEnemyAttr(int enemyID);
}
