using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsourse;
    public AudioSource btnsource;

   
    public void SetMusicVolume(float volume)
    {
        musicsourse.volume = volume;
    }

    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
    }

    public void OnSfx()
    {
        btnsource.Play();
    }

}
