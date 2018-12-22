using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvanceToScene : MonoBehaviour
{
    public Animator anim;
    AudioSource aud;
    bool active;

    private void Awake()
    {
        aud = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(NewScene());
        }
    }

    IEnumerator NewScene()
    {
        anim.SetTrigger("Fade");
        aud.Play();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("HubWorld");
    }
}
