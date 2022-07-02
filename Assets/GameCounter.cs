using UnityEngine;
using System.Collections;

public class GameCounter : MonoBehaviour {

	public int obstacleSpawnCount;//number of spawned obstacles
	public int obstacleSpawnCountLeft;
	public int obstacleSpawnCountRight;
	public int obstacleSpawnCountDefault;

	public int obstacleHitCount;
	public int obstacleHitCountLeft;
	public int obstacleHitCountRight;
	public int obstacleHitCountDefault;

	public int itemSpawnCount; //Number of spawned items
	public int itemSpawnCountLeft;
	public int itemSpawnCountRight;
	public int itemSpawnCountDefault;

	public int itemGetCount;
	public int itemGetCountLeft;
	public int itemGetCountRight;
	public int itemGetCountDefault;

	public int bridgeSpawnCount;
	public int redBlindCount;
	public int greenBlindCount;
	public int notBlindCount;

	public int roadCount;
	public int score;
	public float time;

	public bool counting;//check for counting time

	// Use this for initialization
	void Start () {
		//ResetCounter (); 
		counting = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (counting == true)
			time += Time.deltaTime;//count the time
	}
	//Call this on start will reset all counter value. 
	//In Android bulid the script run order may not be the same as the editor, will make conflict with RoadManager counter change call
	public void ResetCounter (){
		obstacleSpawnCount = 0;
		obstacleSpawnCountLeft = 0;
		obstacleSpawnCountRight = 0;
		obstacleSpawnCountDefault = 0;

		obstacleHitCount = 0;
		obstacleHitCountLeft = 0;
		obstacleHitCountRight = 0;
		obstacleHitCountDefault = 0;

		itemSpawnCount = 0;
		itemSpawnCountLeft = 0;
		itemSpawnCountRight = 0;
		itemSpawnCountDefault = 0;

		itemGetCount = 0;
		itemGetCountLeft = 0;
		itemGetCountRight = 0;
		itemGetCountDefault = 0;

		bridgeSpawnCount = 0;
		redBlindCount = 0;
		greenBlindCount = 0;
		notBlindCount = 0;

		time = 0;
		score = 0;
		roadCount = 0;
	}
	void LateUpdate(){//updating score every second
		score = (obstacleSpawnCount - obstacleHitCount)*100+
			itemGetCount*200-(redBlindCount+greenBlindCount)*100+
			(roadCount*5-(int)time) * 10;
	}
	public void SaveRecord (){//Save all Counter Values to playerPrefs
		PlayerPrefs.SetInt ("recordGameLength", roadCount);
		PlayerPrefs.SetInt ("recordFinishTime", (int)time);
		PlayerPrefs.SetInt ("recordScore", score);

		PlayerPrefs.SetInt ("recordObstacleHitCount", obstacleHitCount);
		PlayerPrefs.SetInt ("recordObstacleHitCountLeft", obstacleHitCountLeft);
		PlayerPrefs.SetInt ("recordObstacleHitCountRight", obstacleHitCountRight);
		PlayerPrefs.SetInt ("recordObstacleHitCountDefault", obstacleHitCountDefault);

		PlayerPrefs.SetInt ("recordObstacleSpawnCount", obstacleSpawnCount);
		PlayerPrefs.SetInt ("recordObstacleSpawnCountLeft", obstacleSpawnCountLeft);
		PlayerPrefs.SetInt ("recordObstacleSpawnCountRight", obstacleSpawnCountRight);
		PlayerPrefs.SetInt ("recordObstacleSpawnCountDefault", obstacleSpawnCountDefault);

		PlayerPrefs.SetInt ("recordItemGetCount", itemGetCount);
		PlayerPrefs.SetInt ("recordItemGetCountLeft", itemGetCountLeft);
		PlayerPrefs.SetInt ("recordItemGetCountRight", itemGetCountRight);
		PlayerPrefs.SetInt ("recordItemGetCountDefault", itemGetCountDefault);

		PlayerPrefs.SetInt ("recordItemSpawnCount", itemSpawnCount);
		PlayerPrefs.SetInt ("recordItemSpawnCountLeft", itemSpawnCountLeft);
		PlayerPrefs.SetInt ("recordItemSpawnCountRight", itemSpawnCountRight);
		PlayerPrefs.SetInt ("recordItemSpawnCountDefault", itemSpawnCountDefault);

		PlayerPrefs.SetInt ("recordBridgeSpawnCount", bridgeSpawnCount); 
		PlayerPrefs.SetInt ("recordRedBlindCount", redBlindCount);
		PlayerPrefs.SetInt ("recordGreenBlindCount", greenBlindCount); 
		PlayerPrefs.SetInt ("recordNotBlindCount", notBlindCount);

		PlayerPrefs.SetString ("recordTime", System.DateTime.Now.ToString());
		PlayerPrefs.Save ();
	}
}