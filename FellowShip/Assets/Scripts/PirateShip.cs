using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PirateShip : MonoBehaviour {
	
	public Transform enemyShip;
	public Transform targetPlayer;

	public PirateshipSpawn pss;

	float distance;
	float moveSpeed;
	float friction;
	//float attackRange;
	public static int state;

	// Use this for initialization
	void Start ()
	{
		//attackRange = 200f;
		pss.Init();
		moveSpeed = 20.0f;
		friction = 5.0f;
		state = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance(targetPlayer.position, enemyShip.position);

		switch(state)
		{
		case 1:
			SpawnPirateShip();
			state = 2;
			break;
			
		case 2:
			LookAt(targetPlayer);
			Attack();
			break;
			
		case 3:
			LookAt(pss.returnSpawnPointPirate()[Random.Range(0, pss.returnSpawnPointPirate().Count)]);
			Attack();
			if(distance < 5.0F)
			{
				Destroy(enemyShip);
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

		Transform clone = Instantiate(enemyShip, pss.returnSpawnPointPirate()[placePos].transform.position, Quaternion.identity) as Transform;
	}


}