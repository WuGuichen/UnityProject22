using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
// using UnityEngine.InputSystem.Layouts;
// using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Serialization;

public class JoystickController : PlayerInput, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField]
    private Transform handleTrans;
    private Transform Handler;
    private Vector2 pointDownPos;
    private float maxRadius;
    private float reciMaxRadius;
    private float sqrMaxRadius;
    [SerializeField]
    private GameObject area;

    [SerializeField]
    private Image handleImg;
    [SerializeField]
    private Image joystickImg;

    private Rect rect;
    public Vector2 dir;
    public Vector2 lastDir = new Vector2(0, 1);



    private void Awake()
    {
        Handler = GameObject.FindGameObjectWithTag("Player").transform;

        area = UnityTool.FindChildGameObject(this.gameObject, "JoystickArea");
        joystickImg = UnityTool.FindChildGameObject(area.gameObject, "Joystick").GetComponent<Image>();
        handleTrans = joystickImg.transform.GetChild(0);
        handleImg = handleTrans.GetComponent<Image>();

        attackBtn = UnityTool.FindChildGameObject(this.gameObject, "AttackBtn").GetComponent<IButtonEvent>().thisBtn;
        jumpBtn = UnityTool.FindChildGameObject(this.gameObject, "JumpBtn").GetComponent<IButtonEvent>().thisBtn;
        rushBtn = UnityTool.FindChildGameObject(this.gameObject, "RushBtn").GetComponent<IButtonEvent>().thisBtn;
        defenseBtn = UnityTool.FindChildGameObject(this.gameObject, "DefenseBtn").GetComponent<IButtonEvent>().thisBtn;
        lockBtn = UnityTool.FindChildGameObject(this.gameObject, "LockBtn").GetComponent<IButtonEvent>().thisBtn;
        skillBtn = UnityTool.FindChildGameObject(this.gameObject, "SkillBtn").GetComponent<IButtonEvent>().thisBtn;
    }
    private void Start()
    {
        rect = joystickImg.GetComponent<RectTransform>().rect;
        Vector3 size = joystickImg.transform.TransformVector(rect.width, rect.height, 0);
        maxRadius = size.x *0.5f;
        reciMaxRadius = 1 / maxRadius;
        sqrMaxRadius = maxRadius * maxRadius;
        // ChangeImgAlpha(0);
    }
    private void Update()
    {
        AxieSignal();

        ButtonSignal();
    }

    void AxieSignal()
    {
        targetDup = dir.y;
        targetDright = dir.x;
        if (inputEnable == false) // 使输入值归零
        {
            // targetDright = 0;
            // targetDup = 0;
        }
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);   // 1.0是maxspeed
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);   // 1.0是maxspeed


        Dmag = Mathf.Sqrt((Dup* Dup) + (Dright * Dright));
        Dvec = Handler.right * Dright + Handler.forward * Dup;

    }

    void ChangeImgAlpha(float value)
    {
        handleImg.color = new Color(255, 255, 255, value);
        joystickImg.color = new Color(255, 255, 255, value);
    }

    // private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    // {
    //     Vector2 localPoint = Vector2.zero;
    //     RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, _camera, out localPoint);
    //     return localPoint;
    // }
    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (transform.GetChild(0).GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out pointDownPos);
        // pointDownPos = eventData.position;
        joystickImg.transform.localPosition = pointDownPos;
        pointDownPos = eventData.position;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dis = eventData.position - pointDownPos;
        // float clamp = Mathf.Clamp(dis.sqrMagnitude, 0f, maxRadius*maxRadius);
        float sqrClampDis = dis.sqrMagnitude;
        if (sqrClampDis < 0) sqrClampDis = 0;
        if (sqrClampDis > sqrMaxRadius)
        {
            sqrClampDis = sqrMaxRadius;
            dis = dis.normalized*maxRadius;
        }
        dir = new Vector2(dis.x * reciMaxRadius, dis.y * reciMaxRadius);
        handleTrans.localPosition = dis;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        handleTrans.localPosition = Vector2.zero;
        lastDir = (dir == Vector2.zero) ? lastDir : dir;
        dir = Vector2.zero;
    }

}
