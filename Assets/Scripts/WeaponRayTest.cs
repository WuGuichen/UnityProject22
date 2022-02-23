using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRayTest : IWeapon
{
    /*
     * 
     * 近战攻击射线检测
     * 
     */
    public Transform pointA;
    public Transform pointB;
    public float lenRay;
    [SerializeField]
    private LayerMask layer;
    // public GameObject particle;//粒子效果
    public Transform[] points; //射线发射点
    public Dictionary<int, Vector3> dic_lastPoints = new Dictionary<int, Vector3>(); //存放上个位置信息
    public bool detect = false;

    private List<Collider> hitTars = new List<Collider>();

    private void Start()
    {
        if (dic_lastPoints.Count == 0)
        {
            for (int i = 0; i < points.Length; i++)
            {
                dic_lastPoints.Add(points[i].GetHashCode(), points[i].position);
            }
        }
        lenRay = Vector3.Distance(pointA.position, pointB.position);
        layer = 1 << LayerMask.NameToLayer("Sensor");
        // Debug.Log("layer:" + layer.value);
        // Debug.Log(this + "Start");

    }
    private void LateUpdate()
    {
        if (detect)
            Casting();
    }
    private void UpdatePoints()
    {
        for (int i = 0; i < points.Length; i++)
        {
            dic_lastPoints[points[i].GetHashCode()] = points[i].position;
        }
    }
    public override void StartDetect()
    {
        UpdatePoints();
        detect = true;
        // ignoreFirst = true;
    }
    public override void StopDetect()
    {
        detect = false;
        hitTars.Clear();
    }

    // 双重检测
    void Casting()
    {
        Transform nowPosA = pointA;
        Debug.DrawRay(pointA.position, pointB.position - pointA.position, Color.blue, 1f);

        Ray ray = new Ray(pointA.position, pointB.position - pointA.position);
        RaycastHit[] raycastHits = new RaycastHit[6];
        Physics.RaycastNonAlloc(ray, raycastHits, lenRay, layer, QueryTriggerInteraction.Ignore);
        DetectTargets(raycastHits);
        for (int i = 0; i < points.Length; i++)
        {
            var nowPos = points[i];
            dic_lastPoints.TryGetValue(nowPos.GetHashCode(), out Vector3 lastPos);
            //Debug.DrawLine(nowPos.position, lastPos, Color.blue, 1f); ;
            float theLen = (lastPos - nowPos.position).sqrMagnitude;
            if (theLen < 0.25f) theLen *= 2;
            else if (theLen < 0.5f) theLen *= 1.5f;
            else if (theLen > 1.5f) theLen = 1f;
            // else if (theLen > 2f) theLen = 1.5f;
            Debug.DrawRay(lastPos, (nowPos.position - lastPos).normalized * theLen, Color.black, 1f);

            Ray ray1 = new Ray(lastPos, nowPos.position - lastPos);
            RaycastHit[] raycastHits1 = new RaycastHit[6];
            Physics.RaycastNonAlloc(ray1, raycastHits1, theLen, layer, QueryTriggerInteraction.Ignore);
            DetectTargets(raycastHits1);
            if (nowPos.position != lastPos)
            {
                dic_lastPoints[nowPos.GetHashCode()] = nowPos.position;//存入上个位置信息
            }
        }
    }
    private void DetectTargets(RaycastHit[] raycastHits)
    {
        foreach (RaycastHit item in raycastHits)
        {
            if (item.collider == null) continue;
            //下面做击中后的一些判断和处理
            //比如扣血之类的,
            // 需要注意:在同一帧会多次击中一个对象
            // Debug.Log("item:" + item.collider.name);

            if (hitTars.Contains(item.collider))
            {
                return;
            }
            else
            {
                hitTars.Add(item.collider);
                CharacterHandler reciver = item.collider.GetComponentInParent<CharacterHandler>();
                if (reciver)
                {
                    Debug.Log(owner);
                    IGameManager.Instance.BattleStart(owner, reciver.m_Character, item.point);
                }
            }

            // if (particle)
            // {
            //     var go = Instantiate(particle, item.point, Quaternion.identity);
            //     Destroy(go, 3f);
            // }
            break;
        }
    }

    // private void OnGUI()
    // { 
    //     var labelstyle = new GUIStyle();
    //     labelstyle.fontSize = 32;
    //     labelstyle.normal.textColor = Color.red;
    //     int height = 40; 
    //     GUIContent[] contents = new GUIContent[]
    //     {
    //            new GUIContent($"hitCount:{hitCount}"),  
    //            new GUIContent($"frameCount:{Time.frameCount }"),
    //      };

    //     for (int i = 0; i < contents.Length; i++)
    //     {
    //         GUI.Label(new Rect(0, height * i, 180, 80), contents[i], labelstyle);
    //     }
    // } 
}