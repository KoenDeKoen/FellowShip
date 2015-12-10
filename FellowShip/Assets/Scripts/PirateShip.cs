using UnityEngine;
using System.Collections;

public class PirateShip : MonoBehaviour {
	
	public Transform enemyShip;
	public Transform targetPlayer;
	float distance;
	float attackRange;
	float moveSpeed;
	float friction;

	// Use this for initialization
	void Start ()
	{
		attackRange = 200f;
		moveSpeed = 20.0f;
		friction = 5.0f;
	}
	
	// Update is called once per frame
	void Update ()
	{
		distance = Vector3.Distance(targetPlayer.position, enemyShip.position);

		if(distance < attackRange)
		{
			LookAt();
			Attack();
		}
	}

	public void LookAt()
	{
		Quaternion rotation = Quaternion.LookRotation(targetPlayer.position - enemyShip.position);
		enemyShip.rotation = Quaternion.Slerp(enemyShip.rotation, rotation, Time.deltaTime * friction);
	}

	public void Attack()
	{
		enemyShip.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
	}
}