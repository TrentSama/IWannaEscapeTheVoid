using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{
    // This is a reference to how many bullets are in the scene so you can't shoot too much 
    public FloatVariable bulletsInScene;
    Rigidbody2D rb;
    //Reference to the script that holds the values for different data types
    private Bullet bullet;
    // Update is called once per frame

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bullet = GetComponent<Bullet>();
        StartCoroutine(TimeToDespawn());
    }
    void FixedUpdate()
    {
        rb.velocity = transform.up * bullet.bulletSpeed;
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision != null)
        {
            bulletsInScene.Value -= 1;
            yield return new WaitForSeconds(0.01f);
            Destroy(gameObject);
        }
    }

    IEnumerator TimeToDespawn()
    {
        yield return new WaitForSeconds(bullet.despawnTime);
        bulletsInScene.Value -= 1;
        Destroy(gameObject);
    }
}
