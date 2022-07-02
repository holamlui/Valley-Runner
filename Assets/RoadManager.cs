using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {

	public GameObject gameManager;
	public GameCounter gameCounter;

	public GameObject[] roadPrefabs;	//Road Objects to spawn

	public GameObject[] rockPrefabs;
	public GameObject[] treePrefabs;
	public GameObject[] itemPrefabs;

	public GameObject bridgePrefab;
	public GameObject[] transparentBlockPrefabs;

	public GameObject goalPrefab;

	private Transform playerTransform; //Transform of player
	private float spawnZ = 10.0f;		//Z of first spawn item
	private int gameLength;
	private int spawnedRoad = 0;

	public float roadLength = 10.0f;
	private int amtOfRoadOnScreen = 7;
	private float safeLength = 40.0f;
	private int[] lastPosX={-1,-1}; //Avoid continously spawning object at same X, Keep Record for two objects|[last, last last]

	private List<GameObject> activeRoad; //A list to store current Road Block

	private int colorBlindSpawnRate;
	private int itemSpawnRate;
	private int rockSpawnRate;

	void Start () {
		gameLength = PlayerPrefs.GetInt ("gameLength");

		gameManager = GameObject.Find ("GameManager");
		gameCounter = gameManager.GetComponent<GameCounter> ();

		activeRoad = new List<GameObject> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		colorBlindSpawnRate = PlayerPrefs.GetInt("ColorBlindSpawnRate",20);
		itemSpawnRate = PlayerPrefs.GetInt("ItemSpawnRate",20);
		rockSpawnRate = PlayerPrefs.GetInt("RockSpawnRate",50);
			
		for (int i = 0; i < amtOfRoadOnScreen; i++) { //spawn some road before the game start
			if (i < gameLength) {
				SpawnRoad ();
			}
		}
	}
	void Update () {
		if (spawnedRoad < gameLength) {
			if (playerTransform.position.z - safeLength > (spawnZ - amtOfRoadOnScreen * roadLength)) {
				SpawnRoad ();
				DeleteRoad ();
				if (playerTransform.position.z > 40) {
					Destroy (GameObject.Find ("Start"));
				}
			}
		} else if(spawnedRoad==gameLength){//if already spawned full game length
			//SpawnGoal
			GameObject goal;
			goal = Instantiate (goalPrefab)as GameObject;
			goal.transform.SetParent (transform);
			goal.transform.position = Vector3.forward * spawnZ;
			spawnedRoad++;
		}
	}

	private void SpawnRoad(int prefabIndex =-1){
		GameObject road;
		road = Instantiate (roadPrefabs [RandomPrefabIndex(roadPrefabs)])as GameObject;
		road.transform.SetParent (transform);
		road.transform.position = Vector3.forward * spawnZ;
		gameCounter.roadCount++;//The Actual numer of road spawned, not include Start & Goal, need this in record

		activeRoad.Add (road); //Add the spawned Road to the list 
		if (Random.Range(0,100) <= colorBlindSpawnRate) {
			//spawn Bridge
			GameObject bridge;
			bridge = Instantiate (bridgePrefab)as GameObject;
			bridge.transform.SetParent (road.transform);//Set this object as the child of the road block
			bridge.transform.position = Vector3.forward * spawnZ;
			//spawn transparent blocks
			SpawnTransparentBlocks(bridge);
			gameCounter.bridgeSpawnCount++;
			//spawn color blindness test
		} else {
			int spawnAmt = Random.Range (1, 3); //number of items to spawn, ranged from 1 to 3 randomly
			int spawnedItem = 0;				//number of spawned item, ensure at most 1 item is spawned in a row
			int spawnedObstacle = 0;			//number of spawned obstacle, ensure at most 2 obstacle are spawned in a row
			for (int i = 0; i < spawnAmt; i++) {
				if (Random.Range (0, 100) <= itemSpawnRate) {//Spwan Items at 70%
					if (spawnedItem == 0) {
						SpawnItem (road);
						spawnedItem++;
					}
				}else {
					if (spawnedObstacle < 2) {
						SpawnObstacle (road);
						spawnedObstacle++;
					}				
				}
			}
		} 
		spawnedRoad++;
		spawnZ += roadLength;
	}
	private void SpawnTransparentBlocks(GameObject bridge){	//Spawn the transparent triggers for the bridge	
		List<int> orderList = new List<int>();
		List<int> remaining = Enumerable.Range (0, 3).ToList ();

		while (remaining.Count > 0) {
			int randomIndex = Random.Range (0, remaining.Count);
			orderList.Add (remaining [randomIndex]);
			remaining.RemoveAt (randomIndex);
		}

		//Assign posX of correct answer
		for(int i=0;i<transparentBlockPrefabs.Length;i++){
			GameObject transparentBlock;
			transparentBlock = Instantiate (transparentBlockPrefabs[i])as GameObject;
			transparentBlock.transform.SetParent (bridge.transform);//Set this object as the child of the road block
			transparentBlock.transform.position = Vector3.forward * spawnZ;
			transparentBlock.transform.Translate(Vector3.up * 1.5f);
			transparentBlock.transform.Translate (Vector3.right * (3.3f - orderList[i]*3.3f));
		}
		//transparentBlockPrefabs

	}
	private void SpawnObstacle(GameObject road){
		GameObject obstacle;
		if (Random.Range (0, 100) <= rockSpawnRate)
			obstacle = Instantiate (rockPrefabs [RandomPrefabIndex (rockPrefabs)])as GameObject;
		else
			obstacle = Instantiate (treePrefabs [RandomPrefabIndex (treePrefabs)])as GameObject;

		obstacle.transform.SetParent (road.transform);//Set this object as the child of the road block
		obstacle.transform.position = Vector3.forward * spawnZ ;
		obstacle.transform.Translate (Vector3.right * RandomPositionX ());
		obstacle.layer = RandomLayer ();
		switch (obstacle.layer) {
		case 8:
			gameCounter.obstacleSpawnCountLeft++;
			break;
		case 9:
			gameCounter.obstacleSpawnCountRight++;
			break;
		default:
			gameCounter.obstacleSpawnCountDefault++;
			break;
		}			
		gameCounter.obstacleSpawnCount++;
	}
	private void SpawnItem(GameObject road){
		GameObject item;
		item = Instantiate (itemPrefabs [RandomPrefabIndex (itemPrefabs)])as GameObject;
		item.transform.SetParent (road.transform);//Set this object as the child of the road block
		item.transform.position = Vector3.forward * spawnZ;
		item.transform.Translate (Vector3.right * RandomPositionX ());
		item.layer = RandomLayer ();
		switch (item.layer) {//0,8,9 defalut,Left,Right
		case 8:
			gameCounter.itemSpawnCountLeft++;
			break;
		case 9:
			gameCounter.itemSpawnCountRight++;
			break;
		default:
			gameCounter.itemSpawnCountDefault++;
			break;
		}
		gameCounter.itemSpawnCount++;
	}


	private void DeleteRoad(){		
		Destroy (activeRoad [0]);	//Destory the first Road in the list
		activeRoad.RemoveAt (0);	//Remove the Road from the list
	}

	private int RandomPrefabIndex(GameObject[] objectArray){//Return a random index number from a list of prefabs
		if(objectArray.Length<=1)
			return 0;
		
		int randomIndex = Random.Range (0, objectArray.Length);//return an int between 0 & endIndex of the array
		return randomIndex;
	}
	private int RandomLayer(){//return a random layer index| 0-default 8-LeftEye 9-RightEye
		int randomLayer = Random.Range(0,3);//return an int from 0-2
		if (randomLayer != 0)
			randomLayer += 7;//1+7 = 8->LeftEye | 2+7 = 9->RightEye
		return randomLayer;
	}
	private float RandomPositionX(){	//return a random Position  -3.3   0     3.3
										//							Left|Center|Right
		if(lastPosX[0]==-1)
			lastPosX[0]=Random.Range(0,3);		
		int randomPosX = Random.Range(0,3);
		while(randomPosX==lastPosX[0]||randomPosX==lastPosX[1]){	//if Object pos is same as last one
			randomPosX=Random.Range(0,3);			//return an random int from 0-2
		}								//3.3-0 =3.3->Right | 3.3-3.3 = 0 ->Center| 3.3-6.6=-3.3 -> Left
		lastPosX[0]=randomPosX;			//update data of lastPosX
		lastPosX[1] = lastPosX[1];
		return 3.3f-randomPosX*3.3f;
	}
}