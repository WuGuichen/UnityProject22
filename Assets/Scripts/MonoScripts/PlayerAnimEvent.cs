using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    private GameObject weaponL = null;
    private GameObject weaponR = null;
    [SerializeField]
    private IWeapon detectorR = null;
    // private Collider colL = null;
    // private Collider colR = null;

    private void Awake()
    {
        weaponL = UnityTool.DeepFind(this.transform,"WeaponHandleL").gameObject;
        weaponR = UnityTool.DeepFind(this.transform,"WeaponHandleR").gameObject;
        detectorR = weaponR.GetComponentInChildren<IWeapon>();
        // Debug.Log(detectorR);
        // colL = weaponL.GetComponentInChildren<Collider>();
        // colR = weaponR.GetComponentInChildren<Collider>();
        // colR.enabled = false;
        // colL.enabled = false;
    }
    void Start()
    {
        detectorR.SetOwner(GetComponentInParent<CharacterHandler>().m_Character);
    }

    void Update()
    {
        
    }
    public void WeaponOn()
    {
        // colR.enabled = true;
        Debug.Log("ONNNNNN" + detectorR);
        detectorR.StartDetect();
    }
    public void WeaponOff()
    {
        // colR.enabled = false;
        detectorR.StopDetect();
    }
}
