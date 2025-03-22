using System.Collections;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [Header("在检视器中设置")]
    public int color;
    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        switch (color)//根据颜色值决定染色器外观
        {
            case 1:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case 2:
                GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
                break;
            case 3:
                GetComponent<SpriteRenderer>().color = Color.cyan;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Projectile")
        {
            audioSource.Play();
            switch (color)//根据颜色值改编子弹外观并赋值，以便最后到达终点时进行判定
            {
                case 1:
                    coll.GetComponent<SpriteRenderer>().color = Color.blue;
                    coll.GetComponent<Projectile>().color = 1;
                    break;
                case 2:
                    coll.GetComponent<SpriteRenderer>().color = new Color(1,0,1,1);
                    coll.GetComponent<Projectile>().color = 2;
                    break;
                case 3:
                    coll.GetComponent<SpriteRenderer>().color = Color.cyan;
                    coll.GetComponent<Projectile>().color = 3;
                    break;
            }

        }
    }
}
