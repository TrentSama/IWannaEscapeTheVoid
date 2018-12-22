using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossDoorEntrance : MonoBehaviour
{
    Animator anim;
    public bool doorOpen;
    public BoolVariable bossDead;
    public Collider2D exitCollider;

    private void Awake()
    {
        exitCollider.enabled = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (bossDead.Switch == true)
        {
            exitCollider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
           // doorOpen = true;
            anim.SetBool("Open", true);
        }
    }

    private IEnumerator OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            yield return new WaitForSeconds(1);
          //  doorOpen = false;
            anim.SetBool("Open", false);
        }
    }


}
