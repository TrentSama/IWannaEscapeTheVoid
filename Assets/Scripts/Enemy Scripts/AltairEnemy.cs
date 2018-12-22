using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltairEnemy : MonoBehaviour
{
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // the rigidbody
    private Rigidbody2D rb;

    public GameObject deathEffect;
    public GameObject bullet;
    public Transform bulletSpawner;

    // Used for invincibility
    bool invincible = false;

    // A variable for when the enemy can shoot and another that's just a generic timer
    public int timeBetweenShots;
    float timer;

    Animator animator;

    public AudioSource audioSource;
    public AudioClip hitSound;

    // this bool is to tell when the player leaves a room, so it can despawn all active enemies
    public BoolVariable playerExit;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Shoot();
        Death();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = -transform.right * enemy.enemySpeed;
    }

    // Death. Self explanitory.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if (playerExit.Switch == true)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        if (timer >= timeBetweenShots)
        {
            Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
            timer = 0;
        }
    }

    //Used for collision. Using an Ienumerator so I can have invincibility time.
    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathWall")
        {
            Death();
        }
        if (!invincible)
        {
            if (collision.tag == "Bullet")
            {
                invincible = true;
                StartCoroutine(FlashWhenHit());
                audioSource.clip = hitSound;
                audioSource.Play();
                enemy.enemyHP -= collision.GetComponent<Bullet>().damage; // Getting the damage value of the bullet. 
                yield return new WaitForSeconds(collision.GetComponent<Bullet>().invincibleTime); // an enemy invincibility is based on the ammo type.
                invincible = false;
            }
        }
    }

    IEnumerator FlashWhenHit()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        while (invincible == true)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
