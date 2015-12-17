using UnityEngine;
using System.Collections;

public class ChaseShip : MonoBehaviour {

		public Transform enemyShip;
		GameObject currentBoat;

		public PickUpSpawning pus;
		public BoatUpgrade bu;
		
		float distance;
		float moveSpeed;
		float friction;


		// Use this for initialization
		void Start ()
		{
			moveSpeed = 20.0f;
			friction = 5.0f;
			
			currentBoat = bu.currentboat;
		}
		
		// Update is called once per frame
		void Update ()
		{
			if(currentBoat == null)
			{
				currentBoat = bu.currentboat;
			}

			distance = Vector3.Distance(currentBoat.transform.position, enemyShip.position);
			LookAt(currentBoat);
			Attack();
		}
		
		public void LookAt(GameObject target)
		{
			Quaternion rotation = Quaternion.LookRotation(target.transform.position - enemyShip.position);
			enemyShip.rotation = Quaternion.Slerp(enemyShip.rotation, rotation, Time.deltaTime * friction);
		}
		
		public void Attack()
		{
			enemyShip.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		}
}
