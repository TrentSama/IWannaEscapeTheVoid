using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSpikes : MonoBehaviour
{

    public GameObject[] spikes;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].GetComponentInChildren<SpikeObstacle>().enteredRoom = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            for (int i = 0; i < spikes.Length; i++)
            {
                spikes[i].GetComponentInChildren<SpikeObstacle>().enteredRoom = false;
            }
        }
    }

}
