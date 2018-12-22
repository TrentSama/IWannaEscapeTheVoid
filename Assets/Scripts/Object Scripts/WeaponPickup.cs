using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public PlayerWeaponsInventory inventory;
    public GameObject weaponType;
    public FloatVariable ammoType;
    AudioSource audioSource;
    public AudioClip pickupSound;
    SpriteRenderer sprite;

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = pickupSound;
            audioSource.Play();
            inventory.weapons.Add(weaponType);
            inventory.ammo.Add(ammoType);
            sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }

}
