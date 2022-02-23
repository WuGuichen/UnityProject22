using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFactory : ICharacterFactory
{
    public override IEnemy CreateEnemy(Vector3 spawnPos, ENUM_Enemy emEnemy)
    {
        EnemyBuildParam enemyParam = new EnemyBuildParam();
        switch (emEnemy)
        {
            case ENUM_Enemy.Slime:
                enemyParam.NewCharacter = new EnemySlime();
                break;
            default:
                Debug.LogWarning("无法建立角色：" + emEnemy);
                break;
        }
        if(enemyParam.NewCharacter == null)
            return null;
        
        enemyParam.SpawnPos = spawnPos;

        EnemyBuilder theEnemyBuilder = new EnemyBuilder();
        theEnemyBuilder.SetBuildParam(enemyParam);
        theEnemyBuilder.Construct();

        

        return enemyParam.NewCharacter as IEnemy;
    }
    public override IPlayer CreatePlayer(Vector3 spawnPos, ENUM_Player emPlayer)
    {
        PlayerBuildParam playerBuildParam = new PlayerBuildParam();
        IPlayer curPlayer = IGameManager.Instance.GetCurPlayer();
        switch (emPlayer)
        {
            case ENUM_Player.Sorcerer:
                if(curPlayer as PlayerOne != null)
                    return curPlayer;
                playerBuildParam.NewCharacter = new PlayerOne();
                break;
            case ENUM_Player.Witch:
                if(curPlayer as PlayerTwo != null)
                    return curPlayer;
                playerBuildParam.NewCharacter = new PlayerTwo();
                break;
            default:
                Debug.LogWarning("无法建立角色：" + emPlayer);
                break;
        }
        
        if(playerBuildParam.NewCharacter == null) return null;

        playerBuildParam.SpawnPos = spawnPos;

        PlayerBuilder thePlayerBuilder = new PlayerBuilder();
        thePlayerBuilder.SetBuildParam(playerBuildParam);
        thePlayerBuilder.Construct();

        // m_BuilderDirector.Construct(thePlayerBuilder);

        return playerBuildParam.NewCharacter as IPlayer;
    }
}
