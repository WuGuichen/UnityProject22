using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private CapsuleCollider col = null;
    private CharacterHandler handler = null;
    private void Awake()
    {
        col = transform.GetComponent<CapsuleCollider>();
        handler = transform.parent.GetComponent<CharacterHandler>();
        if(col == null) // 没有手动挂好Sensor检测碰撞器
        {  // 自动生成(根据Handle碰撞器)
            col = gameObject.AddComponent<CapsuleCollider>();
            CapsuleCollider handleCol =transform.parent.GetComponent<CapsuleCollider>();
            float offset = 0.05f;
            // Debug.Log("高度：" + handleCol.height + handleCol.transform.name);
            
            col.center = Vector3.up*handleCol.height*(offset+0.5f);
            col.height = handleCol.height*(1-offset*2);
            col.radius = handleCol.radius*(1+offset*2);
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Weapon")
        {
            Debug.Log(handler.name+"被打了");
        }
    }
}
