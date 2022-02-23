using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IButtonEvent : MonoBehaviour, IPointerDownHandler,IPointerUpHandler, IPointerExitHandler
{
    public MyButton thisBtn = new MyButton();
    bool isDown = false;

    void Update()
    {
        thisBtn.Tick(isDown);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isDown = true;
    }
    
    public void OnPointerUp(PointerEventData eventData) {
        isDown = false;
    }
 
    public void OnPointerExit(PointerEventData eventData) {
        isDown = false;
    }
}
