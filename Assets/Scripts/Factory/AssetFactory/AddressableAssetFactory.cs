using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableAssetFactory : IAssetFactory
{
    public AddressableAssetFactory()
    {
        EnemysObj = new Dictionary<string, GameObject>();
        OthersObj = new Dictionary<string, GameObject>();
        PlayersObj = new Dictionary<string, GameObject>();
    }
    public override void LoadUI(string UIName, IUserInterface manager)
    {
        if (manager != null)
        {
            Addressables.LoadAssetAsync<GameObject>(UIName).Completed += manager.OnUILoaded;
        }
        else
            Addressables.LoadAssetAsync<GameObject>(UIName).Completed += IGameManager.Instance.OnUILoaded;
    }
    public override void LoadJoytick(string Name)
    {
        if(joystickLoaded == false)
            Addressables.LoadAssetAsync<GameObject>(Name).Completed += IGameManager.Instance.JoystickLoaded;
        joystickLoaded = true;
    }
    public override GameObject LoadPrefabPlayers(string AssetName)
    {
        if(PlayersObj.ContainsKey(AssetName))
            return PlayersObj[AssetName];
        Debug.Log("没有Player预制体" + AssetName);
        return null;
    }

    public override GameObject LoadPrefabEnemys(string AssetName)
    {
        if (EnemysObj.ContainsKey(AssetName))
            return GameObject.Instantiate(EnemysObj[AssetName]);
        Debug.Log("没有Enemy预制体" + AssetName);
        return null;
    }

    public override GameObject LoadPrefabOthers(string AssetName)
    {
        if (OthersObj.ContainsKey(AssetName))
            return GameObject.Instantiate(OthersObj[AssetName]);
        else
            Debug.Log("没有Others预制体" + AssetName);
        return null;
    }
    public override void AttachSprite(string spriteName, Image image)
    {
        Addressables.LoadAssetAsync<Sprite>(spriteName).Completed += (obj) =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Sprite ob = obj.Result;
                image.sprite = ob;
            }
            else if (obj.Status == AsyncOperationStatus.Failed)
            {
                Addressables.LoadAssetAsync<Sprite>("Default").Completed += (obj) =>
                {
                    Sprite ob = obj.Result;
                    image.sprite = ob;
                };
                return;
            }
        };
    }
    public override void SaveAsset(string key, GameObject value, string lable)
    {
        if(lable == "Enemys")
            EnemysObj.Add(key, value);
        else if(lable == "Others")
            OthersObj.Add(key, value);
        else if(lable == "Players")
            PlayersObj.Add(key, value);
        else
            Debug.Log("未识别lable:" +lable);
    }
    public override void AssetLoaded()
    {
        assetsLoaded = true;
    }

    // public override void LoadSceneObject()
    // {
    //     Addressables.LoadAssetAsync<GameObject>("PlayerHandle").Completed += (obj)=>
    //     {
    //         GameObject res = GameObject.Instantiate(obj.Result);
    //     };
    // }
    public void Release()
    {
        joystickLoaded = false;
    }
}
