using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public Transform saveLocation;

    Animator anim;

    bool inTrigger;

    PlayerController player;

    AudioSource aud;
    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (inTrigger == true)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.K))
            {
                aud.Play();
                player.checkpoint = saveLocation;               
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
        }

    }

}

