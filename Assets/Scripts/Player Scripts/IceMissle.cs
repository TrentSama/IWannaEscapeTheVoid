using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceMissle : MonoBehaviour
{
    // This is a reference to how many bullets are in the scene so you can't shoot too much 
    public FloatVariable bulletsInScene;
    Rigidbody2D rb;
    //Reference to the script that holds the values for different data types
    private Bullet bullet;
    // The trail we spawn to make it look nice
    public GameObject trail;
    public Transform trailSpawner;
    //The explosion after it hits something, again, to look nice
    public GameObject explosion;
    public Transform explosionSpawner;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bullet = GetComponent<Bullet>();
        StartCoroutine(TimeToDespawn());
        InvokeRepeating("TrailSpawner", 0.1f, 0.1f);
    }
    void FixedUpdate()
    {
        if (bullet.bulletSpeed < 1)
            bullet.bulletSpeed += 0.1f;
        else if (bullet.bulletSpeed >= 1)
            bullet.bulletSpeed += 0.3f;
        rb.velocity = transform.up * bullet.bulletSpeed;
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision != null)
        {
            bulletsInScene.Value -= 1;
            yield return new WaitForSeconds(0.01f);
            Explosion();
            Destroy(gameObject);
        }
    }

    void Explosion()
    {
        Instantiate(explosion, explosionSpawner.position, explosionSpawner.rotation);
    }

    void TrailSpawner()
    {
        Instantiate(trail, trailSpawner.position, trailSpawner.rotation);
    }

    IEnumerator TimeToDespawn()
    {
        yield return new WaitForSeconds(bullet.despawnTime);
        bulletsInScene.Value -= 1;
        Destroy(gameObject);
    }
}
