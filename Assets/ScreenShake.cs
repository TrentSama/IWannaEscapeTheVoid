using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    CinemachineImpulseSource impulse;
    // Start is called before the first frame update
    void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
        impulse.GenerateImpulse();
    }
}
