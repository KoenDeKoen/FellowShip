using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PirateShip : MonoBehaviour {
	
	public Transform enemyShip;
	//public Transform targetPlayer;

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

		switch(state)
		{
		case 1:
			shipspawned = true;
			SpawnPirateShip();
			state = 2;
			break;
			
		case 2:
			distance = Vector3.Distance(bu.currentboat.transform.position, enemyShip.position);
			LookAt(bu.currentboat.transform);
			Attack();
			break;
			
		case 3:
			LookAt(pss.returnSpawnPointPirate()[2]);
			Attack();
			if(distance < 5.0F)
			{
				Destroy(enemyShip);
				state = 0;
				shipspawned = false;
			}
			break;
		}
	}

	public void LookAt(Transform target)
	{
		Quaternion rotation = Quaternion.LookRotation(target.position - enemyShip.position);
		enemyShip.rotation = Quaternion.Slerp(enemyShip.rotation, rotation, Time.deltaTime * friction);
	}

	public void Attack()
	{
		enemyShip.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

	public void SpawnPirateShip()
	{
		int placePos = Random.Range (0, pss.returnSpawnPointPirate().Count);

		Transform clone;
		clone = Instantiate(enemyShip, pss.returnSpawnPointPirate()[placePos].transform.position, Quaternion.identity) as Transform;
	}


}