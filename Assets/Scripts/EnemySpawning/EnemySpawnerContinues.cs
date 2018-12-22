using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerContinues : MonoBehaviour
{
    bool active;

    public GameObject enemy;

    public Transform[] spawnLocations;

    public float spawnTimer;

    float secondsTimer;


    // Update is called once per frame
    void Update()
    {

        secondsTimer += Time.deltaTime;

        if (active == true )
        {
            if (secondsTimer >= spawnTimer)
            {
                for (int t = 0; t < spawnLocations.Length; t++)
                {
                    Instantiate(enemy, spawnLocations[t].position, spawnLocations[t].rotation);
                    secondsTimer = 0;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = true;
            StartCoroutine(SpawnDelay());
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = false;
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.1f);
        for (int t = 0; t < spawnLocations.Length; t++)
        {
            Instantiate(enemy, spawnLocations[t].position, spawnLocations[t].rotation);
        }
    }

}
