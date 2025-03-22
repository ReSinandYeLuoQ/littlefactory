using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    //我之前照搬书上的基于镜头的边界检测脚本，啥也不用动
    [HideInInspector]//哇嗷！
    public bool offRight, offLeft, offUp, offDown;
    [Header("检视器中设置")]
    public float radius = -1f;//越大边界越靠里，负数的话边界就在屏幕外，游戏对象触碰到边界之后自毁
    [Header("动态设置")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
    }
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;
        offRight = offLeft = offDown = offUp = false;
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }
        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if (!isOnScreen)
        {
            Destroy(gameObject);
        }
    }
}
