using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubWorldWarp : MonoBehaviour
{
    public Transform newLocation;

    Animator anim;
    public Animator canvasAnim;

    bool inTrigger;
    bool warp;

    bool active;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        active = true;
    }

    void Update()
    {
        if (inTrigger == true)
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.K))
            {             
                if (active == true)
                {
                    warp = true;
                    active = false;
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = true;
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            inTrigger = false;
            active = false;
        }

    }

    IEnumerator OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (warp == true)
            {
                warp = false;
                anim.SetTrigger("Opened");
                canvasAnim.SetTrigger("Warp");
                yield return new WaitForSeconds(1f);
                collision.transform.position = newLocation.position;
                active = true;
            }
        }
    }
}
