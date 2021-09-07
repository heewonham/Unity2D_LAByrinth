using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    NPC victim;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Zombie"|| collision.name == "NPC")
        {
            Debug.Log("hi");
            NPC npc = collision.gameObject.GetComponent<NPC>();
            victim  = npc;
            npc.is_Attacked = true;
        }
    }
}
