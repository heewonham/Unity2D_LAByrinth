using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public Transform[] items = new Transform[11];
    Transform item;

    public Transform player;

    public int tutNum;

    Vector3 startingpoint = new Vector3(0,0,0);
    Vector3 nextpoint = new Vector3(3,0,0);

    void Start()
    {

        if(tutNum != 99 && tutNum != 98) item = items[tutNum];
    }

    // Update is called once per frame
    void Update()
    {
      if(tutNum == 99) {
        float dist = Vector2.Distance(startingpoint, player.position);
        if(dist < 1f) showText(99);
        else showText(100);
      }
      else if(tutNum == 98){
        float dist = Vector2.Distance(nextpoint, player.position);
        if(dist < 1f) showText(98);
        else showText(100);
      }
      else{
        if(item != null){
          float dist = Vector2.Distance(item.position, player.position);
          if(dist < 3f) showText(tutNum);
          else showText(100);
        }
        else showText(100);
      }
    }

    void showText(int a){
      // 0 hide, 1 collectable, 2 cardkey, 3 battery, 4 door
      if(a == 100) this.GetComponent<Text>().text = "";
      else if(a == 99) this.GetComponent<Text>().text = "'F'key를 누르면 플래쉬가 켜집니다. \n WASD키로 움직이세요!\n방향키는 플래시의 방향을 결정합니다.";
      else if(a == 98) this.GetComponent<Text>().text = "'Shift'를 눌면 빠르게 달릴 수 있습니다.\n'R'key는 좀비가 가까이 왔을 때 배터리를 사용하여 공격할 수 있습니다.";
      else if(a == 0) this.GetComponent<Text>().text = "'Space' 누르면, 숨을 수 있습니다. \n좀비는 숨었을 때, 당신을 찾을 수 없습니다.";
      else if(a == 1) this.GetComponent<Text>().text = "'Space' 누르면, 메모리칩을 주울 수 있습니다.\n메모리칩의 내용은 방을 나간 뒤 확인할 수 있습니다.\n메모리칩을 모아 엔딩을 확인하세요";
      else if(a == 2) this.GetComponent<Text>().text = "'Space' 누르면, 'Key Card'를 주울 수 있습니다.\n카드 키는 잠긴 문을 열 수 있습니다.(스페이스 사용)";
      else if(a == 3) this.GetComponent<Text>().text = "'Space' 누르면, 'Battery'를 주울 수 있습니다.\n 플래시는 무한정이 아닙니다. 배터리를 주워서 충전하세요.";
      else if(a == 4) this.GetComponent<Text>().text = "'Space' 누르면, 해당 키에 해당하는 잠긴 문을 열 수 있습니다.\n이 방을 열면 숨거나 도망가세요.";
      else if(a == 5) this.GetComponent<Text>().text = "'Space' 누르면, 'Nitro'을 주울 수 있습니다.\n'Nitro'는 잠시동안 속도를 증가시킵니다.";
      else if(a == 6) this.GetComponent<Text>().text = "'Space' 누르면, 'Stealth'을 주울 수 있습니다.\n'Stealth' 잠시 동안 당신의 위치를 제거할 수 있습니다.";
      else if(a == 7) this.GetComponent<Text>().text = "'Space' 누르면, 'Direction Sonar'을 주울 수 있습니다.\n'Direction Sonar'는 잠시동안 좀비가 있는 방향을 확인할 수 있습니다.";
      else if(a == 8) this.GetComponent<Text>().text = "'Space' 누르면, 'Distance Sonar'을 주울 수 있습니다.\n'Distance Sonar'는 소리와 비례하는 좀비의 거리를 확인할 수 있습니다.";
      else if(a == 9) this.GetComponent<Text>().text = "'Space' 누르면, 'Alarm Clock'을 주울 수 있습니다.\n이 아이템 사용시 좀비는 현재 위치로 다가옵니다.\n빨리 도망가세요";
      else if(a == 10 || a == 11 || a == 12) this.GetComponent<Text>().text = "'Space' 누르면, 스위치가 작동됩니다.\n 스위치 작동은 잠겨있는 어딘 가의 문을 엽니다.";
    }
}
