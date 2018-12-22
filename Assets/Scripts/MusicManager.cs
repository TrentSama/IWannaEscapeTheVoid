using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> songs = new List<AudioClip>();
    public IntVariable globalSongIndex;
    public IntVariable stopMusic;
    int songIndex;

    // Start is called before the first frame update
    void Start()
    {
        globalSongIndex.Value = 0;
        songIndex = globalSongIndex.Value;
        audioSource.clip = songs[songIndex];
        audioSource.Play();
    }

    void Update()
    {
       if (songIndex != globalSongIndex.Value)
        {
            songIndex = globalSongIndex.Value;
            audioSource.clip = songs[songIndex];
            audioSource.Play();
        }
       if (stopMusic.Value == 1)
        {
            audioSource.Stop();
            stopMusic.Value = 0;
        }
    }

}
