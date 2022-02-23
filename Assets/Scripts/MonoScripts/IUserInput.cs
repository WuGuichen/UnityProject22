using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IUserInput : MonoBehaviour
{
    // protected MyButton btnA;
    // protected MyButton btnB;
    // protected MyButton btnX;
    // protected MyButton btnY;
    public float Dup;
    public float Dright;
    public float Dmag; // 输入信号值
    public Vector3 Dvec; // 速度

    // signal
    public bool walk;
    public bool run;
    public bool rush;
    public bool defense;
    public bool jump;
    public bool attack;
    public bool skill;
    public bool heavyAttack;
    public bool idle;


    public bool lockOn;
    public bool lockOff;


    [Header("Other")]
    public bool inputEnable = true;

    protected Vector2 SquareToCircle(Vector2 input)
    {
        Vector2 output = Vector2.zero;

        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);

        return output;
    }

    public void SetInputEnable(bool value)
    {
        inputEnable = value;
    }

    protected virtual void ButtonSignal()
    {
        // jump = jumpBtn.onPressed;
        // walk = (Dmag >= 0.1f) && !run;
        // attack = attackBtn.onPressed;
        // heavyAttack = (attackBtn.isPressing && !attackBtn.IsDelaying);
        // lockOn = lockBtn.OnReleased && lockBtn.IsDelaying;
        // lockOff = lockBtn.isPressing && !lockBtn.IsDelaying;
        // run = (rushBtn.isPressing && !rushBtn.IsDelaying);

        // rush = rushBtn.onPressed;
        // defense = defenseBtn.onPressed || defenseBtn.isPressing;

    }
}
