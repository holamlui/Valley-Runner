using UnityEngine;
using System.Collections;

public class ItemGetCount : MonoBehaviour {

	private GameObject gameManager;
	private GameCounter gameCounter;
	private AudioSource itemAudio;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gameCounter = gameManager.GetComponent<GameCounter> ();
		this.gameObject.tag = "Items";
		itemAudio = GameObject.Find ("ItemAudio").GetComponent<AudioSource> ();
	}

	void OnTriggerEnter(Collider obj){
		//Debug.Log (this.gameObject.layer);//Get the layer id of the item| 0-default 8-LeftEye 9-RightEye
		itemAudio.Play ();
		switch (this.gameObject.layer) {
		case 8:
			gameCounter.itemGetCountLeft++;
			break;
		case 9:
			gameCounter.itemGetCountRight++;
			break;
		default:
			gameCounter.itemGetCountDefault++;
			break;
		}		
		gameCounter.itemGetCount++;//Add the number of item get in the game counter
		DestroyObject (this.gameObject); //Destory the item after the player collects it

	}	
}
