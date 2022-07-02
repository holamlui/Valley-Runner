using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSetting : MonoBehaviour {

	private int gameLength;
	private float playerSpeed;

	private Text gameLengthText;
	private Text playerSpeedText;

	private int colorBlindSpawnRate;
	private int itemSpawnRate;
	private int rockSpawnRate;

	private GameObject itemSpawnSlider;
	private GameObject rockSpawnSlider;
	//public int currentScene;

	//private GameObject instance;
	// Use this for initialization
	void Start () {
		gameLength = PlayerPrefs.GetInt ("gameLength", 10); //Get the game length setting, default is 10
		playerSpeed = PlayerPrefs.GetFloat ("playerSpeed", 5); //Get the player Speed setting, default is 5
		colorBlindSpawnRate = PlayerPrefs.GetInt("ColorBlindSpawnRate",20);
		itemSpawnRate = PlayerPrefs.GetInt("ItemSpawnRate",20);
		rockSpawnRate = PlayerPrefs.GetInt("RockSpawnRate",50);

		gameLengthText = GameObject.Find("LengthSetting").GetComponentInChildren<Text>();
		playerSpeedText = GameObject.Find("SpeedSetting").GetComponentInChildren<Text>();


		GameObject.Find ("LengthSlider").GetComponent<Slider> ().value = gameLength;
		GameObject.Find ("SpeedSlider").GetComponent<Slider> ().value = playerSpeed;

		itemSpawnSlider = GameObject.Find ("ItemSpawnSlider");
		rockSpawnSlider = GameObject.Find ("RockSpawnSlider");

		GameObject.Find ("ColorBlindSpawnSlider").GetComponent<Slider> ().value = colorBlindSpawnRate;
		itemSpawnSlider.GetComponent<Slider> ().value = itemSpawnRate;
		rockSpawnSlider.GetComponent<Slider> ().value = rockSpawnRate;

		gameLengthText.text = "Length: " + gameLength;
		playerSpeedText.text = "Speed: " + playerSpeed;
	}

	//Changing the slider value automatically call onValueChange function, which also change value of text
	//Temp change the setting in local variables
	public void ChangeGameLength(float newGameLength){//must use float for dynamic slider input
		gameLength = (int)newGameLength;
		gameLengthText.text = "Length: " + gameLength;
	}
	public void ChangePlayerSpeed(float newPlayerSpeed){
		playerSpeed = newPlayerSpeed;	
		playerSpeedText.text = "Speed: " + playerSpeed;
	}
	//Save the setting in playerPref for other game sessions
	public void SaveSetting(){
		PlayerPrefs.SetInt ("gameLength", gameLength);
		PlayerPrefs.SetFloat ("playerSpeed", playerSpeed);

		PlayerPrefs.SetInt("ColorBlindSpawnRate",colorBlindSpawnRate);
		PlayerPrefs.SetInt("ItemSpawnRate",itemSpawnRate);
		PlayerPrefs.SetInt("RockSpawnRate",rockSpawnRate);

		PlayerPrefs.Save ();
	}
	public void ChangeColorBlindSpawnRate(float newSpawnRate){
		colorBlindSpawnRate = (int)newSpawnRate;
		if (colorBlindSpawnRate == 100) { //if only spawn colorblind test
			itemSpawnSlider.SetActive (false);	//no need for item & rock/tree spawn settings
			rockSpawnSlider.SetActive (false);
		} else if (itemSpawnSlider.activeSelf == false) {
			itemSpawnSlider.SetActive(true);
			if (itemSpawnRate != 100) {////if not only spawn items
				rockSpawnSlider.SetActive (true);
			}
		}
	}
	public void ChangeItemSpawnRate(float newSpawnRate){
		itemSpawnRate = (int)newSpawnRate;
		if (newSpawnRate == 100) { //if only spawn items
			rockSpawnSlider.SetActive (false);
		} else {
			if (rockSpawnSlider.activeSelf == false) {
				rockSpawnSlider.SetActive (true);
			}
		}
	}
	public void ChangeRockSpawnRate(float newSpawnRate){
		rockSpawnRate = (int)newSpawnRate;
	}
	void Update(){
	}
	/*---
	void Awake(){
		if (!instance) {
			instance = this.gameObject;
		} else {
			Destroy (this.gameObject);
		}
		DontDestroyOnLoad (transform.gameObject);
	}
--*/
}
