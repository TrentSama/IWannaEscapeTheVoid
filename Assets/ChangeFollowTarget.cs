using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChangeFollowTarget : MonoBehaviour
{
    GameObject player;
    CinemachineVirtualCamera vcam;
    

    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = player.transform;
        vcam.LookAt = player.transform;
    }
}
