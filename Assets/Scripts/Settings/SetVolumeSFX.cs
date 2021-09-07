using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolumeSFX : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    public AudioSource SFX;

    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("MusicVolumeSFX", 0.75f);
    }

    public void SetLevel()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("MusicVolSFX", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolumeSFX", sliderValue);

        SFX.Play();
    }

   
}
