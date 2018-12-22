using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDisplay : MonoBehaviour
{

    public BulletType bullet;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = bullet.sprite;
        Debug.Log("Bullet Damage: " + bullet.damage + " Bullet Name: " + bullet.name + " Bullet Speed: " + bullet.speed);
    }

}
