using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [Header("在检视器中设置")]
    public bool hasBug;
    public int xLine;//这一大串都是导入关卡用的！
    public float[] xlist = { -8f, -6f, -4f, -2f,0f, 2f, 4f, 6f ,8f};
    private float xset;
    public int yLine;
    public float[] ylist = { 3.6f,2.4f,1.2f,0f,-1.2f,-2.4f,-3.6f };
    private float yset;
    public int Rotote;//从0-3选择一个数，顺时针旋转90*输入的角度
    private float zset =0f;
    public bool hadBug;//用来重置关卡用的变量，记录这个位置应该，或者说曾是有bug的
    public bool hadused = false;//记录已经用过
    public Color shadow;//用过的阴影
    public SpriteRenderer sr;
    public Animator am;
    public AudioSource audioSource;//我草啊as是个关键字
    public AudioClip arrowSound;
    public AudioClip debugSound;
    private Animation arrowanimation;
    void Start()
    {
        arrowanimation = GetComponent<Animation>();
        hadBug = hasBug;//记录hadBug变量
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Allnobug();//初始状态：全蓝
        audioSource = GetComponent<AudioSource>();
        xset = xlist[xLine - 1];
        yset = ylist[yLine - 1];
        Vector3 vec = new Vector3 (xset, yset, zset) ;
        transform.position = vec;
        float rotationAngle = -90f * Rotote;
        transform.Rotate(0f, 0f, rotationAngle);
    }
    private void Update()
    {
        if (hadused == true&& GameManager.startedgame == true)
        {
            shadow = GetComponent<SpriteRenderer>().color;
            shadow.a = 1;
        }
        else
        {
            shadow = GetComponent<SpriteRenderer>().color;
            shadow.a = 0.6f;//用过就要不完全！
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时处理其位置和方向
        if (!hasBug && coll.gameObject.tag == "Projectile"&& hadused == false)
        {
            coll.transform.position = transform.position;//把子弹移到转向器自身位置，不加这行子弹会偏移
            coll.transform.rotation = transform.rotation;//让子弹转向，检视器中三角是什么方向子弹就向哪边飞
            audioSource.clip = arrowSound;
            audioSource.Play();
            hadused = true;
        }
    }

    [System.Obsolete]
    void OnMouseDown()
    {
        //被点击时如果有bug则移除bug并更新颜色
        if (hasBug&&GameManager.chanceCount>0)
        {
            hasBug = false;
            ChangeColor();
            chanceball[] balls = FindObjectsOfType<chanceball>();
            foreach (chanceball rb in balls)
            {
                // 通过组件获取对应的 GameObject
                GameObject go = rb.gameObject;
                if(rb.number== GameManager.chanceCount)
                {
                    rb.chance(this);
                }
            }
            GameManager.chanceCount--;
            audioSource.clip = debugSound;
            audioSource.Play();
            am.Play("debug");
        }
    }
    public void Allnobug()
    {
        sr.color = Color.blue;//全都蓝！然后用动画变红！
        
    }
    void ChangeColor()
    {
        //根据是否有bug决定颜色
        //肯定不是最终外观，这里仅在开发初期作参考
        if(hasBug)
        {
            sr.color = Color.red;
        }
        else 
        { 
            sr.color = Color.blue;
        }
    }
}
