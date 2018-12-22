using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageClearPickup : MonoBehaviour
{
    public string level;
    AudioSource aud;
    public BoolVariable win;
    SpriteRenderer sprite;
    public IntVariable index;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            win.Switch = true;
            index.Value = 1;
            aud.Play();
            sprite.enabled = false;
            yield return new WaitForSeconds(6);
            win.Switch = false;
            SceneManager.LoadScene(level);
        }
    }

}
