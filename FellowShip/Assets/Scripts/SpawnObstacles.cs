//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnObstacles : MonoBehaviour
{
    public Obstacles obstacles;
    private List<GameObject> spawnedobstacles;
    public SpawnableSpots ss;
    public GameObject spawnparent;
    private List<Vector3> places;
	// Use this for initialization
	void Start ()
    {
        obstacles.Init();
        spawnedobstacles = new List<GameObject>();
        resetObstacles();
	}

    public void resetObstacles()
    {
        for (int i = 0; i < spawnedobstacles.Count; i++)
        {
            Destroy(spawnedobstacles[i]);
        }
        spawnedobstacles = new List<GameObject>();
        places = new List<Vector3>();
        foreach (Transform child in spawnparent.transform)
        {
            places.Add(child.position);
            //Debug.Log(child.position);
        }

        int randomnumber = Random.Range(5, places.Count);
        for (int i = 0; i < randomnumber; i++)
        {
            GameObject spawnedobstacle;
            spawnedobstacle = Instantiate(obstacles.returnObstacles()[Random.Range(0, obstacles.returnObstacles().Count)]);
            int randomplace = Random.Range(0, places.Count);
            spawnedobstacle.transform.position = places[randomplace];
            
            spawnedobstacle.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
            places.RemoveAt(randomplace);
            spawnedobstacles.Add(spawnedobstacle);
        }
    }
}
