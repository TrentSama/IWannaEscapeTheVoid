using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchesCat : MonoBehaviour
{
    // This is a reference to how many bullets are in the scene so you can't shoot too much 
    public FloatVariable bulletsInScene;
    Rigidbody2D rb;
    //Reference to the script that holds the values for different data types
    private Bullet bullet;

    //The explosion after it hits something, again, to look nice
    public GameObject explosion;
    public Transform explosionSpawner;

    public AudioSource audioSource;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource.clip = explosionSound;
        rb = GetComponent<Rigidbody2D>();
        bullet = GetComponent<Bullet>();
        StartCoroutine(TimeToDespawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         rb.velocity = transform.up * bullet.bulletSpeed;
        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy" && collision != null)
        {
            audioSource.Play();
            Explosion();
        }
    }

    void Explosion()
    {
        Instantiate(explosion, explosionSpawner.position, explosionSpawner.rotation);
    }

    IEnumerator TimeToDespawn()
    {
        yield return new WaitForSeconds(bullet.despawnTime);
        bulletsInScene.Value -= 1;
        Destroy(gameObject);
    }
}
