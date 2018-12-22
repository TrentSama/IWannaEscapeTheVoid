using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrail : MonoBehaviour
{
    void Destroy()
    {
        Destroy(gameObject);
    }
}
