using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class IAssetFactory
{
    public Dictionary<string, GameObject> EnemysObj { get; protected set; }
    public Dictionary<string, GameObject> OthersObj { get; protected set; }
    public Dictionary<string, GameObject> PlayersObj { get; protected set; }
    public bool assetsLoaded{get; protected set;}
    protected bool joystickLoaded;
    public virtual GameObject LoadPrefab(string AssetName)
    {
        return null;
    }
    // public abstract void LoadSceneObject();
    // public abstract void LoadPlayer(string AssetName, PlayerBuilder reciver);
    public abstract void LoadUI(string UIName, IUserInterface manager= null);
    public virtual void LoadJoytick(string Name){}
    public abstract GameObject LoadPrefabEnemys(string AssetName);
    public abstract GameObject LoadPrefabOthers(string AssetName);
    public abstract GameObject LoadPrefabPlayers(string AssetName);
    public abstract void AttachSprite(string AssetName, Image image);
    public abstract void SaveAsset(string key, GameObject value, string lable);
    public abstract void AssetLoaded();
}
