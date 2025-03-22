using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool allowshoot = false; // ������ͽ����й�
    public int startlevel; // ��ǰ�ؿ����
    public Image blackone; // ��������ź�ɫĻ��
    public GameObject activelevel; // ������
    public string[] levelline = { "level1", "level2", "level3", "level4", "level5", "level6", "level7", "level8", "level9", "level10", "level11", "level12" }; // �ؿ������ֵĶ�Ӧ����
    public int[] chanceCountlist = { 1, 1, 2, 1, 2, 2, 2, 3, 1, 1, 3, 5 };//ʣ�����
    private GameObject[] GOb;
    private float fadeStartTime = 0;
    public static bool isFadingIn;
    public static bool isFadingOut;
    private const float fadeDuration = 2f;//����ʱ��
    private const float initialDelay = 0.5f;//����ǰ���
    private float delayStartTime;
    public static int chanceCount;//ʣ�ೢ�Դ���

    void Start() // ��ʼ��ʱ��ѡ��
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
                    Debug.LogError($"δ���ҵ���Ϸ����: {levelline[i]}");
                }
                if (i != 0)
                {
                    GOb[i].SetActive(false);
                }
            }

            activelevel = GOb[startlevel - 1]; // ��ʼ�ؿ��趨
            //Levelchange(); // ִ�л��غ���
            isFadingOut = true;
        }
    }

    void SetImageOpacity(float getcolor) // ��ɫ����
    {
        // ȷ��͸������ 0 �� 1 �ķ�Χ��
        getcolor = Mathf.Clamp01(getcolor);
        Color color = blackone.color;
        color.a = getcolor;
        blackone.color = color;
    }

    public void Levelchange() // ���غ���
    {
        startlevel = startlevel + 1;
        allowshoot = false;
        delayStartTime = Time.time;
        isFadingIn = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) == true && allowshoot == true)//����ؿ����ڻ�������
        {
            startlevel = startlevel - 1;
            if (startlevel < 1)
            {
                startlevel = 1; // ��ֹ startlevel С�� 1
                Debug.LogError("�ؿ���Ų���С�� 1");
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
                    GOb[startlevel - 2].SetActive(false); // Ȼ��֮ǰ�Ĺؿ�ʧ��
                }
                else
                {
                    Debug.LogError($"�޷����ùؿ�Ϊ�Ǽ���״̬������������Խ������Ϊ null��startlevel: {startlevel}");
                }

                if (startlevel - 1 >= 0 && startlevel - 1 < GOb.Length && GOb[startlevel - 1] != null)
                {
                    GOb[startlevel - 1].SetActive(true); // ��ʹ��
                }
                else
                {
                    Debug.LogError($"�޷����ùؿ�Ϊ����״̬������������Խ������Ϊ null��startlevel: {startlevel}");
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
        // �߼��� Update �д���
    }

    public void FadeOut()
    {
        // �߼��� Update �д���
    }
}