using UnityEngine;
using System.Collections;

public class ColorBlindFilterManager : MonoBehaviour {
	// This Script is only used in the Main Game for loading the colorblind filter preset to the Main Camera
	// Use this for initialization

	public GameObject mainCamera;
	private string colorBlindFilterType;//normal, red, green
	private float colorBlindFilterIntensity;//0 ~ 1
	private jp.gulti.ColorBlind.ColorBlindnessSimulator colorBlindFilter;//The colorBlind simulator component attached to the main camera

	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		colorBlindFilterType = PlayerPrefs.GetString ("ColorBlindFilterType", "normal");
		colorBlindFilterIntensity = PlayerPrefs.GetFloat ("ColorBlindFilterIntensity", 1.0f);
		colorBlindFilter = mainCamera.GetComponent<jp.gulti.ColorBlind.ColorBlindnessSimulator> ();
		LoadColorBlindFilter ();
	}
	private void LoadColorBlindFilter(){
		if (colorBlindFilterType == "red") 
			colorBlindFilter.BlindMode = jp.gulti.ColorBlind.ColorBlindnessSimulator.ColorBlindMode.Protanope;
		else if (colorBlindFilterType == "green")
			colorBlindFilter.BlindMode = jp.gulti.ColorBlind.ColorBlindnessSimulator.ColorBlindMode.Deuteranope;
		
		colorBlindFilter.BlindIntensity = colorBlindFilterIntensity;
		if (colorBlindFilterType == "normal") 
			colorBlindFilter.enabled = false;		
	}
	public void ShiftCameraLeft(float amount){
		GameObject.Find ("Main Camera Left").transform.Rotate (Vector3.up * -amount);
		//Translate (amount*Vector3.left);	

	}
	public void ShiftCameraRight(float amount){
		GameObject.Find ("Main Camera Right").transform.Rotate (Vector3.up * amount);
	}
}
