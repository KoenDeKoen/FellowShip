//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour
{
    private List<GameObject> obstacles;
    public GameObject obstacle1;
    public GameObject obstacle2;
    public GameObject obstacle3;
    public GameObject obstacle4;
    public GameObject obstacle5;
    public GameObject obstacle6;
    // Use this for initialization
    public void Init ()
    {
        obstacles = new List<GameObject>();
        if (obstacle1 != null)
        {
            obstacles.Add(obstacle1);
        }
        if (obstacle2 != null)
        {
            obstacles.Add(obstacle2);
        }
        if (obstacle3 != null)
        {
            obstacles.Add(obstacle3);
        }
        if (obstacle4 != null)
        {
            obstacles.Add(obstacle4);
        }
        if (obstacle5 != null)
        {
            obstacles.Add(obstacle5);
        }
        if (obstacle6 != null)
        {
            obstacles.Add(obstacle6);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public List<GameObject> returnObstacles()
    {
        return obstacles;
    }
}
