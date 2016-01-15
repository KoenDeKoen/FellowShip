using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompassAim : MonoBehaviour {

	public Transform compass;

	public bool goalsAvailable;

	GameObject currentBoat;
	GameObject currentPickUp;
	GameObject focusRotate;

	public BoatUpgrade bu;
	public Pickup pu;

	void Start()
	{
		focusRotate = new GameObject("CompassNeedle");
		goalsAvailable = false;
		currentBoat = bu.currentboat;
		focusRotate.transform.position = currentBoat.transform.position;
	}

	void Update()
	{
		if(goalsAvailable)
		{
			currentPickUp = pu.currentpickup;
			currentBoat = bu.currentboat;
			focusRotate.transform.position = currentBoat.transform.position;
			LookAt();
		}
	}

	public void LookAt()
	{
		Quaternion rot = Quaternion.LookRotation(currentPickUp.transform.position - focusRotate.transform.position);
		focusRotate.transform.rotation = Quaternion.Slerp(focusRotate.transform.rotation, rot, Time.deltaTime * 10.0f);

		float angle = focusRotate.transform.eulerAngles.y;
		angle = -angle;

		compass.rotation = Quaternion.Euler(0, 0, angle);
	}
//	public Transform compass;
//
//	Transform currentBoat;
//	GameObject currentPickUp;
//
//	public bool goalsAvailable;
//	public BoatUpgrade bu;
//	public PickUpSpawning pus;
//	public Pickup pu;
//	
//	void Start()
//	{
//		goalsAvailable = false;
//		currentBoat = bu.currentboat;
//	}
//
//	// Update is called once per frame
//	void Update () {
//		if(goalsAvailable)
//		{
//			currentPickUp = pu.currentpickup;
//			currentBoat = bu.currentboat;
//		
//			AimAtObjective();
//		}
//	}
//
//	public void AimAtObjective()
//	{
//		Vector3 target = currentPickUp.transform.position;
//		Vector3 player = currentBoat.transform.position;
//		Debug.DrawLine(target, player);
//
//		Debug.Log("target" + target);
//		Debug.Log("player" + player);
//
//		float angle = Vector3.Angle(player, target);
//		Vector3 cross = Vector3.Cross(player, target);
//
//		if(cross.y > 0f)
//		{
//			angle = -angle;
//		}
//		
//		compass.rotation = Quaternion.Euler(0, 0, angle);
//	}
}
