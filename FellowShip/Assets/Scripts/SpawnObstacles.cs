using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour
{
    public Obstacles obstacles;
    private List<GameObject> spawnedobstacles;
	// Use this for initialization
	void Start ()
    {
        spawnedobstacles = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void spawnNextObstacle()
    {
        GameObject spawnedobstacle = new GameObject();
        spawnedobstacle = Instantiate(obstacles.returnObstacles()[Random.Range(0, obstacles.returnObstacles().Count)]);
        spawnedobstacles.Add(spawnedobstacle);
    }

    public void resetObstacles()
    {
        for (int i = 0; i < spawnedobstacles.Count; i++)
        {
            Destroy(spawnedobstacles[i]);
            spawnedobstacles.RemoveAt(i);
        }
    }
}
