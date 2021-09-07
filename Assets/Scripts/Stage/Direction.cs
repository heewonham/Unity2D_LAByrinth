using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    Transform npc;
    Transform player;

    float x, y, d;


    void Awake(){
      npc = GameObject.FindWithTag("zombie").transform;
      player = GameObject.FindWithTag("Player").transform;
    }

    void FixedUpdate()
    {
        this.transform.position = player.position;
        x = npc.position.x - player.position.x;
        y = npc.position.y - player.position.y;
        d = Vector3.Distance(npc.position, player.position);
        if(y < 0) this.transform.rotation = Quaternion.Euler (180,0,AngleOfTriangle(x, d, y) - 90);
        else this.transform.rotation = Quaternion.Euler (0,0,AngleOfTriangle(x, d, y) - 90);
    }

    float AngleOfTriangle(float a, float b, float c)
    {
      float cAng = (a*a+b*b- c*c)/(2*a*b);
      float rad = Mathf.Acos(cAng);
      return Mathf.Rad2Deg * rad;
    }
}
