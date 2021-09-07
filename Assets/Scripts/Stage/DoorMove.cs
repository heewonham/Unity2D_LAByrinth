using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{

    public bool locked;
    bool NpcNear = false;
    bool playerNear = false;

    float MoveAmount = 0.0f;
    float move = 0.0f;

    Vector2 firstpos;

    public Transform player;
    public Transform NPC;

    public AudioSource doorOpenAudio;

    float dist, dist2;

    void Awake(){
      firstpos = this.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        dist = Vector2.Distance(NPC.position, firstpos);    // NPC의 거리
        dist2 = Vector2.Distance(firstpos, player.position);    // player의 거리
        if (dist <= 4) NpcNear = true;
        else NpcNear = false;
        if (dist2 <= 6) playerNear = true;
        else playerNear = false;

        // player가 가까이 있을 경우
        if (playerNear)
        {
            if (!locked) // 잠기지 않았으면
            {
                if (MoveAmount == 0)
                {
                    if (dist2 <= 6) doorOpenAudio.GetComponent<AudioSource>().Play();
                }
                if (MoveAmount <= 1)
                {
                    MoveAmount += Time.deltaTime;
                    this.transform.Translate(0, Time.deltaTime * 5, 0);
                }
            }
        }
       else if (NpcNear) // 좀비는 아무문이나 열 수 있음.
        {
            if (locked)
            {
                this.transform.Translate(0, Time.deltaTime * 5, 0);
                this.gameObject.SetActive(false);
                Invoke("lockDoor", 3f);
            }
            else
            {
                this.transform.Translate(0, Time.deltaTime * 5, 0);
            }
        }
        else
        {
            if (MoveAmount > 0)
            {
                MoveAmount -= Time.deltaTime;
                this.transform.Translate(0, -Time.deltaTime * 5, 0);
                if (MoveAmount <= 0) MoveAmount = 0;
            }
        }
    }

    public void unlockDoor(){
      locked = false;
    }
    public void lockDoor()
    {
        this.gameObject.SetActive(true);
    }
}
