using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalAttr
{
    public string m_Name{get; private set;}
    public AdditionalAttr(string name)
    {
        m_Name = name;
    }
}

public class BaseAttrDecorator: BaseAttr
{
    protected BaseAttr m_Component;   // 被装饰的对象
    protected AdditionalAttr m_AdditionalAttr;  // 额外的数值

    // 设定装饰目标
    public void SetComponent(BaseAttr theComponent)
    {
        m_Component = theComponent;
    }

    // 设置额外的值
    public void SetAdditionalAttr(AdditionalAttr theAddition)
    {
        m_AdditionalAttr = theAddition;
    }
    public override string GetAttrName()
    {
        return m_Component.GetAttrName();
    }
}

public enum ENUM_AttrDecorator
{
    Prefix,
    Suffix,
}

// 前缀
public class PrefixBaseAttr: BaseAttrDecorator
{
    public override string GetAttrName()
    {
        return m_AdditionalAttr.m_Name + m_Component.GetAttrName();
    }
}

// 后缀
public class SuffixBaseAttr: BaseAttrDecorator
{
    public override string GetAttrName()
    {
        return m_Component.GetAttrName() + m_AdditionalAttr.m_Name;
    }
}

// 直接强化
public class StengthenBaseAttr:BaseAttrDecorator
{
    protected List<AdditionalAttr> m_Additions;   // 多个强化数值

    public override string GetAttrName()
    {
        string name = m_Component.GetAttrName();
        foreach(AdditionalAttr theAttr in m_Additions)
            name += theAttr.m_Name;
        return name;
    }
}