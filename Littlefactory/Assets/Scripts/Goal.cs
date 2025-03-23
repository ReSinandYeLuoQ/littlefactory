using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [Header("动态设置")]
    public static GameObject filter;
    public bool filterActive;
    public int filterColor;
    public AudioSource audioSource;
    private void Start()
    {
        filter = transform.Find("Filter").gameObject;
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时判定过关
        if (coll.gameObject.tag == "Projectile" )
        {
            if (!filter.activeSelf || (filterActive && coll.GetComponent<Projectile>().color == filterColor))//过滤器判定，虽然这很不优雅但太长了我分两层写
            {
                audioSource.Play();
                Debug.Log(coll.GetComponent<Projectile>().color);
                Destroy(coll.gameObject); ;
                if (!coll.GetComponent<Projectile>().firedDuringCutscene && GameManager.allowshoot == true)//防止切屏的时候进入目标点，放在原来那个位置的话开场动画没法正常运行
                {
                    GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                    gameManager.Levelchange();
                }
            }
        }
    }

    public static void SetFilter(int color)
    {
        switch (color)
        {
            case 0:
                filter.SetActive(false);
                break;
            case 1:
                filter.SetActive(true);
                filter.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(80, 159, 126, 1);
                break;
            case 2:
                filter.SetActive(true);
                filter.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(150, 80, 138, 1);
                break;
            case 3:
                filter.SetActive(true);
                filter.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(80, 81, 159, 1);
                break;
        }
    }
}
