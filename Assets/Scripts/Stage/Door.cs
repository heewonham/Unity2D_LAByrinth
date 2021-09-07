using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public GameObject MainDoor;
    void Update()
    {
        if(!MainDoor.GetComponent<DoorMove>().locked){
            this.GetComponent<Text>().text = "Opened";
            this.GetComponent<Text>().color = new Color(0,255,0,200);
        }
    }
}
