using Unity.VisualScripting;
using UnityEngine;

public class chanceball : MonoBehaviour
{
    public float backDistance = 0.5f; // ���˵ľ���
    public float backDuration = 0.2f; // ���˵ĳ���ʱ��
    public float shootSpeed = 10f; // ������ٶ�
    public int number;//������ǵڼ�����
    public float transparency = 1f; // ͸����ֵ����Χ 0����ȫ͸������ 1����ȫ��͸����


    private Vector3 targetPosition;//�⼸��ai��ģ������ܵ���˵��ʵ��һ�ֻ��е�Ч��
    private Vector3 startPosition;
    private bool isBacking = false;
    private bool isShooting = false;
    private float elapsedTime = 0f;

    public void Start()//��һ��ƪ����󲿷ֶ�������AI�õģ���һ�������Ѿ�
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        Material material = GetComponent<Renderer>().material;
        // ���ò���֧��͸����
        SetMaterialToTransparent(material);
        // �޸Ĳ��ʵ�͸����
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
            // ������˵ķ���
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
            // ����Ŀ��λ�����
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, shootSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                // ��ȡ����
                transparency = 0;
            }
        }
    }

    public void reback()//�ѷ����ȥ�������·Ż���������һ���ִ���Ӧ����manager
    {
        if(GameManager.chanceCount>=number)
                this.GetComponent<Transform>().position= startPosition;
                transparency = 1;
        
    }
    void SetMaterialToTransparent(Material material)
    {
        // ���ò��ʵ���ȾģʽΪ͸��
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