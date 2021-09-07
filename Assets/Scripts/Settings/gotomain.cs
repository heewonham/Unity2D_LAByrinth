﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gotomain : MonoBehaviour
{
    public Text btm;
    public RectTransform btm_btn;

    public void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        if (RectTransformUtility.RectangleContainsScreenPoint(btm_btn, mousePos, Camera.main))
        {
            btm.text = "<color=#602625>Back to Main</color>";
            if (Input.GetMouseButtonDown(0))
            {
                btm.text = "<color=#BE2323>Back to Main</color>";

            }
            if (Input.GetMouseButton(0))
            {
                btm.text = "<color=#BE2323>Back to Main</color>";
            }
            if (Input.GetMouseButtonUp(0))
            {
                btm.text = "<color=#BE2323>Back to Main</color>";
            }
        }
        else { btm.text = "<color=#6F6F6F>Back to Main</color>"; }
    }
    public void OnClickGotoMain()
    {


        AutoFade.LoadLevel("Main Menu", 1, 1, Color.black);
    }
}
