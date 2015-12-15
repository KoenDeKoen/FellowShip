using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PirateshipSpawn : MonoBehaviour {

	private List<Transform> spawnPos;

	public Transform pos1;
	public Transform pos2;
	public Transform pos3;
	public Transform pos4;

	public void Init()
	{
		spawnPos = new List<Transform> ();
		spawnPos.Add (pos1);
		spawnPos.Add (pos2);
		spawnPos.Add (pos3);
		spawnPos.Add (pos4);
	}

	public List<Transform> returnSpawnPointPirate()
	{
		return spawnPos;
	}
}
