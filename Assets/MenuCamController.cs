using UnityEngine;
using System.Collections;

public class MenuCamController : MonoBehaviour {

	public float speedFactor = 0.1f;     
	public float zoomFactor = 1.0f;     
	public Transform currentMount;     
	public Camera cameraComp;    
	Vector3 lastPosition; 

	void Start (){        
		lastPosition = transform.position;  
	}   
	void Update (){
		transform.position = Vector3.Lerp(transform.position, currentMount.position, speedFactor);        
		transform.rotation = Quaternion.Slerp(transform.rotation, currentMount.rotation, speedFactor);        
		var velocity = Vector3.Magnitude(transform.position - lastPosition);      
		cameraComp.fieldOfView = 60 + velocity * zoomFactor;      
		lastPosition = transform.position;  
	}    
	public void setMount(Transform newMount){         
		currentMount = newMount;
	} 
}﻿