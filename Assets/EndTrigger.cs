using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndTrigger : MonoBehaviour {

	private GameObject gameManager;
	private EventController eventController;

	private GameObject resultCanvas;

	private Text textField;
	private GameCounter gameCounter;
	private AudioSource winingAudio;
	private AudioSource backgroundAudio;

	private const string DISPLAY_TEXT_FORMAT = "Time: {0}\n Score: {1}";
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager");
		gameCounter = gameManager.GetComponent<GameCounter> ();
		eventController = gameManager.GetComponent<EventController> ();
		resultCanvas = GameObject.Find ("ResultCanvas");
		//textField = resultCanvas.GetComponentInChildren<Text> ();
		//textField = resultCanvas.GetComponent<Text> ();
		winingAudio = GameObject.Find("WiningAudio").GetComponent<AudioSource>();
		backgroundAudio = GameObject.Find ("BackgroundAudio").GetComponent<AudioSource> ();
	}
	void OnTriggerEnter(Collider other){
		//Debug.Log("End");
		resultCanvas.GetComponent<Canvas> ().enabled = true;
		textField = resultCanvas.GetComponentInChildren<Text> ();
		eventController.PlayerStop ();
		eventController.TimerStop ();
		eventController.ToggleReticle ();

		//Debug.Log (objectHitCount);
		//Debug.Log (itemGetCount);
		textField.text = string.Format (DISPLAY_TEXT_FORMAT, ((int)gameCounter.time).ToString (), gameCounter.score.ToString());
		gameCounter.SaveRecord ();
		backgroundAudio.Stop ();
		winingAudio.Play ();
	}
	// Update is called once per frame
	void Update () {	
	}
}