using UnityEngine;

public class Firespecialeffect : MonoBehaviour
{
    public Animator am;
    private void Start()
    {
        am = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Fireeffect()
    {
        am.Play("fire");
    }


    // Update is called once per frame

}
