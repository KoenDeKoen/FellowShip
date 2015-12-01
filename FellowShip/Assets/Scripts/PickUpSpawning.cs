using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawning : MonoBehaviour {

    // Use this for initialization
    public Pickup pickup;
    private List<Vector3> locations;
    private float borderxmin, borderxmax, borderzmin, borderzmax;
    private int pickupscollected, maxpickups;
    private bool hasspawnednext;

    void Start ()
    {
        locations = new List<Vector3>();
        maxpickups = 5;
        borderxmax = 45;
        borderzmax = 45;
        borderzmin = -45;
        borderxmin = -45;
        //resetGame();
    }

    public void spawnNextPickup()
    {
        pickup.destroyPickup();
        if (pickupscollected < maxpickups)
        {
            pickup.instPickup(locations[pickupscollected]);
            pickupscollected++;
            //UPGRADEEEE
            hasspawnednext = true;
        }
    }

    public void spawnFirstPickup()
    {
        pickup.instPickup(locations[0]);
    }

    public bool hasSpawnedNext()
    {
        return hasspawnednext;
    }

    public void setDoneSpawn()
    {
        hasspawnednext = false;
    }
    public bool checkForDone()
    {
        if (pickupscollected >= maxpickups)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void resetGame()
    {
        pickupscollected = 0;
        hasspawnednext = false;
        for (int i = 0; i < maxpickups; i++)
        {
            float posx = Random.Range(borderxmin, borderxmax);
            float posz = Random.Range(borderzmin, borderzmax);
            if (posx == 0 && posz == 0)
            {
                posx = Random.Range(5, borderzmax);
                posz = Random.Range(5, borderzmax);
            }
            locations.Add(new Vector3(posx, 0, posz));
        }
    }
}
