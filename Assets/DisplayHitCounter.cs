using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayHitCounter : MonoBehaviour {

	private Text textField;

	private int objectHitCount;
	private int itemGetCount;
	private int colorBlindCount;
	private int timeCount;

	private GameObject gameManager;
	private GameCounter gameCounter;

	public Camera cam;
	private const string DISPLAY_TEXT_FORMAT = "{1}";
	void Awake() {
		textField = GetComponent<Text>();
	}

	void Start() {
		if (cam == null) {
			cam = Camera.main;
		}
		if (cam != null) {
			// Tie this to the camera, and do not keep the local orientation.
			transform.SetParent(cam.GetComponent<Transform>(), true);
		}
		gameManager = GameObject.Find ("GameManager");
		gameCounter = gameManager.GetComponent<GameCounter> ();
	}

	void LateUpdate() {
		textField.text = string.Format (DISPLAY_TEXT_FORMAT,gameCounter.score,(int)gameCounter.time);			
	}
}