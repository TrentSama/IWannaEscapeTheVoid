using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchActiveCamera : MonoBehaviour
{
    public CinemachineVirtualCamera roomCamera;

    public BoolVariable exitRoom;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomCamera.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            roomCamera.enabled = false;
            StartCoroutine(DespawnEnemies());
        }
    }

    IEnumerator DespawnEnemies()
    {
        exitRoom.Switch = true;
        yield return new WaitForSeconds(0.01f);
        exitRoom.Switch = false;
    }

}
