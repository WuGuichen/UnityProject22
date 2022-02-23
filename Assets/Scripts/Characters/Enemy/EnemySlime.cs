using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySlime : IEnemy
{
    public EnemySlime()
    {
        m_AssetLable = "Enemys";
        m_Name = "Slime";
        m_AssetName = "Enemy_Slime";
        m_AttrID = 100;
        hitEffectName = "Blood_Arterial_Large_Green";
    }
    public override void Update()
    {
        base.Update();
    }
    public override void DoShowHitEffect(Vector3 pos)
    {
        GameObject hitEff = IFactory.GetObjectPool().GetPrefabObject(hitEffectName, "Others");
        UnityTool.WaitTimeAction(10f, ()=>IFactory.GetObjectPool().PushObject(hitEff));

        Vector3 dir = pos - m_Handler.transform.position;
        hitEff.transform.SetParent(m_GameObject.transform);
        hitEff.transform.position = pos-dir*0.5f;
        hitEff.transform.forward = dir.normalized;
    }
    public override void DoShowDamageValue(int value)
    {
        GameObject dmgTxt = IFactory.GetObjectPool().GetPrefabObject("DamageText", "Others");
        dmgTxt.transform.localScale = Vector3.one;
        dmgTxt.transform.position = m_Handler.transform.position;
        dmgTxt.GetComponent<DamageText>().m_DmgText.text = value.ToString() + "!!";
        float hpRate = m_Attribute.m_CurHP/m_Attribute.m_MaxHP;
        m_StateBar.gameObject.SetActive(true);
        m_StateBar.SetHPBarValue(hpRate);
    }
}