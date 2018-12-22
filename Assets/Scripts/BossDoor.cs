using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    public BoolVariable bossCheck;
    public BoxCollider2D door;
    // Update is called once per frame
    void Update()
    {
        if (bossCheck.Switch == true)
        {
            door.enabled = false;
        }
    }
}
