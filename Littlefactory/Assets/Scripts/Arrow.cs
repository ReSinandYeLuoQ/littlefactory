using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("在检视器中设置")]
    public bool hasBug;
    public SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        ChangeColor();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时处理其位置和方向
        if (!hasBug && coll.gameObject.tag == "Projectile")
        {
            coll.transform.position = transform.position;//把子弹移到转向器自身位置，不加这行子弹会偏移
            coll.transform.rotation = transform.rotation;//让子弹转向，检视器中三角是什么方向子弹就向哪边飞
        }
    }
    void OnMouseDown()
    {
        //被点击时如果有bug则移除bug并更新颜色
        if (hasBug&&SpawnPoint.chanceCount>0)
        {
            hasBug = false;
            ChangeColor();
            SpawnPoint.chanceCount--;
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
