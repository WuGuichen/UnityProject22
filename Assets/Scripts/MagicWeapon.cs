using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWeapon : IWeapon
{
    public Transform magicPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartDetect()
    {
        GameObject hitEff = IFactory.GetObjectPool().GetPrefabObject("Blood_Arterial_Large_Green", "Others");
        // hitEff.transform.position = magicPos.position;
        hitEff.transform.SetParent(magicPos);
        hitEff.transform.localPosition = Vector3.zero;
        hitEff.transform.forward = (magicPos.position - transform.position).normalized;
        UnityTool.WaitTimeAction(1f, ()=>IFactory.GetObjectPool().PushObject(hitEff));
    }
    public override void StopDetect()
    {
    }
}
