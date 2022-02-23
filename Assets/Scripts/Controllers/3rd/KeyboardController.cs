using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class KeyboardController : PlayerInput
{
    [Header("------- Keys Setting -------")]
    private string keyUp = "w";
    private string keyDown = "s";
    private string keyLeft = "a";
    private string keyRight = "d";


    private void Awake()
    {
        attackBtn = new MyButton();
        jumpBtn = new MyButton();
        rushBtn = new MyButton();
        defenseBtn = new MyButton();
        lockBtn = new MyButton();
        skillBtn = new MyButton();
    }
    private void Start()
    {
        HideCursor();
    }
    void TickBtn()
    {
        // btnB.Tick(Input.GetKey(KeyB));
        attackBtn.Tick(Input.GetKey(KeyX));
        jumpBtn.Tick(Input.GetKey(KeyB));
        // rushBtn.Tick(Input.GetKey(KeyA));
        rushBtn.Tick(Input.GetKey("mouse 1"));
        defenseBtn.Tick(Input.GetKey(KeyY));
        lockBtn.Tick(Input.GetKey(KeyLT));
        skillBtn.Tick(Input.GetKey(KeyRT));
    }
    void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;//锁定指针到视图中心
        Cursor.visible = false;//隐藏指针
    }

    private void Update()
    {
        // 摄像机控制
        Cursor.visible = Input.GetKey(KeyCode.LeftAlt);
        if (Cursor.visible)
        {
            Cursor.lockState = CursorLockMode.None;
            camUp = 0;
            camRight = 0;
        }
        else
        {
            TickBtn();
            Cursor.lockState = CursorLockMode.Locked;//锁定指针到视图中心
            camUp = Input.GetAxis("Mouse Y")*3.0f;
            camRight = Input.GetAxis("Mouse X")*2.5f;
        }
        // if (Input.anyKeyDown)
        // {
        //     foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
        //     {
        //         if (Input.GetKeyDown(keyCode))
        //         {
        //             Debug.Log("按下:"+keyCode.ToString());
        //         }
        //     }
        // }
        InputSignal();
        ButtonSignal();
        // run = Input.GetKey(KeyA);
        // walk = (Dmag>=0.1f)&&!run;
        // camUp = Input.GetAxis("CamUp");
        // camRight = Input.GetAxis("CamRight");
    }

    void InputSignal()
    {
        targetDup = (Input.GetKey(keyUp) ? 1.0f : 0) - (Input.GetKey(keyDown) ? 1.0f : 0);
        targetDright = (Input.GetKey(keyRight) ? 1.0f : 0) - (Input.GetKey(keyLeft) ? 1.0f : 0);
        if (inputEnable == false) // 使输入值归零
        {
            // targetDright = 0;
            // targetDup = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);   // 1.0是maxspeed
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);   // 1.0是maxspeed

        Vector2 fixedD = SquareToCircle(new Vector2(Dright, Dup));

        Dmag = Mathf.Sqrt((fixedD.y * fixedD.y) + (fixedD.x * fixedD.x));
        Dvec = transform.right * fixedD.x + transform.forward * fixedD.y;

        // jump = btnB.onPressed;
        
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
