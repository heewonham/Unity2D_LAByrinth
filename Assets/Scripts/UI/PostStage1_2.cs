using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostStage1_2 : MonoBehaviour
{
    public Button mc1;
    public Button mc2;
    public Button mc3;

    public Text mcs1;
    public Text mcs2;
    public Text mcs3;

    public Image Bg;



    void Start()
    {
        Bg.gameObject.SetActive(false);

        if (ItemController.Collectables[0] == 1)//
        {
            mc1.interactable = true;
        }
        else
        {
            mc1.interactable = false;
        }

        if (ItemController.Collectables[2] == 1) //
        {
            mc2.interactable = true;
        }
        else
        {
            mc2.interactable = false;
        }

        if (ItemController.Collectables[2] == 1) //
        {
            mc3.interactable = true;
        }
        else
        {
            mc3.interactable = false;
        }
    }


    void Update()
    {
        if (Bg.gameObject.activeSelf == true&&Input.anyKeyDown)
        {
            mcs1.gameObject.SetActive(false);
            mcs2.gameObject.SetActive(false);
            mcs3.gameObject.SetActive(false);
            Bg.gameObject.SetActive(false);
        }
        if(Bg.gameObject.activeSelf == false && Input.GetKey(KeyCode.Space))
        {
            AutoFade.LoadLevel("Ending", 1, 1, Color.black);
        }
    }

    public void OnClickMC1()
    {
        Bg.gameObject.SetActive(true);

        mcs1.gameObject.SetActive(true);


    }

    public void OnClickMC2()
    {
        Bg.gameObject.SetActive(true);
        mcs2.gameObject.SetActive(true);

    }

    public void OnClickMC3()
    {
        Bg.gameObject.SetActive(true);
        mcs3.gameObject.SetActive(true);

    }
}
