using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

	public void LoadScene(int scene){
		SceneManager.LoadScene(scene);
	}
	public void ExitGame(){
		Application.Quit ();
	}
}