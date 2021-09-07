using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{

    private AudioSource _audioSource;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
        _audioSource.volume = 0.25f;
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

     void Start()
     {

        GameObject.FindGameObjectWithTag("Music").GetComponent<BGM>().PlayMusic();
        //GameObject.FindGameObjectWithTag("Music").GetComponent<BGM>().StopMusic();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
