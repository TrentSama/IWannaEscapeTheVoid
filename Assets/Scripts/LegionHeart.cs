using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionHeart : MonoBehaviour
{
    public BoolVariable topRight;
    public BoolVariable topLeft;
    public BoolVariable bottomRight;
    public BoolVariable bottomLeft;

    public BoolVariable Dead;
    public BoolVariable killSwitch;
    public BoolVariable active;

    public Collider2D heartCollider;

    public FloatVariable phaseCheck;

    public GameObject lazer;

    public Transform[] lazerSpawners;

    public SpriteRenderer sprite;

    public float timeToShoot;
    float timer = 0;

    public AudioSource shoot;
    public AudioClip shootSound;

    public AudioSource audioSource;
    public AudioClip hitSound;


    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;

    // Used for invincibility
    bool invincible = false;


    // Start is called before the first frame update
    void Awake()
    {
        enemy = GetComponent<Enemy>();
        topRight.Switch = false;
        topLeft.Switch = false;
        bottomLeft.Switch = false;
        bottomRight.Switch = false;
        phaseCheck.Value = 0;
        killSwitch.Switch = false;
        Dead.Switch = false;
        active.Switch = false;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
        EnableCollider();
        timer += 1;
        if (phaseCheck.Value == 4)
        {
            SpawnAttacks();
        }
    }

    // Death. Self explanitory.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            Dead.Switch = true;
        }
        if (killSwitch.Switch == true)
        {
            Destroy(gameObject);
        }
    }

    void SpawnAttacks()
    {
        if (timer > timeToShoot && Dead.Switch == false)
        {
            for (int i = 0; i < lazerSpawners.Length; i++)
            {
                shoot.clip = shootSound;
                shoot.Play();
                Instantiate(lazer, lazerSpawners[i].position, lazerSpawners[i].rotation);
            }
            timer = 0;
        }
    }

    void EnableCollider()
    {
        if (topRight.Switch == true && topLeft.Switch == true && bottomLeft.Switch == true && bottomRight.Switch == true)
        {
            heartCollider.enabled = true;
        }
        else
        {
            heartCollider.enabled = false;
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
        while (invincible == true)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.05f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
