using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo:IPlayer
{
    public PlayerTwo()
    {
        m_Name = "PlayerTwo";
        m_AssetName = "Witch";
        m_AssetLable = "Player";
        m_AttrID = 2;
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