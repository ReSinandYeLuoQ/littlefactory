using UnityEngine;

public class name : MonoBehaviour//这个就是主标题的代码
{
    public float fadeInDuration = 2f; // 淡入持续时间
    public float stayDuration = 3f; // 保持实体的持续时间
    public float fadeOutDuration = 2f; // 淡出持续时间

    private Material material;
    private float elapsedTime;

    private enum FadeState//这大概是个流程的分化，这一段总的来说也是AI写的，我就不写注释了，AI写的可靠性应该比我在这个状态下写的可靠
    {
        FadeIn,
        Stay,
        FadeOut
    }

    private FadeState currentState = FadeState.FadeIn;

    void Start()
    {
        // 获取对象的MeshRenderer组件上的材质
        material = GetComponent<MeshRenderer>().material;
        // 确保材质支持透明度更改，设置为透明渲染模式
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;

        // 初始时将对象设置为完全透明
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
                // 计算淡入过程中的透明度
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
                // 计算淡出过程中的透明度
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
