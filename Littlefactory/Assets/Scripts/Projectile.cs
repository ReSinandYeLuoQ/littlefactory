using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("动态设置")]
    public float speed;
    public float color = 0;
    void Update()
    {
        if(GameManager.allowshoot == true)
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime);//每一帧根据速度值和子弹自身朝向移动一次，要动态修改子弹方向就更改子弹transform的朝向
        }
    }
}
