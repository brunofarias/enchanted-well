using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour {
	public Transform backGround;
	public float speed;

	private Transform cam;
	private Vector3 previewCamPosition;

	// Use this for initialization
	void Start () {

		cam = Camera.main.transform;
		previewCamPosition = cam.position;
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float paralaxX = previewCamPosition.x - cam.position.x;
		float bgTargetX = backGround.position.x + paralaxX;
		Vector3 bgPosition = new Vector3 (bgTargetX, backGround.position.y, backGround.position.z);
		backGround.position = Vector3.Lerp (backGround.position, bgPosition, speed * Time.deltaTime);
		previewCamPosition = cam.position;
	}
}
