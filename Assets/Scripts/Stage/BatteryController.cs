using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryController : MonoBehaviour
{
    public GameObject player;
    public Image Battery;
    bool is_light;
    float CurBattery;
    float maxBattery = 100f;
    float BatteryLeft;
    void Awake()
    {
        BatteryLeft = maxBattery - 25f;
    }
    void Update()
    {

        is_light = player.GetComponent<Player>().is_light;
        if(!is_light){
          BatteryLeft -= Time.deltaTime;
          if(BatteryLeft >= maxBattery) BatteryLeft = maxBattery;
        }
        if(BatteryLeft <= 0f) BatteryLeft = 0f; // 문제 없음
        CurBattery = BatteryLeft/maxBattery;
        if(CurBattery >= 0.75f) Battery.fillAmount = 1f;
        else if(CurBattery >= 0.5f) Battery.fillAmount = 0.75f;
        else if(CurBattery >= 0.25f) Battery.fillAmount = 0.5f;
        else if(CurBattery >= 0.1f) Battery.fillAmount = 0.25f;
        else if(CurBattery > 0f) Battery.fillAmount = 0.1f;
        else Battery.fillAmount = 0f;

        if(CurBattery == 0) player.GetComponent<Player>().is_battery = false;
        else player.GetComponent<Player>().is_battery = true;
    }

    public void BatteryUp()
    {
      BatteryLeft += maxBattery/5;
    }
    public void AttackUse(){
      BatteryLeft -= maxBattery/5;
    }
}
