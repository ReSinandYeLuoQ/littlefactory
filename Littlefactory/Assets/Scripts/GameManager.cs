using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool allowshoot = false; // 允许发射和进行切关
    public int startlevel; // 当前关卡序号
    public Image blackone; // 渐变的那张黑色幕布
    public GameObject activelevel; // 父对象
    public string[] levelline = { "level1", "level2", "level3", "level4", "level5", "level6", "level7", "level8", "level9", "level10", "level11", "level12" }; // 关卡和名字的对应数列
    public int[] chanceCountlist = { 1, 1, 2, 1, 2, 2, 2, 3, 1, 1, 3, 5 };//剩余次数
    private GameObject[] GOb;
    private float fadeStartTime = 0;
    public static bool isFadingIn;
    public static bool isFadingOut;
    private const float fadeDuration = 2f;//黑屏时长
    private const float initialDelay = 0.5f;//黑屏前间隔
    private float delayStartTime;
    public static int chanceCount;//剩余尝试次数

    void Start() // 开始的时候选关
    {
        if (startlevel > 0 && startlevel <= levelline.Length)
        {
            GOb = new GameObject[levelline.Length];
            for (int i = 0; i < levelline.Length; i++)
            {
                Debug.Log(i);
                GOb[i] = GameObject.Find(levelline[i]);
                if (GOb[i] == null)
                {
                    Debug.LogError($"未能找到游戏对象: {levelline[i]}");
                }
                if (i != 0)
                {
                    GOb[i].SetActive(false);
                }
            }

            activelevel = GOb[startlevel - 1]; // 初始关卡设定
            //Levelchange(); // 执行换关函数
            isFadingOut = true;
        }
    }

    void SetImageOpacity(float getcolor) // 变色函数
    {
        // 确保透明度在 0 到 1 的范围内
        getcolor = Mathf.Clamp01(getcolor);
        Color color = blackone.color;
        color.a = getcolor;
        blackone.color = color;
    }

    public void Levelchange() // 换关函数
    {
        startlevel = startlevel + 1;
        allowshoot = false;
        delayStartTime = Time.time;
        isFadingIn = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true && allowshoot == true)//这个重开现在还有问题
        {
            startlevel = startlevel - 1;
            if (startlevel < 1)
            {
                startlevel = 1; // 防止 startlevel 小于 1
                Debug.LogError("关卡序号不能小于 1");
                return;
            }
            Levelchange();
        }
        if (isFadingIn)
        {
            if (Time.time - delayStartTime < initialDelay)
            {
                return;
            }

            if (fadeStartTime == 0)
            {
                fadeStartTime = Time.time;
            }

            float elapsedTime = Time.time - fadeStartTime;
            if (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
                SetImageOpacity(alpha);
            }
            else
            {
                SetImageOpacity(1f);
                Destroy(GameObject.FindWithTag("Projectile"));

                if (startlevel - 2 >= 0 && startlevel - 2 < GOb.Length && GOb[startlevel - 2] != null)
                {
                    GOb[startlevel - 2].SetActive(false); // 然后将之前的关卡失活
                }
                else
                {
                    Debug.LogError($"无法设置关卡为非激活状态，可能是索引越界或对象为 null，startlevel: {startlevel}");
                }

                if (startlevel - 1 >= 0 && startlevel - 1 < GOb.Length && GOb[startlevel - 1] != null)
                {
                    GOb[startlevel - 1].SetActive(true); // 并使其活动
                }
                else
                {
                    Debug.LogError($"无法设置关卡为激活状态，可能是索引越界或对象为 null，startlevel: {startlevel}");
                }

                isFadingIn = false;
                isFadingOut = true;
                fadeStartTime = 0;
                delayStartTime = Time.time;
            }
        }
        else if (isFadingOut)
        {
            if (Time.time - delayStartTime < initialDelay)
            {
                return;
            }

            if (fadeStartTime == 0)
            {
                fadeStartTime = Time.time;
            }

            float elapsedTime = Time.time - fadeStartTime;
            if (elapsedTime < fadeDuration)
            {
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
                SetImageOpacity(alpha);
            }
            else
            {
                SetImageOpacity(0f);
                fadeStartTime = 0;
                isFadingOut = false;
                chanceCount = chanceCountlist[startlevel - 1];
                allowshoot = true;
            }
        }
    }

    public void FadeIn()
    {
        // 逻辑在 Update 中处理
    }

    public void FadeOut()
    {
        // 逻辑在 Update 中处理
    }
}