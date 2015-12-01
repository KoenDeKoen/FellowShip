using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawning : MonoBehaviour {

    // Use this for initialization
    public Pickup pickup;
	public Highscore hs;
    private List<Vector3> locations;
    private float borderxmin, borderxmax, borderzmin, borderzmax;
    private int pickupscollected, maxpickups;
    private bool hasspawnednext, done;

    void Start ()
    {
        //locations = new List<Vector3>();
        maxpickups = 5;
        borderxmax = 45;
        borderzmax = 45;
        borderzmin = -45;
        borderxmin = -45;
        resetGame();
    }

    public void spawnNextPickup()
    {
        pickup.destroyPickup();
        if (pickupscollected < maxpickups)
        {
            pickup.instPickup(locations[0]);
            pickupscollected++;
            //UPGRADEEEE
            hasspawnednext = true;
        }
        else
        {
			hs.ended = true;
            done = true;
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
        return done;
    }

    public void resetGame()
    {
        locations = new List<Vector3>();
        pickupscollected = 0;
        hasspawnednext = false;
        done = false;
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

    public void improveSpawnRange(float borderxmax, float borderxmin, float borderymin, float borderymax)
    {
        locations = new List<Vector3>();
        float posx = Random.Range(borderxmin, borderxmax);
        float posz = Random.Range(borderymin, borderymax);
        locations.Add(new Vector3(posx, 0, posz));
    }
}
