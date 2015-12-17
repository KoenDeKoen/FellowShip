using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PirateShip : MonoBehaviour {
	
	public GameObject enemyShip;
	//public Transform targetPlayer;
	GameObject currentBoat;

	public PirateshipSpawn pss;
	public PickUpSpawning pus;
	public BoatUpgrade bu;

	float distance;
	float moveSpeed;
	float friction;
	//float attackRange;

	bool shipspawned;
	public static int state;

	// Use this for initialization
	void Start ()
	{
		shipspawned = false;
//		attackRange = 200f;
		pss.Init();
		moveSpeed = 20.0f;
		friction = 5.0f;
		state = 0;

		currentBoat = bu.currentboat;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!shipspawned){
			if (pus.getCurrentPickups() == 3)
			{
				state = 1;
			}
		}

		if(currentBoat == null)
		{
			currentBoat = bu.currentboat;
		}

		switch(state)
		{
		case 1:
			shipspawned = true;
			SpawnPirateShip();
			state = 2;
			break;
			
		case 2:
			LookAt(currentBoat);
			Attack();
			break;
			
		case 3:
			LookAt(pss.returnSpawnPointPirate()[2]);
			Attack();
			if(distance <= 10F)
			{
				Destroy(enemyShip);
				state = 0;
				shipspawned = false;
			}
			break;
		}
	}

	public void LookAt(GameObject target)
	{
		distance = Vector3.Distance(target.transform.position, enemyShip.transform.position);

		Quaternion rotation = Quaternion.LookRotation(target.transform.position - enemyShip.transform.position);
		enemyShip.transform.rotation = Quaternion.Slerp(enemyShip.transform.rotation, rotation, Time.deltaTime * friction);
	}

	public void Attack()
	{
		enemyShip.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

	public void SpawnPirateShip()
	{
		int placePos = Random.Range (0, pss.returnSpawnPointPirate().Count);

		GameObject clone;
		clone = Instantiate(enemyShip, pss.returnSpawnPointPirate()[placePos].transform.position, Quaternion.identity) as GameObject;
		enemyShip = clone;
		enemyShip.tag = "Pirate";
	}
}