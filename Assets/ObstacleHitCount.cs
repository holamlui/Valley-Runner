using UnityEngine;
using System.Collections;

public class ObstacleHitCount : MonoBehaviour {

	private GameObject gameManager;
	private GameCounter gameCounter;
	private AudioSource obstacleAudio;
	private bool hit; //record the hit only once per obstacle

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gameCounter = gameManager.GetComponent<GameCounter> ();
		hit = false;
		this.gameObject.tag = "Obstacle";//For finding game objects from the Unity Engine, Not used in other scripts
		obstacleAudio = GameObject.Find("ObstacleAudio").GetComponent<AudioSource>();
	}
		
	void OnTriggerEnter(Collider obj){
		//Debug.Log (this.gameObject.layer);//Get the layer id of the item| 0-default 8-LeftEye 9-RightEye
		if (!hit) {
			obstacleAudio.Play ();
			switch (this.gameObject.layer) {
			case 8:
				gameCounter.obstacleHitCountLeft++;
				break;
			case 9:
				gameCounter.obstacleHitCountRight++;
				break;
			default:
				gameCounter.obstacleHitCountDefault++;
				break;
			}
			gameCounter.obstacleHitCount++;
			hit = true;
		}
	}
}