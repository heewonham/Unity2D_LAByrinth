using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoStage : MonoBehaviour
{
    public void Stage1(){
      SceneManager.LoadScene("Stage1");
    }
}
