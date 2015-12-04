//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour
{
    public Obstacles obstacles;
    private List<GameObject> spawnedobstacles;
    public SpawnableSpots ss;
    public GameObject place1, place2, place3, place4, place5, place6, place7, 
        place8, place9, place10, place11, place12, place13, place14, place15, place16;
    private List<Vector3> places;
	// Use this for initialization
	void Start ()
    {
        places = new List<Vector3>();
        places.Add(place1.transform.position);
        places.Add(place2.transform.position);
        places.Add(place3.transform.position);
        places.Add(place4.transform.position);
        places.Add(place5.transform.position);
        places.Add(place6.transform.position);
        places.Add(place7.transform.position);
        places.Add(place8.transform.position);
        places.Add(place9.transform.position);
        places.Add(place10.transform.position);
        places.Add(place11.transform.position);
        places.Add(place12.transform.position);
        places.Add(place13.transform.position);
        places.Add(place14.transform.position);
        places.Add(place15.transform.position);
        places.Add(place16.transform.position);
        //Debug.Log(place1.transform.position);
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
