//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnableSpots : MonoBehaviour
{
    private List<int> xspots;
    private List<int> zspots;
    private int borderxmin, borderxmax, borderzmin, borderzmax;
    public int size;
    private List<Vector3> availableSpots;

    void Start()
    {
        resetSize();
    }

    public List<int> returnXSpots()
    {
        return xspots;
    }

    public List<int> returnZSpots()
    {
        return zspots;
    }

    public void removeXSpot(int spot)
    {
        xspots.Remove(spot);
    }

    public void removeZSpot(int spot)
    {
        zspots.Remove(spot);
    }

    public void improveSpawnRange(int borderxmaxi, int borderxmini, int borderzmini, int borderzmaxi)
    {
        int prevxmax = borderxmax;
        int prevxmin = borderxmin;
        int prevzmax = borderzmax;
        int prevzmin = borderzmin;
        borderxmax = borderxmaxi;
        borderxmin = borderxmini;
        borderzmax = borderzmaxi;
        borderzmin = borderzmini;
        for (int i = prevxmax; i < borderxmax; i++)
        {
            xspots.Add(i);
        }
        for (int i = prevxmin; i > borderxmin; i++)
        {
            xspots.Add(i);
        }
        for (int i = prevzmax; i < borderzmax; i++)
        {
            zspots.Add(i);
        }
        for (int i = prevzmin; i < borderzmin; i++)
        {
            zspots.Add(i);
        }
    }

    public Vector3 returnPos()
    {
        return availableSpots[Random.Range(0, availableSpots.Count)];
    }

    public void resetSize()
    {
        xspots = new List<int>();
        zspots = new List<int>();
        availableSpots = new List<Vector3>();
        borderxmax = 45;
        borderzmax = 45;
        borderzmin = -45;
        borderxmin = -45;
        for (int i = borderxmin; i <= borderxmax; i++)
        {
            xspots.Add(i);
        }
        for (int i = borderzmin; i <= borderzmax; i++)
        {
            zspots.Add(i);
        }
        for (int i = borderxmin; i <= borderxmax; i++)
        {
            for (int i2 = borderzmin; i2 <= borderzmax; i2++)
            {
                availableSpots.Add(new Vector3(i, 0, i2));
            }
        }
        size = availableSpots.Count;
    }

    public void removeSpawnableSpot(Vector3 spot)
    {
        for (int i = 0; i < availableSpots.Count; i++)
        {
            if (spot == availableSpots[i])
            {
                availableSpots.Remove(spot);
                size = availableSpots.Count;
                return;
            }
        }
    }

    public Vector3 returnSpotForObject(int xboundary, int zboundary)
    {
        int tempxmax = borderxmax - xboundary;
        int tempxmin = borderxmin + xboundary;
        int tempzmax = borderzmax - zboundary;
        int tempzmin = borderzmin + zboundary;

        int xval = Random.Range(tempxmin, tempxmax);
        int zval = Random.Range(tempzmin, tempzmax);
        return new Vector3(xval, 0, zval);
    }
}
