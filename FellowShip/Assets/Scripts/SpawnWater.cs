using UnityEngine;
using System.Collections;

public class SpawnWater : MonoBehaviour {
    public GameObject Water;
    private int xsize = 6;
    private int zsize = 7;

	void Start () {
        for (int x = -6; x < xsize; x++)
        {
            for (int z = -4; z < zsize; z++)
            {
               GameObject w =  Instantiate((Water), new Vector3(x * 100, -3, z * 100), Quaternion.Euler(0,0,0)) as GameObject;
                w.transform.parent = gameObject.transform;
            }
        }
	}
	

	void Update () {
	
	}
}
