using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("在检视器中设置")]
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public float fireDistance;
    [Header("动态设置")]
    public Vector3 myPosition;
    public Quaternion myRotation;
    void Start()
    {
        myPosition = transform.position;
        myRotation = transform.rotation;
        InvokeRepeating("Fire", 0f, fireDistance);
    }

    public void Fire()
    {
        GameObject pGO = Instantiate(projectilePrefab,myPosition,myRotation);
        Projectile projectile = pGO.GetComponent<Projectile>();
        projectile.speed = projectileSpeed;
    }
}
