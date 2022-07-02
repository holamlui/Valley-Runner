using UnityEngine;
using System.Collections;

public class QuadMove : MonoBehaviour {
	public void MoveBack() {
		Vector3 position = transform.position;
		float distance = 1.5f;
		position.z += distance;
		transform.position = position;
	}
	public void MoveForward() {
		Vector3 position = transform.position;
		float distance = 1.5f;
		position.z -= distance;
		if(position.z>1.2)
			transform.position = position;
	}
	public void ResetPosition() {
		Vector3 position = new Vector3(0.0f,1.2f,1.8f);
		transform.position = position;
	}
}
