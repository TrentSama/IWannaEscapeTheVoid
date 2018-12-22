using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingShooterEnemy : MonoBehaviour
{
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // the rigidbody
    private Rigidbody2D rb;
    // References to global vars
    public FloatVariable playerHP;
    public FloatVariable bulletsInScene;

    // The first var is a timer used to check how long it's been moving, the second is used to randomly determine when it should turn
    float moveTime = 0;
    float turnTime;

    // Just a variable I used to get a random number later.
    private int ran;

    // This is the transform for where the enemy will shoot out a projectile
    public Transform bulletSpawner;
    public GameObject bullet;

    // These are bools for the enemy directions, and it's invincibilty state.
    bool canMove = true;
    bool moveLeft = false;
    bool moveRight = false;
    bool invincible = false;

    // Awake is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        RandomizeDirection();
        RandomizeTiming();
    }
    // Update is called every frame
    void Update()
    {
        Death();
    }

    // FixedUpdate is called once per physics update
    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        // These just move the enemy left and right. Once the moveTime reaches the turntime it will switch 
        // directions and randomize the amount of time it will be moving before turning again.
        if (canMove)
        {
            if (moveLeft)
            {
                transform.localScale = new Vector2(1, 1);
                rb.velocity = new Vector2(1 * enemy.enemySpeed, rb.velocity.y);
                moveTime += 1;
                if (moveTime >= turnTime)
                {
                    moveTime = 0;
                    StartCoroutine(Shoot());
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
                    StartCoroutine(Shoot());
                    ChangeDirection();
                    RandomizeTiming();
                }
            }
            else
            {
                RandomizeDirection(); // this is just a safeguard for if it doesn't have a direction, it just sets a random direction again.
            }
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
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

    IEnumerator Shoot()
    {
        Instantiate(bullet, bulletSpawner.position, bulletSpawner.rotation);
        canMove = false;
        yield return new WaitForSeconds(1);
        canMove = true;
    }

    // Randomizes the time it takes before it turns.
    void RandomizeTiming()
    {
        turnTime = Random.Range(120, 240);
    }

    //Used for collision. Using an Ienumerator so I can have invincibility time.
    public IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (!invincible)
        {
            if (collision.tag == "Bullet")
            {
                invincible = true;
                enemy.enemyHP -= collision.GetComponent<Bullet>().damage; // Getting the damage value of the bullet. 
                yield return new WaitForSeconds(collision.GetComponent<Bullet>().invincibleTime); // an enemy invincibility is based on the ammo type.
                invincible = false;
            }
        }
    }
    // Death. ezpz.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }


}
