using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharController : MonoBehaviour {

public bool isWell;
public float forwardVel ;
public float forwardJump ;
public Animator anim;
Rigidbody rBody;

void Start()
	{
		rBody = GetComponent<Rigidbody>();
	}

	
void Update()
	{
		Walk();
	}

void Walk()
	{
		if(!isWell)
		{

			rBody.velocity = transform.forward  * forwardVel;
			
		}	
		else
		{
			rBody.velocity = Vector3.zero;
			forwardVel=0;
			Jump();	
			
		}
	}

 void Jump()
  {
	rBody.AddForce(transform.up * forwardJump);
	anim.Play ("Vault Over Box");	
  }


 void OnTriggerEnter(Collider other)
	{
		isWell=true;
	}

public void DestroyPlayer()
{
	SceneManager.LoadScene("GamePlay");
	Destroy(this.gameObject);
}

 
}
