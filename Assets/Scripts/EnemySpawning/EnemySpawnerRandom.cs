using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerRandom : MonoBehaviour
{
    bool active;

    public GameObject enemy;

    public Transform[] spawnLocations;

    public float spawnTimer;

    float secondsTimer;

    // Update is called once per frame
    void Update()
    {

        secondsTimer += 1;

        if (active == true)
        {
            if (secondsTimer >= spawnTimer)
            {
                int ran = Random.Range(0, spawnLocations.Length);
                Instantiate(enemy, spawnLocations[ran].position, spawnLocations[ran].rotation);
                secondsTimer = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = false;
        }
    }

}
