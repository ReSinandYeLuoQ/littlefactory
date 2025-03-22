using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [Header("动态设置")]
    public GameObject filter;
    public bool filterActive;
    public int filterColor;
    public AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //设置过滤器是否显示以及过滤器颜色
        filter.SetActive(filterActive);
        switch (filterColor)
        {
            case 1:
                filter.GetComponent<SpriteRenderer>().color = UnityEngine.Color.blue;
                break;
            case 2:
                filter.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(1, 0, 1, 1);
                break;
            case 3:
                filter.GetComponent<SpriteRenderer>().color = UnityEngine.Color.cyan;
                break;
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        //子弹进入时判定过关
        if (coll.gameObject.tag == "Projectile" && GameManager.allowshoot == true)//防止切屏的时候进入目标点
        {
            if (!filterActive || (filterActive && coll.GetComponent<Projectile>().color == filterColor))//过滤器判定，虽然这很不优雅但太长了我分两层写
            {
                audioSource.Play();
                Debug.Log(coll.GetComponent<Projectile>().color);
                Destroy(coll.gameObject);
                GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
                gameManager.Levelchange();
            }
        }
    }
}
