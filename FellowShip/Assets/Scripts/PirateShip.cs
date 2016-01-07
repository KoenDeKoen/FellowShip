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

	Vector3 dir;

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
			//Attack();
		break;
			
		case 3:
			LookAt(pss.returnSpawnPointPirate()[2]);
			if(Vector3.Distance(spawnedenemyship.transform.position, pss.returnSpawnPointPirate()[2].transform.position) <= 10f)
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
//		distance = Vector3.Distance(target.transform.position, spawnedenemyship.transform.position);
//
//		Debug.DrawLine(spawnedenemyship.transform.position, spawnedenemyship.transform.position + spawnedenemyship.transform.forward * 50.0f, Color.red);
//
//		Quaternion rotation = Quaternion.LookRotation(target.transform.position - spawnedenemyship.transform.position);
//		spawnedenemyship.transform.rotation = Quaternion.Slerp(spawnedenemyship.transform.rotation, rotation, Time.deltaTime * friction);
		dir = (target.transform.position - spawnedenemyship.transform.position).normalized;
		RaycastHit hit = new RaycastHit();

		if(Physics.Raycast(spawnedenemyship.transform.position, spawnedenemyship.transform.forward, out hit, 45f)) 
		{
			if (hit.transform.tag != "Wall" && hit.transform.tag != "Player")
			//if(hit.transform.tag != "Player")
			{
				Debug.Log("blue");
				Debug.DrawLine(spawnedenemyship.transform.position, hit.point, Color.blue);
				dir += hit.normal * 30;
			}
		}

		Vector3 leftR = spawnedenemyship.transform.position - spawnedenemyship.transform.right * -7f;
		Vector3 rightR = spawnedenemyship.transform.position - spawnedenemyship.transform.right * 7f;

		if(Physics.Raycast(leftR, spawnedenemyship.transform.forward, out hit, 40f)) 
		{
			if(hit.transform.tag != "Wall" && hit.transform.tag != "Player")
			//if(hit.transform.tag != "Player")
			{
				Debug.Log("red");
				Debug.DrawLine(leftR, hit.point, Color.red);
				dir += hit.normal * 35f;
			}
		}
	
		if(Physics.Raycast(rightR, spawnedenemyship.transform.forward, out hit, 40f)) 
		{                
			if(hit.transform.tag != "Wall" && hit.transform.tag != "Player")
			//if(hit.transform != spawnedenemyship.transform)
			{
				Debug.Log("yellow");
				Debug.DrawLine(rightR, hit.point, Color.yellow);
				dir += hit.normal * 35f;
			}
		}
		
		Quaternion rot = Quaternion.LookRotation(dir);
		spawnedenemyship.transform.rotation = Quaternion.Slerp(spawnedenemyship.transform.rotation, rot, Time.deltaTime);
		spawnedenemyship.transform.position += spawnedenemyship.transform.forward * 20f * Time.deltaTime;
	}
//	public void Attack()
//	{
//		spawnedenemyship.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
//	}
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