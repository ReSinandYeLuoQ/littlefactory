using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时判定过关
        if (coll.gameObject.tag == "Projectile")
        {
            Debug.Log("你过关");
            Destroy(coll.gameObject);
        }
    }
}
