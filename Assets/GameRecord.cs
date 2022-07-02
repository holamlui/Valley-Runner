using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameRecord : MonoBehaviour {

	public int obstacleHitCount;
	private int obstacleHitCountLeft;
	private int obstacleHitCountRight;
	private int obstacleHitCountDefault;

	public int obstacleSpawnCount;
	private int obstacleSpawnCountLeft;
	private int obstacleSpawnCountRight;
	private int obstacleSpawnCountDefault;

	public int itemGetCount;
	public int itemGetCountLeft;
	public int itemGetCountRight;
	public int itemGetCountDefault;

	public int itemSpawnCount;
	public int itemSpawnCountLeft;
	public int itemSpawnCountRight;
	public int itemSpawnCountDefault;

	public int bridgeSpawnCount;
	public int redBlindCount;
	public int greenBlindCount;
	public int notBlindCount;

	public string gameDateTime;
	public int gameLength;
	public int finishTime;
	public int score;

	private string currentRecordType;
	private Text recordDetailType;
	private Text recordDetail1;
	private Text recordDetail2;
	private Text recordDetail3;
	private Text recordDetailTotal;

	// Use this for initialization
	void Start () {
		/*--Get last game record saved in playerPrefs--*/
		GetPlayerRecord ();
		ShowMainRecord (); //Show Main Record include time, game length, score and overall performance
		ShowVisionTestResult();

		GetTextObjects (); //Get Text Objects in Detail Record
		ShowObstacleRecordDetail (); //Show the Obstacle Detail Record as Default
	}

	
	// Update is called once per frame
	void Update () {
	
	}
	private void GetPlayerRecord(){
		//display overall performance
		gameLength = PlayerPrefs.GetInt ("recordGameLength", 0);//Get the value of the record, not the game setting!!
		finishTime = PlayerPrefs.GetInt ("recordFinishTime", 0); 
		score = PlayerPrefs.GetInt ("recordScore", 0);
		gameDateTime = PlayerPrefs.GetString ("recordTime", "No Record Yet");
		//obstacles
		obstacleHitCount = PlayerPrefs.GetInt ("recordObstacleHitCount", 0); 
		obstacleHitCountLeft = PlayerPrefs.GetInt ("recordObstacleHitCountLeft", 0); 
		obstacleHitCountRight = PlayerPrefs.GetInt ("recordObstacleHitCountRight", 0); 
		obstacleHitCountDefault = PlayerPrefs.GetInt ("recordObstacleHitCountDefault", 0); 

		obstacleSpawnCount = PlayerPrefs.GetInt ("recordObstacleSpawnCount", 0);
		obstacleSpawnCountLeft = PlayerPrefs.GetInt ("recordObstacleSpawnCountLeft", 0);
		obstacleSpawnCountRight = PlayerPrefs.GetInt ("recordObstacleSpawnCountRight", 0);
		obstacleSpawnCountDefault = PlayerPrefs.GetInt ("recordObstacleSpawnCountDefault", 0);

		//items
		itemGetCount = PlayerPrefs.GetInt ("recordItemGetCount", 0); 
		itemGetCountLeft = PlayerPrefs.GetInt ("recordItemGetCountLeft", 0); 
		itemGetCountRight = PlayerPrefs.GetInt ("recordItemGetCountRight", 0); 
		itemGetCountDefault = PlayerPrefs.GetInt ("recordItemGetCountDefault", 0); 

		itemSpawnCount = PlayerPrefs.GetInt ("recordItemSpawnCount", 0);
		itemSpawnCountLeft = PlayerPrefs.GetInt ("recordItemSpawnCountLeft", 0);
		itemSpawnCountRight = PlayerPrefs.GetInt ("recordItemSpawnCountRight", 0);
		itemSpawnCountDefault = PlayerPrefs.GetInt ("recordItemSpawnCountDefault", 0);

		//color blind bridge test
		bridgeSpawnCount = PlayerPrefs.GetInt ("recordBridgeSpawnCount", 0); 
		redBlindCount = PlayerPrefs.GetInt ("recordRedBlindCount", 0);
		greenBlindCount = PlayerPrefs.GetInt ("recordGreenBlindCount", 0); 
		notBlindCount = PlayerPrefs.GetInt ("recordNotBlindCount", 0);
	}
	private void ShowMainRecord(){
		GameObject.Find ("DateRecord").GetComponentInChildren<Text> ().text = gameDateTime;
		GameObject.Find ("LengthRecord").GetComponentInChildren<Text> ().text = "Length: " + gameLength;
		GameObject.Find ("TimeRecord").GetComponentInChildren<Text> ().text = "Time: " + finishTime;
		GameObject.Find ("ScoreRecord").GetComponentInChildren<Text> ().text = "Score: " + score;
		//
		GameObject.Find ("ObstacleRecord").GetComponentInChildren<Text> ().text = "Obstacles Pass: " + (obstacleSpawnCount-obstacleHitCount) + " / " + obstacleSpawnCount;
		GameObject.Find ("ItemRecord").GetComponentInChildren<Text>().text = "Items Get: " + itemGetCount + " / " + itemSpawnCount;
		GameObject.Find ("ColorBlindRecord").GetComponentInChildren<Text> ().text = "Bridges Pass: " + (bridgeSpawnCount-redBlindCount-greenBlindCount) + " / " + bridgeSpawnCount;
	}
	private void ShowVisionTestResult(){
		Text visionDetail = GameObject.Find ("VisionDetail").GetComponentInChildren<Text> ();
		Text colorBlindDetail = GameObject.Find ("ColorBlindDetail").GetComponentInChildren<Text> ();

		if (obstacleSpawnCount == 0 && itemSpawnCount == 0) {
			visionDetail.text = "Binocular Vision: \n No test data given";
		} else {
			float binocularRate = ((float)(obstacleSpawnCount - obstacleHitCount + itemGetCount) / (obstacleSpawnCount + itemSpawnCount))*1.0f;
			if (binocularRate >= 0.8f) {//pass obstacles and get items above 80%, need more test data to be accurate
				visionDetail.text = "Your Binocular vision \n is Good!";
			} else {
				visionDetail.text = "You may have diplopia,\n amblyopia or strabismus";
			}
		}
		if (bridgeSpawnCount == 0) {
			colorBlindDetail.text = "Color Blind: \n No test data given";
		} else {
			if ((float)notBlindCount / bridgeSpawnCount > 0.8f) {//if pass color blind test at >80%
				colorBlindDetail.text = "You are not color blind";
			} else if (redBlindCount > greenBlindCount) {
				colorBlindDetail.text = "You may be Red Blind";
			} else if (greenBlindCount > redBlindCount) {
				colorBlindDetail.text = "You may be Green Blind";
			} else {
				colorBlindDetail.text = "You may be color blind";
			}
		}
	}
	public void ShowNextRecordType(){
		switch (currentRecordType) {
		case "Obstacle":
			ShowItemRecordDetail ();
			break;
		case "Item":
			ShowColorBlindRecordDetail ();
			break;
		case "ColorBlind":
			ShowRecordDetailByLayer();
			break;
		case "ByLayer":
			ShowObstacleRecordDetail ();
			break;
		default:
			break;
		}
	}
	public void ShowPrevoiusRecordType(){
		switch (currentRecordType) {
		case "Obstacle":
			ShowRecordDetailByLayer();
			break;
		case "Item":
			ShowObstacleRecordDetail ();
			break;
		case "ColorBlind":
			ShowItemRecordDetail ();
			break;
		case "ByLayer":
			ShowColorBlindRecordDetail ();
			break;
		default:
			break;
		}

	}
	private void GetTextObjects(){
		recordDetailType = GameObject.Find ("RecordDetailType").GetComponentInChildren<Text> ();
		recordDetail1 = GameObject.Find ("RecordDetail1").GetComponentInChildren<Text> ();
		recordDetail2 = GameObject.Find ("RecordDetail2").GetComponentInChildren<Text> ();
		recordDetail3 = GameObject.Find ("RecordDetail3").GetComponentInChildren<Text> ();
		recordDetailTotal = GameObject.Find ("RecordDetailTotal").GetComponentInChildren<Text> ();			
	}
	private void ShowObstacleRecordDetail(){
		currentRecordType = "Obstacle";
		recordDetailType.text = "Obstacles Pass";
		recordDetail1.text = 	 " Left Eye: " + (obstacleSpawnCountLeft - obstacleHitCountLeft) + " / " + obstacleSpawnCountLeft;
		recordDetail2.text =	 "Right Eye: " + (obstacleSpawnCountRight - obstacleHitCountRight) + " / " + obstacleSpawnCountRight;
		recordDetail3.text = 	 " Both Eye: " + (obstacleSpawnCountDefault - obstacleHitCountDefault) + " / " + obstacleSpawnCountDefault;
		recordDetailTotal.text = "    Total: " + (obstacleSpawnCount - obstacleHitCount) + " / " + obstacleSpawnCount;
	}
	private void ShowItemRecordDetail(){
		currentRecordType = "Item";
		recordDetailType.text = "Items Collect";
		recordDetail1.text = 	 " Left Eye: " + itemGetCountLeft + " / " + itemSpawnCountLeft;
		recordDetail2.text = 	 "Right Eye: " + itemGetCountRight + " / " + itemSpawnCountRight;
		recordDetail3.text = 	 " Both Eye: " + itemGetCountDefault + " / " + itemSpawnCountDefault;
		recordDetailTotal.text = "    Total: " + itemGetCount + " / " + itemSpawnCount;
	}
	private void ShowColorBlindRecordDetail(){
		currentRecordType = "ColorBlind";
		recordDetailType.text = "Color Blind";
		recordDetail1.text = "    Red Blind: " + redBlindCount+ " / " + bridgeSpawnCount;
		recordDetail2.text = "  Green Blind: " + greenBlindCount +" / " + bridgeSpawnCount;
		recordDetail3.text = "Normal Vision: " + notBlindCount +" / " + bridgeSpawnCount;
		recordDetailTotal.text = "";
	}
	private void ShowRecordDetailByLayer(){
		currentRecordType = "ByLayer";
		recordDetailType.text = "By Layer";
		recordDetail1.text =     " Left Eye: " + (itemGetCountLeft+obstacleSpawnCountLeft - obstacleHitCountLeft)+ " / " +(obstacleSpawnCountLeft+itemSpawnCountLeft);
		recordDetail2.text =     "Right Eye: " + (itemGetCountRight+obstacleSpawnCountRight - obstacleHitCountRight)+ " / " +(obstacleSpawnCountRight+itemSpawnCountRight);
		recordDetail3.text =     " Both Eye: " + (itemGetCountDefault+obstacleSpawnCountDefault - obstacleHitCountDefault) +" / " +(obstacleSpawnCountDefault+itemSpawnCountDefault);
		recordDetailTotal.text = "   Total : " + (itemGetCount+obstacleSpawnCount - obstacleHitCount) + " / " + (obstacleSpawnCount+itemSpawnCount);
	}

}