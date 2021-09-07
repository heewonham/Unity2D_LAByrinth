using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{

    public Text startbtn;
    public Text setbtn;
    public Text exitbtn;

    public RectTransform startbttn;
    public RectTransform setbttn;
    public RectTransform exitbttn;

    public Image Bg1;
    public Image Bg2;


    void Start()
    {
        Bg1.gameObject.SetActive(true);
        Bg2.gameObject.SetActive(false);

        float fScaleWidth = ((float)Screen.width / (float)Screen.height) / ((float)16 / (float)9);
        Vector3 vecButtonPos = GetComponent<RectTransform>().localPosition;
        vecButtonPos.x = vecButtonPos.x * fScaleWidth;
        GetComponent<RectTransform>().localPosition = new Vector3(vecButtonPos.x, vecButtonPos.y, vecButtonPos.z);

        
    }


    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(startbttn, mousePos, Camera.main))
        {
            startbtn.text = "<color=#602625>Start Game</color>";
            if(Input.GetMouseButtonDown(0))
            {
                startbtn.text = "<color=#BE2323>Start Game</color>";
                
            }
            if (Input.GetMouseButton(0))
            {
                startbtn.text = "<color=#BE2323>Start Game</color>";
            }
            if (Input.GetMouseButtonUp(0))
            {
                startbtn.text = "<color=#BE2323>Start Game</color>";
            }
        }
        else { startbtn.text = "<color=#6F6F6F>Start Game</color>"; }





        if (RectTransformUtility.RectangleContainsScreenPoint(setbttn, mousePos, Camera.main))
        {
            setbtn.text = "<color=#602625>Settings</color>";
            if (Input.GetMouseButtonDown(0))
            {
                setbtn.text = "<color=#BE2323>Settings</color>";

            }
            if (Input.GetMouseButton(0))
            {
                setbtn.text = "<color=#BE2323>Settings</color>";
            }
            if (Input.GetMouseButtonUp(0))
            {
                setbtn.text = "<color=#BE2323>Settings</color>";
            }
        }
        else { setbtn.text = "<color=#6F6F6F>Settings</color>"; }






        if (RectTransformUtility.RectangleContainsScreenPoint(exitbttn, mousePos, Camera.main))
        {
            exitbtn.text = "<color=#602625>Exit Game</color>";
            if (Input.GetMouseButtonDown(0))
            {
                exitbtn.text = "<color=#BE2323>Exit Game</color>";

            }
            if (Input.GetMouseButton(0))
            {
                exitbtn.text = "<color=#BE2323>Exit Game</color>";
            }
            if (Input.GetMouseButtonUp(0))
            {
                exitbtn.text = "<color=#BE2323>Exit Game</color>";
            }
        }
        else { exitbtn.text = "<color=#6F6F6F>Exit Game</color>"; }
    }


    public void OnClickStartButton()
    {

        startbtn.text = "<color=#602625>Start Game</color>";

        Bg1.gameObject.SetActive(false);
        Bg2.gameObject.SetActive(true);


        AutoFade.LoadLevel("PreStage1", 2, 1, Color.black);
        
    }
   
    
    public void OnClickSettingButton()
    {
        setbtn.text = "<color=#602625>Settings</color>";

        AutoFade.LoadLevel("Sound", 1, 1, Color.black);
    }

    public void OnClickExitGameButton()
    {
        exitbtn.text = "<color=#602625>Exit Game</color>";
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying=false;
#else
        Application.Quit();
#endif
    }
    

    

}
