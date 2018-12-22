using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float despawnTime;
    public float bulletSpeed;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(TimeToDespawn());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * bulletSpeed;
    }

    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            yield return new WaitForSeconds(0.02f);
            Destroy(gameObject);
        }
    }

    IEnumerator TimeToDespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        Destroy(gameObject);
    }

}
