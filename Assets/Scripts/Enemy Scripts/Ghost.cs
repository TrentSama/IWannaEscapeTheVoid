using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // reference to the script that tells us if the player is in range
    private FieldOfVision fieldOfView;
    // the rigidbody
    private Rigidbody2D rb;
    [HideInInspector]
    public Transform player;

    float distanceFromPlayer;
    public float despawnRange;
    // Used for invincibility
    bool invincible = false;

    public GameObject deathEffect;

    Animator animator;

    public AudioSource audioSource;
    public AudioClip hitSound;

    // this bool is to tell when the player leaves a room, so it can despawn all active enemies
    public BoolVariable playerExit;

    // Awake is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        fieldOfView = GetComponentInChildren<FieldOfVision>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        CalculateDistance();
        Movement();
        Death();
    }

    // Death. Self explanitory.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            Instantiate(deathEffect, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if (distanceFromPlayer > despawnRange)
        {
            Destroy(gameObject);
        }
        if (playerExit.Switch == true)
        {
            Destroy(gameObject);
        }
    }

    void CalculateDistance()
    {
        distanceFromPlayer = Vector2.Distance(transform.position, player.position);
    }

    void Movement()
    {
        if (fieldOfView.playerInRange)
        {
            if (player.transform.position.x >= transform.localPosition.x)
            {
                transform.localScale = new Vector2(-1, 1);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.enemySpeed * Time.deltaTime);

            }
            else if (player.transform.position.x < transform.localPosition.x)
            {
                transform.localScale = new Vector2(1, 1);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.enemySpeed * Time.deltaTime);
            }
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
