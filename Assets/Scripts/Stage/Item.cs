using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Item : MonoBehaviour
{
    public GameObject player;

    public GameObject[] Door = new GameObject[2];

    public Sprite[] item_Sprite = new Sprite[8];
    public int item_num;
    // 1. Switch | 2. KeyCard | 3. Battery Pack | 4. Nitro
    // 5. Stealth | 6. Direction | 7. Distance | 8. Alarm | 9. Memory Chip
    public int item_ID;

    void Start()
    {
      this.GetComponent<SpriteRenderer>().sprite = item_Sprite[item_num-1];
    }

    void Update()
    {
      //Light();
      float dist = Vector3.Distance(transform.position, player.transform.position);
      if(dist <= 5){
        player.GetComponent<Player>().item_found[item_ID] = 1;
        player.GetComponent<Player>().item_num = item_num;
      }
      else
        player.GetComponent<Player>().item_found[item_ID] = 0;
    }
}
