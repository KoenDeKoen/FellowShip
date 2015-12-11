//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickUpSpawning : MonoBehaviour {

    public Pickup pickup;
    public SpawnableSpots ss;
    private int pickupscollected, maxpickups;
    private bool hasspawnednext, done;
    public GameObject round1spots, round2spots, round3spots, round4spots, round5spots, round6spots;
    private List<Vector3> round1children, round2children, round3children, round4children, round5children, round6children;
    private Vector3 lastpos;
	public NewHighscore nhs;

    void Start ()
    {
        maxpickups = 7;
        round1children = new List<Vector3>();
        round2children = new List<Vector3>();
        round3children = new List<Vector3>();
        round4children = new List<Vector3>();
        round5children = new List<Vector3>();
        round6children = new List<Vector3>();
        foreach (Transform child in round1spots.transform)
        {
            if (child.tag == "Pickup")
            {
                round1children.Add(child.position);
            }
        }
        foreach (Transform child in round2spots.transform)
        {
            if (child.tag == "Pickup")
            {
                round2children.Add(child.position);
            }
            
        }
        foreach (Transform child in round3spots.transform)
        {
            if (child.tag == "Pickup")
            {
                round3children.Add(child.position);
            }
            
        }
        foreach (Transform child in round4spots.transform)
        {
            if (child.tag == "Pickup")
            {
                round4children.Add(child.position);
            }
            
        }
        foreach (Transform child in round5spots.transform)
        {
            if (child.tag == "Pickup")
            {
                round5children.Add(child.position);
            }
            
        }
        foreach (Transform child in round6spots.transform)
        {
            if (child.tag == "Pickup")
            {
                round6children.Add(child.position);
            }

        }
        resetGame();
    }

    public void spawnNextPickup()
    {
        pickupscollected++;
        Vector3 newpos = new Vector3();
        switch (pickupscollected)
        {
            case 1:
                newpos = round1children[Random.Range(0, round1children.Count)];
                while (newpos == lastpos)
                {
                    newpos = round1children[Random.Range(0, round1children.Count)];
                }
                lastpos = newpos;
                break;

            case 2:
                newpos = round2children[Random.Range(0, round2children.Count)];
                while (newpos == lastpos)
                {
                    newpos = round2children[Random.Range(0, round2children.Count)];
                }
                lastpos = newpos;
                break;

            case 3:
                newpos = round3children[Random.Range(0, round3children.Count)];
                while (newpos == lastpos)
                {
                    newpos = round3children[Random.Range(0, round3children.Count)];
                }
                lastpos = newpos;
                break;

            case 4:
                newpos = round4children[Random.Range(0, round4children.Count)];
                while (newpos == lastpos)
                {
                    newpos = round4children[Random.Range(0, round4children.Count)];
                }
                lastpos = newpos;
                break;

            case 5:
                newpos = round5children[Random.Range(0, round5children.Count)];
                while (newpos == lastpos)
                {
                    newpos = round5children[Random.Range(0, round5children.Count)];
                }
                lastpos = newpos;
                break;

            case 6:
                newpos = round6children[Random.Range(0, round6children.Count)];
                while (newpos == lastpos)
                {
                    newpos = round6children[Random.Range(0, round6children.Count)];
                }
                lastpos = newpos;
                break;
        }
        pickup.destroyPickup();    
        if (pickupscollected < maxpickups)
        {
            pickup.instPickup(newpos);
            hasspawnednext = true;
        }
        else
        {
			nhs.ended = false;
            done = true;
        }
    }

    public void spawnFirstPickup()
    {
        Vector3 newpos = new Vector3();
        newpos = round1children[Random.Range(0, round1children.Count)];
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
        lastpos = new Vector3();
        pickup.destroyPickup();
        ss.resetSize();
        pickupscollected = 1;
        hasspawnednext = false;
        done = false;
    }

    public void shipGotOuchie()
    {
        pickupscollected--;
    }

    public int getMaxPickups()
    {
        return maxpickups;
    }
}
