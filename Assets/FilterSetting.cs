using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FilterSetting : MonoBehaviour {
	
	private string colorBlindFilterType;//normal, red, green
	private float colorBlindFilterIntensity;//0 ~ 1
	private GameObject colorBlindFilterIntensitySlider;
	private jp.gulti.ColorBlind.ColorBlindnessSimulator colorBlindFilter;//The colorBlind simulator component
	private Text filterIntensityText;

	// Use this for initialization
	void Start () {
		filterIntensityText = GameObject.Find("IntensitySetting").GetComponentInChildren<Text>();
		colorBlindFilterType = PlayerPrefs.GetString ("ColorBlindFilterType", "normal");
		colorBlindFilterIntensity = PlayerPrefs.GetFloat ("ColorBlindFilterIntensity", 1.0f);
		colorBlindFilter = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<jp.gulti.ColorBlind.ColorBlindnessSimulator> ();
		colorBlindFilterIntensitySlider = GameObject.Find ("FilterIntensitySlider");
		colorBlindFilterIntensitySlider.GetComponent<Slider> ().value = colorBlindFilterIntensity;

		ChangerFilterOnStart ();
	}
	private void ChangerFilterOnStart(){
		if (colorBlindFilterType == "normal")
			ChangeFilterNormalVision ();
		else if (colorBlindFilterType == "red")
			ChangeFilterRedBlind ();
		else if (colorBlindFilterType == "green")
			ChangeFilterGreenBlind ();
		filterIntensityText.text = "Intensity :"+colorBlindFilterIntensity.ToString("F2");
	}
	public void ChangeFilterRedBlind(){
		if (colorBlindFilterType == "normal") {
			colorBlindFilter.enabled = true;
			colorBlindFilterIntensitySlider.SetActive (true);
		}
		colorBlindFilter.BlindMode = jp.gulti.ColorBlind.ColorBlindnessSimulator.ColorBlindMode.Deuteranope;
		colorBlindFilterType = "red";
	}
	public void ChangeFilterGreenBlind(){
		if (colorBlindFilterType == "normal") {
			colorBlindFilter.enabled = true;
			colorBlindFilterIntensitySlider.SetActive (true);
		}
		colorBlindFilter.BlindMode = jp.gulti.ColorBlind.ColorBlindnessSimulator.ColorBlindMode.Protanope;
		colorBlindFilterType = "green";
	}
	public void ChangeFilterNormalVision(){
		colorBlindFilter.enabled = false;//disable the filter
		colorBlindFilterIntensitySlider.SetActive(false);
		colorBlindFilterType = "normal";
	}
	public void ChangeFilterIntensity(float newIntensity){
		colorBlindFilterIntensity = newIntensity;
		colorBlindFilter.BlindIntensity = colorBlindFilterIntensity;
		filterIntensityText.text = "Intensity :"+colorBlindFilterIntensity.ToString("F2");
	}
	public void SaveSetting(){
		PlayerPrefs.SetString ("ColorBlindFilterType", colorBlindFilterType);
		PlayerPrefs.SetFloat ("ColorBlindFilterIntensity", colorBlindFilterIntensity);
		PlayerPrefs.Save ();
	}
	// Update is called once per frame
	void Update () {
	
	}
}
