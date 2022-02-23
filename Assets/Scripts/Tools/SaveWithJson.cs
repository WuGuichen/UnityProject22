using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System;
using System.Text;
using System.IO;

public class SaveWithJson
{
    private static SaveWithJson _instance;
    public static SaveWithJson Instance
    {
        get
        {
            if(_instance == null)
                _instance = new SaveWithJson();
            return _instance;
        }
    }
    public string SaveToJson<T>(T t, string assetSubPath)
    {
        string filePath = Application.persistentDataPath + assetSubPath;
        FileInfo file = new FileInfo(filePath);

        string jsonStr = JsonMapper.ToJson(t);

        using(System.IO.StreamWriter thefile = new StreamWriter(filePath, false, Encoding.UTF8))
        {
            thefile.WriteLine(jsonStr);
        }

#if UNITY_EDITOR
UnityEditor.AssetDatabase.Refresh();
#endif

        return jsonStr;
    }
    
    public T LoadFromJsonFile<T>(string assetSubPath)
    {
        try
        {
            string filePath = Application.persistentDataPath + assetSubPath;

            T t = JsonMapper.ToObject<T>(File.ReadAllText(filePath));
            if(t == null)
            {
                Debug.Log("读取Json为空");
                return default(T);
            }
            return t;
        }catch(Exception e)
        {
            Debug.Log(e);
            return default(T);
        }
    }
}
