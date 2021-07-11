using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
  public PlayerController Player;
  public Vector3 lastPosition;

  void Start()
  {
    lastPosition = transform.position;
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    if (col.tag == "Enemy")
    {
      Player.ApplyDie();
    }

    if (col.tag == "CheckPoint")
    {
      lastPosition = transform.position;
    }
  }

  void OnCollisionEnter2D(Collision2D coll)
  {
    if (coll.gameObject.tag == "Platform")
    {
      if (Player.transform.position.y > coll.transform.position.y)
      {
        transform.parent = coll.transform;
      }
    }

    if (coll.gameObject.layer == 6) Player.isGrounded = true;
    
  }

  void OnCollisionExit2D(Collision2D coll)
  {
    if (coll.gameObject.tag == "Platform") transform.parent = null;
  }
}
