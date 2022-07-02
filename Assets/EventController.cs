using UnityEngine;
using System.Collections;

public class EventController : MonoBehaviour {

	public GameObject player;
	public GameObject gameManager;
	public GameObject reticle; //The white dot in vr for user interaction
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
	}
	
	// Update is called once per frame
	void Update () {
	}
	public void PlayerMove(){
		player.GetComponent<VRAutowalk> ().move = true;
	}
	public void PlayerStop(){
		player.GetComponent<VRAutowalk> ().move = false;
	}
	public void TimerStart(){
		gameManager.GetComponent<GameCounter> ().counting = true;
	}
	public void TimerStop(){
		gameManager.GetComponent<GameCounter> ().counting = false;
	}
	public void ToggleReticle(){
		reticle.SetActive (!reticle.activeSelf);
	} 

}
