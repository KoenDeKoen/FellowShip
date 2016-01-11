using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Compass : MonoBehaviour {
	
	public GameObject compass;

	GameObject currentBoat;
	GameObject currentPickUp;

	public BoatUpgrade bu;
	public PickUpSpawning pus;

	// Update is called once per frame
	void Update () {
		Debug.Log(pus.currentPos);
		currentPickUp.transform.position = pus.currentPos;
		currentBoat = bu.currentboat;
		AimAtObjective();
	}

	public void AimAtObjective()
	{
		Quaternion rotation = Quaternion.LookRotation(currentBoat.transform.position - currentPickUp.transform.position);
		compass.transform.rotation = Quaternion.Slerp(compass.transform.rotation, rotation, Time.deltaTime * 5.0f);
	}
}
