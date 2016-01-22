using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PirateshipSpawn : MonoBehaviour {

	private List<GameObject> spawnPos;

	public GameObject pos1;
	public GameObject pos2;
	public GameObject pos3;
	public GameObject pos4;

	//Adds Gameobject position to list object
	public void Init()
	{
		spawnPos = new List<GameObject> ();
		spawnPos.Add (pos1);
		spawnPos.Add (pos2);
		spawnPos.Add (pos3);
		spawnPos.Add (pos4);
	}

	//returns value to List spawnpos
	public List<GameObject> returnSpawnPointPirate()
	{
		return spawnPos;
	}
}
