using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{

    public FloatVariable playerAmmo;
    public float ammoAmount;
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
            playerAmmo.Value += ammoAmount;
            sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }

}
