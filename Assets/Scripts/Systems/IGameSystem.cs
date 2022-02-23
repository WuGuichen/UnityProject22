using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGameSystem
{
    protected IGameManager m_GM = null;
    public IGameSystem(IGameManager game)
    {
        m_GM = game;
    }

    public virtual void Initialize(){}
    public virtual void Release(){}
    public virtual void Update(){}
    public virtual void FixedUpdate(){}
}
