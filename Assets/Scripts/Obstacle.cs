using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
  private Rigidbody2D rb;
  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    rb.bodyType = RigidbodyType2D.Static;
  }

  
}
