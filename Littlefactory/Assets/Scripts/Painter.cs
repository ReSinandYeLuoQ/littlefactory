using System.Collections;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public int xLine;//这一段是导入关卡用的
    public float[] xlist = { -8f, -6f, -4f, -2f, 0f, 2f, 4f, 6f, 8f };
    private float xset;
    public int yLine;
    public float[] ylist = { 3.6f, 2.4f, 1.2f, 0f, -1.2f, -2.4f, -3.6f };
    private float yset;
    private float zset = 0f;
    public bool hadused = false;//是否已用
    [Header("在检视器中设置")]
    public int color;
    public Color shadow;
    public AudioSource audioSource;
    void Start()
    {
        xset = xlist[xLine - 1];//这一段是导入关卡用的
        yset = ylist[yLine - 1];
        Vector3 vec = new Vector3(xset, yset, zset);
        transform.position = vec;
        audioSource = GetComponent<AudioSource>();
        switch (color)//根据颜色值决定染色器外观
        {
            case 1://三个颜色我给换了，反正换了三个饱和度比较低但是明度挺高的颜色
                GetComponent<SpriteRenderer>().color = new UnityEngine.Color(80, 159, 126);
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = new UnityEngine.Color(150, 80, 138);
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = new UnityEngine.Color(80, 81, 159);
                break;
        }
    }
    private void Update()
    {
        if (hadused == true&&GameManager.startedgame == true)//如果已经生效过了透明度就会降低
        {
            shadow = GetComponent<SpriteRenderer>().color;
            shadow.a = 1;
        }
        else
        {
            shadow = GetComponent<SpriteRenderer>().color;
            shadow.a = 0.6f;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile"&&hadused==false)
        {
            audioSource.Play();
            hadused = true;
            switch (color)
            {
                case 1:
                    coll.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(80, 159, 126);
                    coll.GetComponent<Projectile>().color = 1;
                    break;
                case 2:
                    coll.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(150, 80, 138);
                    coll.GetComponent<Projectile>().color = 2;
                    break;
                case 3:
                    coll.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(80, 81, 159);
                    coll.GetComponent<Projectile>().color = 3;
                    break;
            }

        }
    }
}
