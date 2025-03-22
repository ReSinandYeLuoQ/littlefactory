using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("在检视器中设置")]
    [Header("动态设置")]
    public float speed;
    public Rigidbody2D rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.linearVelocity = new Vector2 (speed, 0);
    }

}
