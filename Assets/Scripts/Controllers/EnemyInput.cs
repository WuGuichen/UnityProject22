using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInput : IUserInput
{
    // Start is called before the first frame update
    Transform player = null;
    // private Vector3 disVector;
    private float coolTime = 1f;
    private Vector3 dir;
    // private float m_Mag;
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(attack) attack = false;
        if (coolTime > 0)
            coolTime -= Time.deltaTime;
        else
        {
            coolTime = 0.7f;
            if ((player.position - transform.position).sqrMagnitude <= 8f)
            {
                // UnityTool.WaitTimeAction(1f, () => AttackTarget(player));
                AttackTarget(player);
            }
            else
            {
                WalkToTarget(player);
            }// RunToTarget(player);
        }
        InputSignal();
    }

    void InputSignal()
    {
        Dvec = new Vector3(Dup, 0, Dright);
    }
    void Waiting(Transform target)
    {
        dir = GetTargetDir(target);
        Dup = dir.x*0.01f;
        Dright = dir.y*0.01f;
        Dmag = 0;

        idle = false;
        walk = false;
        run = false;
        attack = false;
        
    }
    Vector3 GetTargetDir(Transform target)
    {
        Vector3 dis = target.position - transform.position;
        dis.y = 0;
        return dis.normalized;
    }
    // void ResetState()
    // {
    //     walk = false;
    //     run = false;
    //     jump = false;
    // }
    protected void WalkToTarget(Transform target)
    {
        dir = GetTargetDir(target);
        Dup = dir.x;
        Dright = dir.z;
        Dmag = 1;

        idle = false;
        walk = true;
        run = false;
    }
    protected void RunToTarget(Transform target)
    {
        dir = GetTargetDir(target);
        Dup = dir.x;
        Dright = dir.z;
        Dmag = 1;

        idle = false;
        walk = false;
        run = true;
    }
    protected void AttackTarget(Transform target)
    {
        dir = GetTargetDir(target);
        Dup = dir.x * 0.5f;
        Dright = dir.z * 0.5f;
        Dmag = 1;
        // Dright = 0;
        // Dmag = 0;

        idle = false;
        walk = false;
        run = false;
        attack = true;
    }
}
