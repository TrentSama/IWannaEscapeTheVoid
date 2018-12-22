using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltairBomb : MonoBehaviour
{
    public float timer;
    public Transform[] bulletSpawners;
    public Transform explosionSpawner;
    public GameObject bullet;
    public GameObject explosion;

    void Start()
    {
        StartCoroutine(TimedExplosion()); 
    }


    IEnumerator TimedExplosion()
    {
        int i = 0;
        yield return new WaitForSeconds(timer);
        foreach (Transform transform in bulletSpawners)
        {
            Instantiate(bullet, bulletSpawners[i].position, bulletSpawners[i].rotation);
            i += 1;
        }
        Instantiate(explosion, explosionSpawner.position, explosionSpawner.rotation);
        Destroy(gameObject);
    }

}
