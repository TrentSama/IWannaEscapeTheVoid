﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


public class EventManagerTest : MonoBehaviour
{

    private Dictionary <string, UnityEvent> eventDictionary;

    private static EventManagerTest eventManager;

    public static EventManagerTest Instance
    {
        get

        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManagerTest)) as EventManagerTest;

                if(!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a Gameobject in your scene");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }
    void Init()
    {

        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }

    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    } 

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue (eventName, out thisEvent))
        {
            thisEvent.Invoke ();
        }
    }
}
