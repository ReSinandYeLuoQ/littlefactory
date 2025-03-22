using UnityEngine;

public class Restart : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnMouseDown()
    {
        if (GameManager.allowshoot == true)//这个重开现在问题不大了
        {
            
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.startlevel = gameManager.startlevel - 1;//相当于是关卡计数减少1位，然后通关……但是我们的布局不是代码存的！我靠！大部分的工作量其实都是从这里延伸出来的。妈妈生的
            gameManager.Levelchange();
        }
    }

    // Update is called once per frame
}
