//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour
{
    public Obstacles obstacles;
    private List<GameObject> spawnedobstacles;
    public SpawnableSpots ss;
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
        /*GameObject spawnedobstacle = new GameObject();
        spawnedobstacle = Instantiate(obstacles.returnObstacles()[Random.Range(0, obstacles.returnObstacles().Count)]);
        spawnedobstacle.transform.localPosition = ss.returnSpotForObject((int)spawnedobstacle.GetComponent<Renderer>().bounds.size.x
            , (int)spawnedobstacle.GetComponent<Renderer>().bounds.size.z);
        spawnedobstacle.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        spawnedobstacles.Add(spawnedobstacle);
        for (int i = (int)spawnedobstacle.GetComponent<Renderer>().bounds.min.x; i <= (int)spawnedobstacle.GetComponent<Renderer>().bounds.max.x; i++)
        {
            for (int i2 = (int)spawnedobstacle.GetComponent<Renderer>().bounds.min.z; i2 <= (int)spawnedobstacle.GetComponent<Renderer>().bounds.max.z; i2++)
            {
                //Debug.Log(i + "," + i2);
                ss.removeSpawnableSpot(new Vector3(i, 0, i2));
            }
        }*/
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
