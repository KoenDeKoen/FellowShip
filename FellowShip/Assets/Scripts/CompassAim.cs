using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompassAim : MonoBehaviour {
	
	public Transform compass;

	GameObject currentBoat;
	GameObject currentPickUp;
	public bool goalsAvailable;

	public BoatUpgrade bu;
	public PickUpSpawning pus;
	public Pickup pu;


	void Start()
	{
		goalsAvailable = false;
		currentBoat = bu.currentboat;
	}

	// Update is called once per frame
	void Update () {
		if(goalsAvailable)
		{
			currentPickUp = pu.currentpickup;
			//currentPickUp.transform.position = pus.currentPos;
			currentBoat = bu.currentboat;
			AimAtObjective();
		}
	}

//	public void AimAtObjective()
//	{
//		Quaternion rotation = Quaternion.LookRotation(currentBoat.transform.position - currentPickUp.transform.position);
//		Debug.Log(rotation);
//		compass.rotation = Quaternion.Slerp(compass.rotation, rotation, Time.deltaTime * 5.0f);
//	}

	public void AimAtObjective()
	{
		Vector3 target = currentPickUp.transform.position - currentBoat.transform.position;
		Vector3 player = currentBoat.transform.right;
		
		float angle = Vector3.Angle(player, target);
		Vector3 cross = Vector3.Cross(player, target);
		
		if(cross.y > 0f)
		{
			angle = -angle;
		}
		
		compass.rotation = Quaternion.Euler(0, 0, angle);
	}
}
