using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionDeath : MonoBehaviour
{
    public BoolVariable deathTrigger;
    public BoolVariable killBoss;
    public Transform[] spawners;
    public GameObject explosion;
    bool active;
    public IntVariable music;

    private void Awake()
    {
        active = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (active == true && deathTrigger.Switch == true)
        {
            StartCoroutine(BossDead());
        }       
    }

    IEnumerator BossDead()
    {
        active = false;
        {
            {
                for (int i = 0; i < 25; i++)
                {
                    int r = Random.Range(0, spawners.Length);
                    Instantiate(explosion, spawners[r].position, spawners[r].rotation);
                    yield return new WaitForSeconds(0.1f);
                }
                music.Value = 1;
                killBoss.Switch = true;
            }
        }
    }

}
