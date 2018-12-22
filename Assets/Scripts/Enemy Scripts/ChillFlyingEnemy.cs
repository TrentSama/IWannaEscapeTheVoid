﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChillFlyingEnemy : MonoBehaviour
{
    // All the spawners the enemy can shoot from.
    public Transform[] bulletSpawners;
    // The bullet the enemy spawns 
    public GameObject bullet;
    // Reference to the script that holds the stats every enemy uses.
    private Enemy enemy;
    // reference to the script that tells us if the player is in range
    private FieldOfVision fieldOfView;

    // A variable for when the enemy can shoot and another that's just a generic timer
    public int timeBetweenShots;
    float timer;

    // Used for invincibility
    bool invincible = false;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        fieldOfView = GetComponentInChildren<FieldOfVision>();
    }

    // Update is called once per frame
    void Update()
    {
        InscreaseTimer();
        Shoot();
        Death();
    }


    void Shoot()
    {
        if (timer >= timeBetweenShots)
        {
            if (fieldOfView.playerInRange == true)
            {               

                Instantiate(bullet, bulletSpawners[0].position, bulletSpawners[0].rotation);
                Instantiate(bullet, bulletSpawners[1].position, bulletSpawners[1].rotation);
                Instantiate(bullet, bulletSpawners[2].position, bulletSpawners[2].rotation);
                Instantiate(bullet, bulletSpawners[3].position, bulletSpawners[3].rotation);
                Instantiate(bullet, bulletSpawners[4].position, bulletSpawners[4].rotation);
                Instantiate(bullet, bulletSpawners[5].position, bulletSpawners[5].rotation);


            }
            timer = 0;
        }
    }

    // Increases a local timer for this specific enemy
    void InscreaseTimer()
    {
        if (fieldOfView.playerInRange == true)
        {
         timer += 0.1f;
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
                enemy.enemyHP -= collision.GetComponent<Bullet>().damage; // Getting the damage value of the bullet. 
                yield return new WaitForSeconds(collision.GetComponent<Bullet>().invincibleTime); // an enemy invincibility is based on the ammo type.
                invincible = false;
            }
        }
    }

}