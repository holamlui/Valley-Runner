using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HowToPlayImage : MonoBehaviour {

	public Texture[] imageList;
	private int currentIndex;
	private RawImage imageDisplay;
	// Use this for initialization
	void Start () {
		currentIndex = 0;
		imageDisplay = GameObject.Find("HowToPlayImage").GetComponent<RawImage> ();
		imageDisplay.texture = imageList [0];
	}
	
	// Update is called once per frame
	void Update () {	
	}
	public void NextImage(){
		if (currentIndex == imageList.Length - 1) {//if this is last image
			currentIndex = 0;//back to first image
		} else {
			currentIndex++;
		}
		imageDisplay.texture = imageList [currentIndex];
	}
	public void PreviousImage(){
		if (currentIndex == 0) {//if this is first image
			currentIndex = imageList.Length-1;//back to last image
		} else {
			currentIndex--;
		}
		imageDisplay.texture = imageList [currentIndex];
	}
}
