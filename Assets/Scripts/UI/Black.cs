using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Black : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AutoFade.LoadLevel("Startup Screen", 1, 1, Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
