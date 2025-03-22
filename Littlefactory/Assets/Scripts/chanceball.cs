using Unity.VisualScripting;
using UnityEngine;

public class chanceball : MonoBehaviour
{
    public float backDistance = 0.5f; // 后退的距离
    public float backDuration = 0.2f; // 后退的持续时间
    public float shootSpeed = 10f; // 射出的速度
    public int number;//这个球是第几个球
    public float transparency = 1f; // 透明度值，范围 0（完全透明）到 1（完全不透明）


    private Vector3 targetPosition;//这几行ai打的，反正总的来说是实现一种击中的效果
    private Vector3 startPosition;
    private bool isBacking = false;
    private bool isShooting = false;
    private float elapsedTime = 0f;

    public void Start()//这一整篇代码大部分都是我用AI敲的，有一点晕了已经
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = GetComponent<Renderer>().material;
        // 设置材质支持透明度
        SetMaterialToTransparent(material);
        // 修改材质的透明度
        Color color = material.color;
        color.a = transparency;
        material.color = color;
    }
    public void chance(Arrow bug)
    {
        targetPosition = bug.GetComponent<Transform>().position;
        startPosition = transform.position;
        isBacking = true;
        elapsedTime = 0f;

        if (isBacking)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / backDuration);
            // 计算后退的方向
            Vector3 backDirection = (transform.position - targetPosition).normalized;
            transform.position = Vector3.Lerp(startPosition, startPosition + backDirection * backDistance, t);

            if (t >= 1f)
            {
                isBacking = false;
                isShooting = true;
                elapsedTime = 0f;
            }
        }
        else if (isShooting)
        {
            // 朝着目标位置射出
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, shootSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // 获取材质
                transparency = 0;
            }
        }
    }

    public void reback()//把发射出去的球重新放回来，另外一部分代码应该在manager
    {
        if(GameManager.chanceCount>=number)
                this.GetComponent<Transform>().position= startPosition;
                transparency = 1;
        
    }
    void SetMaterialToTransparent(Material material)
    {
        // 设置材质的渲染模式为透明
        material.SetFloat("_Mode", 3);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("_ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = 3000;
    }
}