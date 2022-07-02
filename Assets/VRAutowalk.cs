using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]

public class VRAutowalk : MonoBehaviour {

	public float speed;
	public bool  move;
	public bool  jump;
	public float gravity = 20.0F;
	public float jumpSpeed = 8.0F;
	public float acceleration = 0.1F;

	private CharacterController controller;
	private GvrViewer gvrViewer;
	private Transform vrHead;
	private Vector3 moveDirection = Vector3.zero;
	private float timeInterval=0;

	private AudioSource jumpAudio;
	void Start () {
		controller = GetComponent<CharacterController> ();
		//gvrViewer = transform.GetChild (0).GetComponent<GvrViewer> ();
		vrHead = Camera.main.transform;
		speed = PlayerPrefs.GetFloat ("playerSpeed");
		jumpAudio = GameObject.Find ("JumpAudio").GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		timeInterval += Time.deltaTime;			
		if(move){
			//acceleration
			if (timeInterval >= 1) {
				timeInterval = 0;
				speed *= (1 + acceleration*Time.deltaTime);
			}
			if (controller.isGrounded) {	
				moveDirection = vrHead.TransformDirection (Vector3.forward);
				moveDirection *= speed;
				if (Input.GetButton ("Fire1")) {
					jumpAudio.Play ();
					moveDirection.y = jumpSpeed;
				}
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
		//if (move) {
		//	Vector3 forward = vrHead.TransformDirection (Vector3.forward);
		//	controller.SimpleMove (forward * speed);
		//}
		}
	}
}
