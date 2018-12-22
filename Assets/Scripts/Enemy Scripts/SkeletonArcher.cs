using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcher : MonoBehaviour
{
    // The spawner usedfor the enemy to shoot from.
    public Transform bulletSpawner;
    // The bullet the enemy spawns 
    public GameObject bullet;
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // reference to the script that tells us if the player is in range
    private FieldOfVision fieldOfView;

    private GameObject player;
    public GameObject deathEffect;

    Animator animator;

    // A variable for when the enemy can shoot and another that's just a generic timer
    public int timeBetweenShots;
    bool firstShot;
    float timer;

    // Used for invincibility
    bool invincible = false;

    // this bool is to tell when the player leaves a room, so it can despawn all active enemies
    public BoolVariable playerExit;

    public float pickupDropChance;

    public GameObject[] pickups;

    public AudioSource audioSource;
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        fieldOfView = GetComponentInChildren<FieldOfVision>();
    }

    // Update is called once per frame
    void Update()
    {
        InscreaseTimer();
        Turn();
        Shoot();
        Death();
    }

    void Turn()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    void Shoot()
    {
        if (fieldOfView.playerInRange == true && firstShot == false)
        {
            animator.SetTrigger("Shoot");
            firstShot = true;
        }
        if (timer >= timeBetweenShots && firstShot == true)
        {
            if (fieldOfView.playerInRange == true)
            {
                animator.SetTrigger("Shoot");
                timer = 0;
            }
        }
    }

    void FireProjectile()
    {
        Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
    }


    // Increases a local timer for this specific enemy
    void InscreaseTimer()
    {
        if (fieldOfView.playerInRange == true && firstShot == true)
        {
            timer += 0.2f;
        }
        else if (fieldOfView.playerInRange == false)
        {
            timer = 0;
        }

    }

    // Death. Self explanitory.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            int dropRate = Random.Range(0, 100);
            if (dropRate <= pickupDropChance)
            {
                int ran = Random.Range(0, pickups.Length);
                Instantiate(pickups[ran], gameObject.transform.position, gameObject.transform.rotation);
            }
            Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if (playerExit.Switch == true)
        {
            Destroy(gameObject);
        }
    }

    //Used for collision. Using an Ienumerator so I can have invincibility time.
    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
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
