//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour
{
    private List<GameObject> obstacles;
    public GameObject obstacle1, obstacle2, obstacle3, obstacle4, obstacle5, obstacle6, obstacle7, obstacle8, obstacle9;
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
        if (obstacle7 != null)
        {
            obstacles.Add(obstacle7);
        }
        if (obstacle8 != null)
        {
            obstacles.Add(obstacle8);
        }
        if (obstacle9 != null)
        {
            obstacles.Add(obstacle9);
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
