using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IUserInput
{
    public string KeyA = "left shift";
    public string KeyB = "space";
    public string KeyX = "mouse 0";
    public string KeyY = "h";
    public string KeyLT = "f";
    public string KeyRT = "m";

    // 按钮
    protected MyButton attackBtn;
    protected MyButton jumpBtn;
    protected MyButton rushBtn;
    protected MyButton defenseBtn;
    protected MyButton lockBtn;
    protected MyButton skillBtn;

    protected float targetDup; 
    protected float targetDright; 
    protected float velocityDup; 
    protected float velocityDright; 

    // 相机控制
    public float camUp;
    public float camRight;

    protected override void ButtonSignal()
    {
        jump = jumpBtn.onPressed;
        walk = (Dmag >= 0.1f) && !run;
        attack = attackBtn.onPressed;
        heavyAttack = (attackBtn.isPressing && !attackBtn.IsDelaying);
        lockOn = lockBtn.OnReleased && lockBtn.IsDelaying;
        lockOff = lockBtn.isPressing && !lockBtn.IsDelaying;
        run = (rushBtn.isPressing && !rushBtn.IsDelaying);

        rush = rushBtn.onPressed;
        defense = defenseBtn.onPressed || defenseBtn.isPressing;
        skill = skillBtn.onPressed;

    }
}
