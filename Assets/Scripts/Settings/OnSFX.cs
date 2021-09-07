using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSFX : MonoBehaviour
{
    public AudioSource SFX;
    

    public void OnClickSFX()
    {
        SFX.Play();
    }
}
