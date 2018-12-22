using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healAmount;
    public FloatVariable playerHP;
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
            playerHP.Value += healAmount;
            sprite = GetComponent<SpriteRenderer>();
            sprite.enabled = false;
            yield return new WaitForSeconds(1);
            Destroy(gameObject);
        }
    }

}
