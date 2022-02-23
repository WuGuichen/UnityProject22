using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickHandle : IUserInput, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Transform handleTrans;
    private Vector2 pointDownPos;
    private float maxRadius = 150f;
    public Vector2 dir{get; private set;}
    private void Awake()
    {
        handleTrans = transform.GetChild(0).GetChild(0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        pointDownPos = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dis = eventData.position - pointDownPos;
        // float clamp = Mathf.Clamp(dis.sqrMagnitude, 0f, maxRadius*maxRadius);
        float clampDis = dis.magnitude;
        if(clampDis <= 0) clampDis = 0;
        if(clampDis >= maxRadius) clampDis = maxRadius;
        dir = dis.normalized;
        handleTrans.localPosition = clampDis*dir;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handleTrans.localPosition = Vector2.zero;
        dir = Vector2.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
