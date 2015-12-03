//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawning : MonoBehaviour {

    public Pickup pickup;
	public Highscore hs;
    public SpawnableSpots ss;
    private float borderxmin, borderxmax, borderzmin, borderzmax;
    private int pickupscollected, maxpickups;
    private bool hasspawnednext, done;

    void Start ()
    {
        maxpickups = 5;
        borderxmax = 45;
        borderzmax = 45;
        borderzmin = -45;
        borderxmin = -45;
        resetGame();
    }

    public void spawnNextPickup()
    {   
        int posx = ss.returnXSpots()[Random.Range(0, ss.returnXSpots().Count)];
        int posz = ss.returnZSpots()[Random.Range(0, ss.returnZSpots().Count)];
        Vector3 newpos = new Vector3(posx, 0, posz);
        pickup.destroyPickup();
        if (pickupscollected < maxpickups)
        {
            pickup.instPickup(newpos);
            pickupscollected++;
            //UPGRADEEEE
            hasspawnednext = true;
        }
        else
        {
			//hs.ended = true;
            done = true;
        }
    }

    public void spawnFirstPickup()
    {
        int posx = ss.returnXSpots()[Random.Range(0, ss.returnXSpots().Count)];
        int posz = ss.returnZSpots()[Random.Range(0, ss.returnZSpots().Count)];
        Vector3 newpos = new Vector3(posx, 0, posz);
        pickup.instPickup(newpos);
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
        pickupscollected = 0;
        hasspawnednext = false;
        done = false;
        float posx = ss.returnXSpots()[Random.Range(0, ss.returnXSpots().Count)];
        float posz = ss.returnZSpots()[Random.Range(0, ss.returnZSpots().Count)];
    }
}
