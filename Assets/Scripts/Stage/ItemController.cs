using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public GameObject player;
    public NPC npc;
    GameObject[] Doors = new GameObject[2];
    GameObject[] Door_room = new GameObject[6];
    public static int[] Collectables = new int[3];

    public int Item; // 현재 들고 있는 아이템
    public int[] Cards = new int[6]; // 현재 들고 있는 카드키들
    public GameObject[] items = new GameObject[6]; // 아이템 목록, itemID

    public GameObject BatteryControl;

    public GameObject dirSonar;
    GameObject DirPrefab;

    public GameObject Alarm;
    GameObject alarmPrefab;

    public AudioSource[] itemAudio = new AudioSource[5];

    public Sprite changeSwitch;

    public Zsound ZS;

    public void ItemControl(int a, int b)
    {
      if(a == 1){
        itemAudio[2].Play();
        Switch(b);
      }

      else if(a == 2){
        for(int i = 0; i < Cards.Length; i++){
          if(Cards[i] != 0 && Cards[i] != 99) continue;
          Cards[i] = b;
          Doors = items[b].GetComponent<Item>().Door;
          Door_room[i] = Doors[0];
          break;
        }
        itemAudio[4].Play();
        DestroyItem(b);
      }

      else if(a == 3){
        itemAudio[3].Play();
        Battery();
        DestroyItem(b);
      }

      else if(a == 9 || a == 10 || a == 11){
        Collectable(b);
        DestroyItem(b);
        itemAudio[4].Play();
      }

      else{
        Debug.Log(a);
        if(Item == 0)
        {
          itemAudio[4].Play();
          Item = a;
          DestroyItem(b);
        }
        else Debug.Log("Full");
      }
    }

    void Switch(int n)
    {
      items[n].GetComponent<SpriteRenderer>().sprite = changeSwitch;
      Doors = items[n].GetComponent<Item>().Door;
      int door_num = Doors.Length;
      for(int i = 0; i < door_num; i++) Doors[i].GetComponent<DoorMove>().unlockDoor();
    }

    void Collectable(int a)
    {
      Collectables[a-16] = 1;
    }

    public void ItemUse(int a) // a = 아이템 넘버, b = 아이템칸 넘버
    {
      if(a == 4) NitroCombustor();
      else if(a == 6) DirectionSonar();
      else if(a == 8) AlarmClock();
      Item = 0;
    }

    public void CardKey()
    {
      for(int i = 0; i < Cards.Length; i++){
        int CardNum = Cards[i];
        if(Cards[i] == 0 && Cards[i] == 99) break;
        if(Door_room[i] == null) break;
        float dist = Vector3.Distance(Door_room[i].transform.position, player.transform.position);
        if(dist <= 6){
          Debug.Log("Unlock!");
          Door_room[i].GetComponent<DoorMove>().unlockDoor();
          Cards[i] = 99;
        }
      }
    }

    void Battery()
    {
      BatteryControl.GetComponent<BatteryController>().BatteryUp();
    }

    void NitroCombustor()
    {
      player.GetComponent<Player>().NitroOn();
    }

    void Steath(){
      player.GetComponent<Player>().stealthS = 1/2;
      Invoke("StealthEnd", 5f);
    }

    void StealthEnd(){
      player.GetComponent<Player>().stealthS = 1;
    }

    void DirectionSonar(){
      DirPrefab = Instantiate(dirSonar, new Vector3(0, 0, 0), Quaternion.identity, GameObject.FindGameObjectWithTag("Player").transform);
      Invoke("DestroyDir", 5f);
    }
    void DestroyDir(){
      Destroy(DirPrefab);
    }
    void DistanceSonar(){
      ZS.DistanceSonar();
    }

    void AlarmClock(){
      alarmPrefab = Instantiate(Alarm, player.transform.position, Quaternion.identity);
      itemAudio[0].Play();
      Invoke("DestroyAlarm", 5f);
    }

    void DestroyAlarm(){
      npc.is_alarm = false;
      Destroy(alarmPrefab);
    }

    void DestroyItem(int b)
    {
      Destroy(items[b]);
      player.GetComponent<Player>().item_found[b] = 0;
    }
}
