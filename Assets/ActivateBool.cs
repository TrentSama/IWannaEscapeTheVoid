using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBool : MonoBehaviour
{
    public BoolVariable zuul;
    public BoolVariable bossKilled;

    // Start is called before the first frame update
    void Start()
    {
        zuul.Switch = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && bossKilled.Switch == false)
        {
            zuul.Switch = true;
        }
    }

}
