using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{
    public TextMesh m_DmgText;
    public TextMesh dmgT;
    public float upSpeed;
    const float UPSPEED = 2f;
    private float rightSpeed;
    public float lifeTime;
    private float gravity = -1.5f;
    private bool stop = false;
    private bool speedDown = false;
    private float stopSpeed;
    private float expandScale = 0.1f;
    private Transform faceTrans;
    // Start is called before the first frame update
    void OnEnable()
    {
        upSpeed = UPSPEED;
        stop = false;
        speedDown = false;
        // m_DmgText.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        // m_DmgText.transform.localPosition = Vector3.zero;
        m_DmgText.color += new Color(255, 0, 0, 1);

        StartCoroutine("PushObject");
        // Destroy(gameObject, lifeTime);
        rightSpeed = Random.Range(-upSpeed * 0.3f, upSpeed * 0.3f);
        stopSpeed = Random.Range(-0.5f, -0.8f);
        m_DmgText = transform.GetComponentInChildren<TextMesh>();
    }
    private void Awake()
    {
        faceTrans = IGameManager.Instance.GetPlayerHandle().transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        // this.transform.rotation = Quaternion.LookRotation(new Vector3(0, 0, 1));
        // transform.forward = faceTrans.forward;
        if(upSpeed>0&&!stop)
            transform.localScale += Vector3.one*Time.deltaTime*0.9f;
        if (upSpeed < stopSpeed)
            speedDown = true;
        if (speedDown)
        {
            upSpeed += 3 * Time.deltaTime;
            if(upSpeed >= 0)
                stop = true;
        }
        else
            upSpeed += gravity * Time.deltaTime;

        if (!stop)
            transform.position += new Vector3(rightSpeed, upSpeed, 0) * Time.deltaTime;
        else
        {
            transform.localScale += new Vector3(expandScale, expandScale, 0) * Time.deltaTime;
            StartCoroutine("SetColor");
        }
        //     upSpeed = 0;
        // m_DmgText.transform.position = Vector3.zero;
    }
    public void ShowUIDamage(float value)
    {
        m_DmgText.text = value.ToString();
    }
    IEnumerator SetScale()
    {
        yield return new WaitForSeconds(0.1f);
        m_DmgText.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        yield return new WaitForSeconds(0.06f);
        m_DmgText.transform.localScale = new Vector3(0.5f, 0.5f, 1);
    }
    IEnumerator SetColor()
    {
        yield return new WaitForSeconds(1f);
        m_DmgText.color -= new Color(0, 0, 0, 1 * Time.deltaTime);
    }
    IEnumerator PushObject()
    {
        yield return new WaitForSeconds(lifeTime);
        // Debug.Log("回收");
        IFactory.GetObjectPool().PushObject(gameObject);
    }
}
