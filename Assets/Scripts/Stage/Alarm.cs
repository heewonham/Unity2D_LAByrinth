using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    Transform player;
    GameObject npc;
    Transform npcPos;
    float dist;
    void Awake()
    {
      npc = GameObject.FindWithTag("zombie");
      npcPos = npc.transform;
      player = GameObject.FindWithTag("Player").transform;
      this.transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      dist = Vector2.Distance(player.position, npcPos.position);
      if(dist <= 30){
        npc.GetComponent<NPC>().is_alarm = true;
        npc.GetComponent<NPC>().alarmPos = this.transform;
      }
    }
}
