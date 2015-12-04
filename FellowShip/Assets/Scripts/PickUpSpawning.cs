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
    public GameObject place1, place2, place3, place4, place5, place6, place7,
        place8, place9, place10, place11, place12, place13, place14, place15, place16;
    public List<Vector3> places;

    void Start ()
    {
        places = new List<Vector3>();
        places.Add(place1.transform.position);
        places.Add(place2.transform.position);
        places.Add(place3.transform.position);
        places.Add(place4.transform.position);
        places.Add(place5.transform.position);
        places.Add(place6.transform.position);
        places.Add(place7.transform.position);
        places.Add(place8.transform.position);
        places.Add(place9.transform.position);
        places.Add(place10.transform.position);
        places.Add(place11.transform.position);
        places.Add(place12.transform.position);
        places.Add(place13.transform.position);
        places.Add(place14.transform.position);
        places.Add(place15.transform.position);
        places.Add(place16.transform.position);
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
        ss.resetSize();
        pickupscollected = 0;
        hasspawnednext = false;
        done = false;
        float posx = ss.returnXSpots()[Random.Range(0, ss.returnXSpots().Count)];
        float posz = ss.returnZSpots()[Random.Range(0, ss.returnZSpots().Count)];
    }
}
