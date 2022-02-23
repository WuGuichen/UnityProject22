using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IWeapon : MonoBehaviour
{
    public ICharacter owner;
    public abstract void StartDetect();
    public abstract void StopDetect();
    public void SetOwner(ICharacter theOwner)
    {
        owner = theOwner;
        // print(owner.m_Name);
    }
}
