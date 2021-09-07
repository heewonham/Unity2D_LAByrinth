using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Input.anyKeyDown)
        {
            AutoFade.LoadLevel("Main Menu", 1, 1, Color.black);
        }
    }
}
