using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // reference to the script that tells us if the player is in range
    private FieldOfVision fieldOfView;
    // the rigidbody
    private Rigidbody2D rb;

    public Transform player;

    // Used for invincibility
    bool invincible = false;


    // Awake is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<Enemy>();
        fieldOfView = GetComponentInChildren<FieldOfVision>();
    }
    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Movement();
        Death();
    }

    // Death. Self explanitory.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        if (fieldOfView.playerInRange)
        {
            if (player.transform.position.x >= transform.localPosition.x)
            {
                transform.localScale = new Vector2(1, 1);
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.enemySpeed * Time.deltaTime);

            }
            else if (player.transform.position.x < transform.localPosition.x)
            {
                transform.localScale = new Vector2(-1, 1);
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
                enemy.enemyHP -= collision.GetComponent<Bullet>().damage; // Getting the damage value of the bullet. 
                yield return new WaitForSeconds(collision.GetComponent<Bullet>().invincibleTime); // an enemy invincibility is based on the ammo type.
                invincible = false;
            }
        }
    }

}
