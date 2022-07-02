using UnityEngine;
using System.Collections;

public class ColorBlindCount : MonoBehaviour {

	// Use this for initialization
	private GameObject gameManager;
	private GameCounter gameCounter;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gameCounter = gameManager.GetComponent<GameCounter> ();
	}
	void OnTriggerEnter(Collider obj){
		if (this.gameObject.tag.ToString() == "RedBlindChoice")
			gameCounter.redBlindCount++;
		else if (this.gameObject.tag.ToString() == "GreenBlindChoice")
			gameCounter.greenBlindCount++;
		else if (this.gameObject.tag.ToString() == "NormalVisionChoice")
			gameCounter.notBlindCount++;		
	}
}
