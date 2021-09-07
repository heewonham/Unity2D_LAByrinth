using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour
{
    public GameObject hideplayer;
    public GameObject player;
    public Player p;
    public FieldView flash1;
    public FieldView flash2;
    float prevfov;

    public void Hide()
    {
        if (!p.is_Hide)
        {
            hideplayer.SetActive(true);
            player.layer = 10;
            prevfov = flash2.fov;
            flash1.fov = flash2.fov = 0;
        }
        else
        {
            hideplayer.SetActive(false);
            player.layer = 8;
            flash1.fov = 360;
            flash2.fov = prevfov;
        }
            
    }
}
