using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{

    public GameObject[] hpPickup;
    public GameObject[] ammoPickup;
    public Transform[] spawnLocations;
    bool spawnedObject;
    public BoolVariable iceMissleObtained;
    public BoolVariable catObtained;
    int random;
    public FloatVariable playerHP;

    private void Awake()
    {
        spawnedObject = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (spawnedObject == false)
            {
                if (iceMissleObtained.Switch == true && catObtained.Switch == false)
                {
                    
                    if (playerHP.Value > 5)
                    {
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(ammoPickup[1], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    else if (playerHP.Value <= 5)
                    {
                        random = Random.Range(0, hpPickup.Length);
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(hpPickup[random], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    spawnedObject = true;
                }
                else if (iceMissleObtained.Switch == false && catObtained.Switch == true)
                {                   
                    if (playerHP.Value >= 6)
                    {
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(ammoPickup[0], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    else if (playerHP.Value <= 5)
                    {
                        random = Random.Range(0, hpPickup.Length);
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(hpPickup[random], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    spawnedObject = true;
                }
                else if(iceMissleObtained.Switch == true && catObtained.Switch == true)
                {
                    random = Random.Range(0, 100);
                    if (playerHP.Value > 5 && random <= 50)
                    {
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(ammoPickup[0], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    else if (playerHP.Value > 5 && random >= 51)
                    {
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(ammoPickup[1], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    else if (playerHP.Value <= 5)
                    {
                        random = Random.Range(0, hpPickup.Length);
                        for (int t = 0; t < spawnLocations.Length; t++)
                        {
                            Instantiate(hpPickup[random], spawnLocations[t].position, spawnLocations[t].rotation);
                        }
                    }
                    spawnedObject = true;
                }
                else
                {
                    random = Random.Range(0, hpPickup.Length);
                    for (int t = 0; t < spawnLocations.Length; t++)
                    {
                        Instantiate(hpPickup[random], spawnLocations[t].position, spawnLocations[t].rotation);
                    }
                    spawnedObject = true;
                }
            }
        }
    }

}
