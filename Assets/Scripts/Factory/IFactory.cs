using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IFactory
{
    private static ICharacterFactory m_CharacterFactory = null;
    private static IAssetFactory m_AssetFactory = null;
    private static ObjectPool m_ObjectPool = null;
    private static IAttrFactory m_AttrFactory = null;

    // 获取将Unity Asset实例化的工厂
    public static IAssetFactory GetAssetFactory()
    {
        if (m_AssetFactory == null)
        {
            m_AssetFactory = new AddressableAssetFactory();
        }
        return m_AssetFactory;
    }
    public static ObjectPool GetObjectPool()
    {
        if (m_ObjectPool == null)
            m_ObjectPool = new ObjectPool();
        return m_ObjectPool;
    }
    public static IAttrFactory GetAttrFactory()
    {
        if(m_AttrFactory == null)
            m_AttrFactory = new AttrFactory();
        return m_AttrFactory;
    }

    // 游戏角色工厂
    public static ICharacterFactory GetCharacterFactory()
    {
        if (m_CharacterFactory == null)
            m_CharacterFactory = new CharacterFactory();
        return m_CharacterFactory;
    }
}
