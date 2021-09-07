using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zanim : MonoBehaviour
{
    public NPC npc;
    public Transform p;
    Animator anim;

    void Awake()
    {
        anim = GetComponent <Animator>();
    }
    public void CHk()
    {
        if(npc.nextPy - p.position.y < -0.1f)
            anim.SetBool("is_Down", true);
        else
            anim.SetBool("is_Down", false);

    }
}
