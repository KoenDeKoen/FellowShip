//Made by Martijn Koekkoek

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PirateShip : MonoBehaviour {
	
	public GameObject enemyShip;
	private GameObject spawnedenemyship;
	GameObject currentBoat;

	public PirateshipSpawn pss;
	public PickUpSpawning pus;
	public BoatUpgrade bu;

	float distance;
	float moveSpeed;
	float friction;

	public bool shipspawned;
	public static int state;

	// Use this for initialization
	void Start ()
	{
		shipspawned = false;
		pss.Init();
		moveSpeed = 20.0f;
		friction = 5.0f;
		state = 0;

		currentBoat = bu.currentboat;
		Physics.IgnoreLayerCollision(8,9);
	}
	
	// Update is called once per frame
	void Update ()
	{

		if(!shipspawned){
			if (pus.getCurrentPickups() >= 3)
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
				Destroy(spawnedenemyship);
				shipspawned = false;
				state = 0;
			}
			break;
		}
	}

	public void LookAt(GameObject target)
	{
		distance = Vector3.Distance(target.transform.position, spawnedenemyship.transform.position);

		Quaternion rotation = Quaternion.LookRotation(target.transform.position - spawnedenemyship.transform.position);
		spawnedenemyship.transform.rotation = Quaternion.Slerp(spawnedenemyship.transform.rotation, rotation, Time.deltaTime * friction);

	}

	public void Attack()
	{
		spawnedenemyship.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}

	public void SpawnPirateShip()
	{
		int placePos = Random.Range (0, pss.returnSpawnPointPirate().Count);

		GameObject clone;
		clone = Instantiate(enemyShip, pss.returnSpawnPointPirate()[placePos].transform.position, Quaternion.identity) as GameObject;
		spawnedenemyship = clone;
		spawnedenemyship.tag = "Pirate";
	}

	public void DestoryShip()
	{
		Destroy(spawnedenemyship);
		shipspawned = true;
		state = 0;
	}
}