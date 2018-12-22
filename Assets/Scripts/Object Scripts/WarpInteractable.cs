using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpInteractable : MonoBehaviour
{
    public string warpLocation;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        active = true;
    }
    bool inTrigger;
    bool warp;

    bool active;

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
                animator.SetTrigger("Warp");
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene(warpLocation);
                active = true;
            }
        }
    }
}
