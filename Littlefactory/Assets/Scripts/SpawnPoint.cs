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
    public Vector3 shooterPosition;
    public Quaternion myRotation;
    public List<GameObject> ChanceTokens = new List<GameObject>();//用来存储标记物的列表
    public static bool alreadyfire = false;
    void Start()
    {
        shooterPosition = shooter.transform.position;
        myRotation = transform.rotation;
        while (GameManager.startedgame == false)
        {
            InvokeRepeating("Fire", 0f, fireDistance);//根据预设值每隔一段时间发射一次
        }
    }
    private void Update()
    {
        if(alreadyfire = false&& GameManager.chanceCount == 0)
        {
            Fire();
            alreadyfire = true;
        }
    }
    public void Fire()
    {
        if (GameManager.allowshoot == true)//防止在切换界面的时候发射子弹
        {
            GameObject pGO = Instantiate(projectilePrefab, shooterPosition, myRotation);//把位置和方向赋给子弹
            Projectile projectile = pGO.GetComponent<Projectile>();
            projectile.speed = projectileSpeed;//获取子弹脚本并赋予速度
        }
        
    }

    public static void UpdateChanceToken()
    {
        //处理标记物的函数
        //没写完，您请
    }//我用别的函数处理了，这个是当个赛博遗址还是怎滴？
}
