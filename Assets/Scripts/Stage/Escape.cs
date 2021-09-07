using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : MonoBehaviour
{
  public Transform player;

  void OnTriggerEnter2D (Collider2D other)
  {
      if (other.transform == player)
      {
          AutoFade.LoadLevel("Post Stage 1-2", 1, 1, Color.black);
      }
  }
}
