using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {
	public Vector3 position;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//CameraControll
		position = transform.position;
		position.x += Input.GetAxis("Horizontal");
		position.z += Input.GetAxis("Vertical");
		position.y -= Input.GetAxis ("Mouse ScrollWheel");
		transform.position = position;
	}
}
