using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionEnemySpawner : MonoBehaviour
{

    public FloatVariable phaseCheck;

    public Transform[] spawners;
    public GameObject enemy;

    // used to keep track of how many seconds pass
    float timer;
    // used to tell when to spawn an enemy;
    float spawnTimer;
    // used to randomize which spot to spawn the enemy
    int spawnRandomizer;

    public BoolVariable bossActive;



    // Update is called once per frame
    void Update()
    {
        if (bossActive.Switch == true)
        {
            timer += 1;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (timer > spawnTimer)
        {
            spawnRandomizer = Random.Range(0, 2);
            Instantiate(enemy, spawners[spawnRandomizer].position, spawners[spawnRandomizer].rotation);
            timer = 0;
            RandomizeTiming();
        }
    }

    // Randomizes the time it takes before it spawns an enemy;
    void RandomizeTiming()
    {
        if (phaseCheck.Value == 0)
        {
          spawnTimer = Random.Range(600, 750);
        }
        else if (phaseCheck.Value == 1)
        {
          spawnTimer = Random.Range(400, 500);
        }
        else if (phaseCheck.Value == 2)
        {
          spawnTimer = Random.Range(250, 350);
        }
        else if (phaseCheck.Value == 3)
        {
          spawnTimer = Random.Range(100, 200);
        }
        else
        {
            spawnTimer = 0;
        }

    }

}
