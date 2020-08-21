using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class bird : MonoBehaviour
{
    public float maxDis = 2;//最大距离
    [HideInInspector]
    public SpringJoint2D sp;
    protected bool isClick = false;
    protected Rigidbody2D rg;

    //获得画线组件
    public LineRenderer left;
    public LineRenderer right;
    public Transform leftPos;
    public Transform rightPos;

    protected TestMyTrail myTrail;//拖尾
    public GameObject boom;//爆炸
    [HideInInspector]
    public bool canMove = false;
    public float smooth = 3;//距离

    public AudioClip select;//选择声音
    public AudioClip fly;//飞翔声音

    public bool isRelease = false;//是否在弹弓上

    private bool isFlay = false;//是否在飞行中

    public Sprite hurt;//受伤
    protected SpriteRenderer render;


    //控制动力学
    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestMyTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    //鼠标按下
    private void OnMouseDown()
    {
        if (canMove)
        {
            AudoPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }

    }
    //鼠标抬起
    private void OnMouseUp()
    {
        if (canMove)
        {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", 0.1f);
            //禁用画线组件
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }

    }
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject()){//判断点击的是否是按钮
            return;
        }
        //鼠标一直按下，进行位置跟随
        if (isClick)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // transform.position += new Vector3(0,0,10);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            //进行位置限定
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis)
            {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDis;//控制最大长度
                transform.position = pos + rightPos.position;
            }
            Line();

        }

        //相机跟随
        float posX = transform.position.x;//小鸟的x
        Camera.main.transform.position =
            Vector3.Lerp(Camera.main.transform.position,
            new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, Camera.main.transform.position.z),
            smooth * Time.deltaTime);

        if (isFlay)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ShowSkill();
            }
        }
    }

    void Fly()
    {
        isRelease = true;
        isFlay = true;
        AudoPlay(fly);
        sp.enabled = false;
        // 五秒后调用
        Invoke("Next", 3);
        myTrail.StartTrails();

    }
    //画线函数
    protected void Line()
    {
        right.enabled = true;
        left.enabled = true;
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);

    }

    // 下一个小鸟飞出
    protected virtual void Next()
    {
        GameManager._instance.birds.Remove(this);//移除小鸟
        Destroy(gameObject);//销毁对象
        //生成爆炸特效
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    //小鸟碰撞
    private void OnCollisionEnter2D(Collision2D other)
    {
        isFlay = false;
        myTrail.ClearTrails();
    }

    //播放音乐
    public void AudoPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    //小鸟技能
    public virtual void ShowSkill()
    {
        isFlay = false;
    }

    public void Hurt()
    {
        render.sprite = hurt;
    }
}
