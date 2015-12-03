//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnableSpots : MonoBehaviour
{
    private List<int> xspots;
    private List<int> zspots;
    private int borderxmin, borderxmax, borderzmin, borderzmax;

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
        int prevzmin = borderzmini;
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

    public void resetSize()
    {
        xspots = new List<int>();
        zspots = new List<int>();
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
    }
}
