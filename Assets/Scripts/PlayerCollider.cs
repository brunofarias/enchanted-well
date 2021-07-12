using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    if (col.tag == "Die")
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    if (coll.gameObject.tag == "Obstacle")
    {
      Rigidbody2D rb = coll.gameObject.GetComponent<Rigidbody2D>();

      if (Player.isChild || Player.isOld)
      {       
        rb.bodyType = RigidbodyType2D.Static;  
      }
      else {
        rb.bodyType = RigidbodyType2D.Dynamic;  
      }
    }

  }

  void OnCollisionExit2D(Collision2D coll)
  {
    if (coll.gameObject.tag == "Platform") transform.parent = null;  
  }
}
