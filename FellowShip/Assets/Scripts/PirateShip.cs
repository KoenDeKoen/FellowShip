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
		moveSpeed = 15.0f;
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

		CollisionDetection(spawnedenemyship.transform.position, 30f, 45f, Color.blue);
//		if(Physics.Raycast(spawnedenemyship.transform.position, spawnedenemyship.transform.forward, out hit, 45f)) 
//		{
//			if (hit.transform.tag != "Wall" && hit.transform.tag != "Player")
//			//if(hit.transform.tag != "Player")
//			{
//				Debug.Log("blue");
//				Debug.DrawLine(spawnedenemyship.transform.position, hit.point, Color.blue);
//				dir += hit.normal * 30;
//			}
//		}

		Vector3 leftR = spawnedenemyship.transform.position - spawnedenemyship.transform.right * -10f;
		Vector3 rightR = spawnedenemyship.transform.position - spawnedenemyship.transform.right * 10f;

		Vector3 leftS = spawnedenemyship.transform.position - spawnedenemyship.transform.forward * -10f;
		Vector3 rightS = spawnedenemyship.transform.position - spawnedenemyship.transform.forward * 10f;

		CollisionDetection(leftR, 20f, 30f, Color.red);
		CollisionDetection(rightR, 20f, 30f, Color.yellow);

//		CollisionDetection(leftS, 20f, 10f, Color.red);
//		CollisionDetection(rightS, 20f, 10f, Color.yellow);

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

	public void CollisionDetection(Vector3 DirectionRay, float turn, float Raylenght, Color color)
	{
		RaycastHit hit = new RaycastHit();
		//Physics.Raycast(DirectionRay, spawnedenemyship.transform.forward, out hit, Raylenght);
		//Debug.DrawLine(DirectionRay, spawnedenemyship.transform.position, color);
		if(Physics.Raycast(DirectionRay, spawnedenemyship.transform.forward, out hit, Raylenght)) 
		{  
			Debug.DrawLine(DirectionRay, hit.point, color);
			if(hit.transform.tag != "Wall" && hit.transform.tag != "Player")
			{
				Debug.DrawLine(DirectionRay, hit.point, color);
				dir += hit.normal * turn;
			}
		}
	}
}