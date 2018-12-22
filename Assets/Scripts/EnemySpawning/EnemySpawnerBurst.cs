using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerBurst : MonoBehaviour
{
    bool active;

    public GameObject enemy;

    public Transform[] spawnLocations;


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = true;
            if (active == true)
            {
                StartCoroutine(DelayedSpawn());
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            active = false;
        }
    }

    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(0.2f);
        for (int t = 0; t < spawnLocations.Length; t++)
        {
            Instantiate(enemy, spawnLocations[t].position, spawnLocations[t].rotation);
        }
    }

}
