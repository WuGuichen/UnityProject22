using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateBar : MonoBehaviour
{
    public Slider HPBar;
    void Start()
    {
        HPBar.value = 1;
    }

    void Update()
    {
        Vector3 relativePos = transform.position - Camera.main.transform.position;
        // Quaternion rotation = 
        this.transform.rotation = Quaternion.LookRotation(relativePos);
    }

    public void SetHPBarValue(float value)
    {
        HPBar.value = value;
    }
}
