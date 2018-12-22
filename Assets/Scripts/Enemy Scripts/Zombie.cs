using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // the rigidbody
    private Rigidbody2D rb;
    public Collider2D col;
    public GameObject deathEffect;
    // The first var is a timer used to check how long it's been moving, the second is used to randomly determine when it should turn
    float moveTime = 0;
    float turnTime;
    [HideInInspector]
    public float deathTimer = 0;

    // Just a variable I used to get a random number later.
    private int ran;

    // These are bools for the enemy directions, and it's invincibilty state.
    bool moveLeft = false;
    bool moveRight = false;
    bool invincible = false;
    bool canMove = false;

    public AudioSource audioSource;
    public AudioClip hitSound;

    // this bool is to tell when the player leaves a room, so it can despawn all active enemies
    public BoolVariable playerExit;

    // Awake is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        StartCoroutine(WaitBeforeMoving());
        RandomizeDirection();
        RandomizeTiming();
    }
    // Update is called every frame
    void Update()
    {
        deathTimer += 1;
        Death();
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        if (canMove == true){
            Movement();
        }
    }

    public void Movement()
    {
        // These just move the enemy left and right. Once the moveTime reaches the turntime it will switch 
        // directions and randomize the amount of time it will be moving before turning again.
        if (moveLeft)
        {
            transform.localScale = new Vector2(1, 1);
            rb.velocity = new Vector2(1 * enemy.enemySpeed, rb.velocity.y);
            moveTime += 1;
            if (moveTime >= turnTime)
            {
                moveTime = 0;
                ChangeDirection();
                RandomizeTiming();
            }
        }
        else if (moveRight)
        {
            transform.localScale = new Vector2(-1, 1);
            rb.velocity = new Vector2(-1 * enemy.enemySpeed, rb.velocity.y);
            moveTime += 1;
            if (moveTime >= turnTime)
            {
                moveTime = 0;
                ChangeDirection();
                RandomizeTiming();
            }
        }
        else
        {
            RandomizeDirection(); // this is just a safeguard for if it doesn't have a direction, it just sets a random direction again.
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(2);
        col.enabled = true;
        canMove = true;
    }

    // Changes the direction the enemy is moving, simple stuff
    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        moveRight = !moveRight;
    }
    // When a wanderer spawns it needs a direction to move at first, so this randomizes it.
    void RandomizeDirection()
    {
        ran = Random.Range(0, 20);
        if (ran <= 20 && ran >= 10)
        {
            moveLeft = true;
        }
        else if (ran >= 0 && ran < 10)
        {
            moveRight = true;
        }
    }
    // Randomizes the time it takes before it turns.
    void RandomizeTiming()
    {
        turnTime = Random.Range(240, 480);
    }
    //Used for collision. Using an Ienumerator so I can have invincibility time.
    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            if (canMove == true)
            {
                if (collision.tag == "Bullet" && col.enabled == true)
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

    // Death. ezpz.
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
        if (deathTimer > 2000)
        {
            Destroy(gameObject);
        }
    }
}
