using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour
{
    public Obstacles obstacles;
    private List<Vector3> spawnedobstacles;
	// Use this for initialization
	void Start ()
    {
        spawnNextObstacle();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void spawnNextObstacle()
    {
        GameObject spawnedobstacle = Instantiate(obstacles.returnObstacles()[Random.Range(0, obstacles.returnObstacles().Count)]);

    }
}
