using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne:IPlayer
{
    public PlayerOne()
    {
        m_Name = "PlayerOne";
        m_AssetName = "Sorcerer";
        m_AssetLable = "Player";
        m_AttrID = 1;
    }
    public override void Update()
    {
        base.Update();
        if(Input.GetKeyDown(KeyCode.U))
        {
            Debug.Log(m_Name + m_Attribute.m_MaxHP);
        }
    }

    
}