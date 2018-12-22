using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bullet", menuName = "Bullet")]
public class BulletType : ScriptableObject
{
    public float damage;
    public float speed;
    public Sprite sprite;

}
