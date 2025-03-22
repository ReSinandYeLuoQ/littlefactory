using UnityEngine;

public class name : MonoBehaviour//�������������Ĵ���
{
    public float fadeInDuration = 2f; // �������ʱ��
    public float stayDuration = 3f; // ����ʵ��ĳ���ʱ��
    public float fadeOutDuration = 2f; // ��������ʱ��

    private Material material;
    private float elapsedTime;

    private enum FadeState//�����Ǹ����̵ķֻ�����һ���ܵ���˵Ҳ��AIд�ģ��ҾͲ�дע���ˣ�AIд�Ŀɿ���Ӧ�ñ��������״̬��д�Ŀɿ�
    {
        FadeIn,
        Stay,
        FadeOut
    }

    private FadeState currentState = FadeState.FadeIn;

    void Start()
    {
        // ��ȡ�����MeshRenderer����ϵĲ���
        material = GetComponent<MeshRenderer>().material;
        // ȷ������֧��͸���ȸ��ģ�����Ϊ͸����Ⱦģʽ
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        // ��ʼʱ����������Ϊ��ȫ͸��
        Color color = material.color;
        color.a = 0f;
        material.color = color;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        switch (currentState)
        {
            case FadeState.FadeIn:
                // ���㵭������е�͸����
                float alphaIn = Mathf.Clamp01(elapsedTime / fadeInDuration);
                SetAlpha(alphaIn);
                if (elapsedTime >= fadeInDuration)
                {
                    elapsedTime = 0f;
                    currentState = FadeState.Stay;
                }
                break;
            case FadeState.Stay:
                if (elapsedTime >= stayDuration)
                {
                    elapsedTime = 0f;
                    currentState = FadeState.FadeOut;
                }
                break;
            case FadeState.FadeOut:
                // ���㵭�������е�͸����
                float alphaOut = 1f - Mathf.Clamp01(elapsedTime / fadeOutDuration);
                SetAlpha(alphaOut);
                if (elapsedTime >= fadeOutDuration)
                {
                    elapsedTime = 0f;
                    currentState = FadeState.FadeIn;
                    GameManager.startedgame = true;
                }
                break;
        }
    }

    void SetAlpha(float alpha)
    {
        Color color = material.color;
        color.a = alpha;
        material.color = color;
    }
}
