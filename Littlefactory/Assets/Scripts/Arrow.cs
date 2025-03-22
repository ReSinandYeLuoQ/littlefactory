using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("在检视器中设置")]
    public bool hasBug;
    public int xLine;
    public float[] xlist = { -8f, -6f, -4f, -2f,0f, 2f, 4f, 6f ,8f};
    private float xset;
    public int yLine;
    public float[] ylist = { 3.6f,2.4f,1.2f,0f,-1.2f,-2.4f,-3.6f };
    private float yset;
    public int Rotote;//从0-3选择一个数，顺时针旋转90*输入的角度
    private float zset =0f;
    public SpriteRenderer sr;
    public AudioSource audioSource;//我草啊as是个关键字
    public AudioClip arrowSound;
    public AudioClip debugSound;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        ChangeColor();
        xset = xlist[xLine - 1];
        yset = ylist[yLine - 1];
        Vector3 vec = new Vector3 (xset, yset, zset) ;
        transform.position = vec;
        float rotationAngle = -90f * Rotote;
        transform.Rotate(0f, 0f, rotationAngle);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时处理其位置和方向
        if (!hasBug && coll.gameObject.tag == "Projectile")
        {
            coll.transform.position = transform.position;//把子弹移到转向器自身位置，不加这行子弹会偏移
            coll.transform.rotation = transform.rotation;//让子弹转向，检视器中三角是什么方向子弹就向哪边飞
            audioSource.clip = arrowSound;
            audioSource.Play();
        }
    }
    void OnMouseDown()
    {
        //被点击时如果有bug则移除bug并更新颜色
        if (hasBug&&GameManager.chanceCount>0)
        {
            hasBug = false;
            ChangeColor();
            GameManager.chanceCount--; 
            audioSource.clip = debugSound;
            audioSource.Play();
        }
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
            sr.color = Color.white;
        }
    }
}
