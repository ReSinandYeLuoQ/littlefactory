using UnityEngine;

public class Restart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMouseDown()
    {
        if (GameManager.allowshoot == true)//����ؿ��������ⲻ����
        {
            
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.startlevel = gameManager.startlevel - 1;//�൱���ǹؿ���������1λ��Ȼ��ͨ�ء����������ǵĲ��ֲ��Ǵ����ģ��ҿ����󲿷ֵĹ�������ʵ���Ǵ�������������ġ���������
            gameManager.Levelchange();
        }
    }

    // Update is called once per frame
}
