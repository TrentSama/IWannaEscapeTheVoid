using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerTest : MonoBehaviour
{


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EventManagerTest.TriggerEvent("Test");
        }
    }
}
