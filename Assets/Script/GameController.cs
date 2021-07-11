using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public Transform playerTransform;
	public  Transform LimiteCamLeft, LimiteCamRight, LimiteCamUp, LimiteCamDown;
	public float speedCam;
	public AudioSource SFXSource;
	public AudioSource musicSource;
	public AudioClip sfxJump;
	public AudioClip sfxSword;
	public AudioClip sfxHit;
	public AudioClip sfxDie;
	public AudioClip sfxShout;
	public AudioClip[] sfxsteps;






	private Camera cam;

	// Use this for initialization
	void Start () {
		 
		cam = Camera.main;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void LateUpdate()
	{
		CamController ();
	}

	void CamController()
	{
		float posCamX= playerTransform.position.x;
		float posCamY= playerTransform.position.y;
		if (cam.transform.position.x < LimiteCamLeft.position.x && playerTransform.position.x < LimiteCamLeft.position.x) 
		{
			posCamX = LimiteCamLeft.position.x;
		}
		else if (cam.transform.position.x > LimiteCamRight.position.x && playerTransform.position.x > LimiteCamRight.position.x)
		{
			posCamX = LimiteCamRight.position.x;
		}
		if (cam.transform.position.y < LimiteCamDown.position.y && playerTransform.position.y < LimiteCamDown.position.y)
		{
			posCamY = LimiteCamDown.position.y;

		}
		if (cam.transform.position.y > LimiteCamUp.position.y && playerTransform.position.y > LimiteCamUp.position.y)
		{
			posCamY = LimiteCamUp.position.y;
		}

		Vector3 posCam = new Vector3 (posCamX, posCamY, cam.transform.position.z);
		cam.transform.position = Vector3.Lerp(cam.transform.position,posCam,speedCam * Time.deltaTime);

	}

	public void playSFX (AudioClip sfxClip, float volume)
	{
		SFXSource.PlayOneShot (sfxClip, volume);
	}



}
