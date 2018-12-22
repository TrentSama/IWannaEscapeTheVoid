using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionShell : MonoBehaviour
{
    public BoolVariable dead;
    public FloatVariable phase;
    public AudioSource audioSource;
    public AudioClip hitSound;

    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;

    // Used for invincibility
    bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    // Death. Self explanitory.
    void Death()
    {
        if (enemy.enemyHP <= 0)
        {
            dead.Switch = true;
            phase.Value += 1;
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
