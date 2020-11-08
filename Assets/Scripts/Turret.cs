using UnityEngine;

public class Turret : MonoBehaviour, ITurret
{
    [SerializeField]
    public Bullet bulletPrefab;
    
    [SerializeField]
    public Transform spawnPoint;

    public float degreeRotation = 0;
    public float power = 0;

    public void Rotate()
    {
        transform.rotation = Quaternion.identity;
        transform.Rotate(Vector3.up, degreeRotation);
    }
    public Bullet Shoot()
    {
        var bullet = Instantiate(bulletPrefab, spawnPoint.position, transform.rotation);
        bullet.Push(power);
        return bullet;
    }

}