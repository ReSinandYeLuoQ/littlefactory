using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时判定过关
        if (coll.gameObject.tag == "Projectile" && GameManager.allowshoot==true)//防止切屏的时候进入目标点
        {
            Debug.Log("你过关");//过关的小曲~
            Destroy(coll.gameObject);
            GameManager gameManager=GameObject.Find("GameManager").GetComponent< GameManager >();
            gameManager.Levelchange();
        }
    }
}
