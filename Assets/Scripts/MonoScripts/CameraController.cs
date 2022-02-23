using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject cameraHandle;
    [SerializeField]
    private GameObject playerHandle;
    [SerializeField]
    private PlayerInput input;
    private PlayerHandler handler;
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private GameObject model;

    public float horizontalSpeed = 100.0f;
    public float verticleSpeed = 80.0f;
    private Vector3 cameraDampVelocity;
    public float cameraDampSpeed = 0.03f;

    private float tempEulerX;

    float camRight;
    float camUp;
    // float camForward;

    private LockTarget lockTarget = new LockTarget(null);
    private Image lockDot = null;
    private float sqrLockDis = 0;

    private void Awake()
    {
        cameraHandle = transform.parent.gameObject;
        playerHandle = cameraHandle.transform.parent.gameObject;
        input = playerHandle.GetComponent<PlayerInput>();
        handler = playerHandle.GetComponent<PlayerHandler>();
        handler.CallChangeInput += ChangeInput;
        cam = Camera.main.gameObject;
        GameObject go = Instantiate(Resources.Load<GameObject>("LockDot"), UITool.CanvasObj.transform);
        lockDot = go.GetComponent<Image>();
        lockDot.enabled = false;
        tempEulerX = 15;
    }
    void ChangeInput()
    {
        input = handler.pi as PlayerInput;
    }
    public void UpdateModel(GameObject _model)
    {
        model = _model;
        // Debug.Log(model);
    }
    void Start()
    {
        sqrLockDis = 121f;
    }
    private void Update()
    {
        camRight = input.camRight;
        camUp = input.camUp;
        // camForward = Input.GetAxis("Mouse ScrollWheel");
        // transform.position += new Vector3(0,0,camForward);
        if (input.lockOn)
        {
            LockUnLock();
        }
        if (lockTarget.obj != null)
        {
            lockDot.rectTransform.position = Camera.main.WorldToScreenPoint(lockTarget.obj.transform.position + new Vector3(0, lockTarget.halfHeight, 0));
            if ((handler.transform.position - lockTarget.obj.transform.position).sqrMagnitude >= sqrLockDis)
            {
                RemoveTarget();
            }
            if(input.skill)
            {
                lockTarget.obj.GetComponent<CharacterHandler>().m_Character.Killed();
                // RemoveTarget();
            }
        }
    }
    void FixedUpdate()
    {
        if (lockTarget.obj == null)
        {
            Vector3 tempModelEuler = model.transform.eulerAngles;

            playerHandle.transform.Rotate(Vector3.up, camRight * horizontalSpeed * Time.fixedDeltaTime);
            tempEulerX -= camUp * verticleSpeed * Time.fixedDeltaTime;

            tempEulerX = Mathf.Clamp(tempEulerX, -40, 30);
            cameraHandle.transform.localEulerAngles = new Vector3(tempEulerX, 0, 0);

            model.transform.eulerAngles = tempModelEuler;
            cam.transform.LookAt(cameraHandle.transform);
        }
        else
        {
            Vector3 tempForward = lockTarget.obj.transform.position - model.transform.position;
            tempForward.y = 0;
            playerHandle.transform.forward = tempForward;
            // cameraHandle.transform.LookAt(lockTarget.obj.transform.position);
            cam.transform.LookAt(lockTarget.obj.transform);
        }

        // cam.transform.position = Vector3.Lerp( cam.transform.position,transform.position, 0.2f);
        cam.transform.position = Vector3.SmoothDamp(cam.transform.position, transform.position, ref cameraDampVelocity, cameraDampSpeed);
        // cam.transform.eulerAngles = transform.eulerAngles;
    }

    public void LockUnLock()
    {
        Vector3 modelOrigin1 = model.transform.position;
        Vector3 modelOrigin2 = modelOrigin1 + new Vector3(0, 1, 0);
        // Debug.Log("for:" + cam.transform.forward + "/" + model.transform.forward);

        Vector3 boxCenter = modelOrigin2 + model.transform.forward * 5f;
        Collider[] cols = Physics.OverlapBox(boxCenter, new Vector3(0.5f, 0.5f, 5f), model.transform.rotation, LayerMask.GetMask("Enemy"));
        if (cols.Length == 0)
        {
            RemoveTarget();
            // Debug.Log("解锁");
        }
        else
        {
            foreach (var col in cols)
            {
                if (lockTarget.obj == col.gameObject)
                {
                    RemoveTarget();
                    // Debug.Log("重复锁定");
                    break;
                }
                AddTarget(col.gameObject);
                lockTarget.halfHeight = col.bounds.extents.y;
                // Debug.Log("锁定：" + col.gameObject.name + "高度：" + lockTarget.halfHeight);
                break;
            }
        }
    }
    void RemoveTarget()
    {
        lockTarget.obj = null;
        lockDot.enabled = false;
        handler.lockState = false;
    }
    void AddTarget(GameObject tar)
    {
        lockTarget.obj = tar;
        lockDot.enabled = true;
        handler.lockState = true;
    }
    public void RemoveLockedTarget(GameObject target)
    {
        if(target == lockTarget.obj) RemoveTarget();
    }
    private class LockTarget
    {
        public GameObject obj;
        public float halfHeight;
        public LockTarget(GameObject obj)
        {
            this.obj = obj;
        }

    }
}
