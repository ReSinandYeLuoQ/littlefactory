using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("在检视器中设置")]
    public GameObject projectilePrefab;
    public GameObject shooter;
    public GameObject ChanceTokenPrefab;
    public float projectileSpeed;//子弹速度
    public float fireDistance;//发射间隔
    [Header("动态设置")]
    public static int chanceCount;//剩余尝试次数
    public Vector3 shooterPosition;
    public Quaternion myRotation;
    public List<GameObject> ChanceTokens = new List<GameObject>();//用来存储标记物的列表
    void Start()
    {
        chanceCount = 1;//这里仅作测试，后续会根据关卡来设置尝试次数
        shooterPosition = shooter.transform.position;
        myRotation = transform.rotation;
        InvokeRepeating("Fire", 0f, fireDistance);//根据预设值每隔一段时间发射一次
    }

    public void Fire()
    {
        GameObject pGO = Instantiate(projectilePrefab,shooterPosition,myRotation);//把位置和方向赋给子弹
        Projectile projectile = pGO.GetComponent<Projectile>();
        projectile.speed = projectileSpeed;//获取子弹脚本并赋予速度
    }

    public static void UpdateChanceToken()
    {
        //处理标记物的函数
        //没写完，您请
    }
}
