using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnemySpawner : MonoBehaviour
{

    bool active;

    public GameObject enemy;

    public Transform[] spawnLocations;

    // I'll be using this to determine what the spawner does when you enter
    // 1 means spawn every enemy at once
    // 2 means randomly spawn in randomly chosen locations
    // 3 means continuesly spawn in set locations 
    [Tooltip("1 spawns all enemies at once. 2 spawns them randomly in random spots. 3 spawns continuesly in set locations")]
    public float spawnState;

    public float spawnTimer;

    float secondsTimer;

    // Start is called before the first frame update
    void Start()
    {
        secondsTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (active == true && spawnState == 2)
        {
            secondsTimer += 1; 
            if (secondsTimer >= spawnTimer)
            {
                int ran = Random.Range(0, spawnLocations.Length - 1);
                Instantiate(enemy, spawnLocations[ran].position, spawnLocations[ran].rotation);
                secondsTimer = 0;
            }
        }
        if (active == true && spawnState == 3)
        {
            secondsTimer += 1;
            if (secondsTimer >= spawnTimer)
            {
                for (int t = 0; t < spawnLocations.Length; t++)
                {
                    Instantiate(enemy, spawnLocations[t].position, spawnLocations[t].rotation);
                }
            }
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        active = true;
        if (active == true && spawnState == 1)
        {
            foreach (Transform transform in spawnLocations)
            {
                Instantiate(enemy);
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        active = false;
    }

}
