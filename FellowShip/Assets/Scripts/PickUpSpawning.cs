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
        hasspawnednext = false;
        locations = new List<Vector3>();
        pickupscollected = 0;
        maxpickups = 5;
        borderxmax = 45;
        borderzmax = 45;
        borderzmin = -45;
        borderxmin = -45;
        for (int i = 0; i < maxpickups; i++)
        {
            float posx = Random.Range(borderxmin, borderxmax);
            float posz = Random.Range(borderzmin, borderzmax);
            if (posx == 0 && posz == 0)
            {
                posx = Random.Range(5, borderzmax);
                posz = Random.Range(5, borderzmax);
            }
            locations.Add(new Vector3(posx, 0, posz ));
        }
        pickup.instPickup(locations[0]);
    }

    public void spawnNextPickup()
    {
        pickupscollected++;
        //UPGRADEEEE
        pickup.destroyPickup();
        if (pickupscollected < maxpickups)
        {
            
            pickup.instPickup(locations[pickupscollected]);
            hasspawnednext = true;
        }
    }

    public bool hasSpawnedNext()
    {
        return hasspawnednext;
    }

    public void setDoneSpawn()
    {
        hasspawnednext = false;
    }

}
