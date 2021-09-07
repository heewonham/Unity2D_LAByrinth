using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Invoke("GoToMain", 1f);
    }

    void GoToMain(){
      AutoFade.LoadLevel("Main Menu", 1, 1, Color.black);
    }
}
