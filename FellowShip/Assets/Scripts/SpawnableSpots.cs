using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnableSpots : MonoBehaviour {

    // Use this for initialization
    private List<Vector3> spots;
    private int[] spotarray;
    private float borderxmin, borderxmax, borderzmin, borderzmax;

    void Start()
    {
        spotarray = new int[90];
        borderxmax = 45;
        borderzmax = 45;
        borderzmin = -45;
        borderxmin = -45;
        //for(int i = 0; i)
    }

    public void addNonSpawnableSpot(Vector3 spot)
    {
        spots.Add(spot);
    }

    public void removeNonSpawnableSpot(Vector3 spot)
    {
        spots.Remove(spot);
    }

    public List<Vector3> returnNonSpawnableSpots()
    {
        return spots;
    }
}
