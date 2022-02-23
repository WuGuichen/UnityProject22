using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGroundSensor : MonoBehaviour
{
    public CapsuleCollider capCol;

    private Vector3 point1;
    private Vector3 point2;
    private float radius;
    private float offset = 0.1f;
    private CharacterHandler handler;

    private void Awake()
    {
        capCol = GetComponentInParent<CapsuleCollider>();
        radius = capCol.radius - 0.05f;
        handler = GetComponentInParent<CharacterHandler>();
        handler.SetIsOnGound(true);
    }
    private void FixedUpdate()
    {
        point1 = transform.position + transform.up*(radius-offset);
        point2 = transform.position + transform.up*(capCol.height - offset - radius);
        if(Physics.OverlapCapsule(point1, point2, radius,LayerMask.GetMask("Ground")).Length != 0)
        {
            handler.SetIsOnGound(true);
        }
        else
        {
            handler.SetIsOnGound(false);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
