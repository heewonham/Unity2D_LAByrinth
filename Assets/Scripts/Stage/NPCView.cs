using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCView : MonoBehaviour
{
    public Transform player;

    public bool is_seen;

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.transform == player)
        {
            is_seen = true;
        }
    }

    void OnTriggerExit2D (Collider2D other)
    {
        if (other.transform == player)
        {
            is_seen = false;
        }
    }
}
