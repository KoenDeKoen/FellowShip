//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KrakenSpawn : MonoBehaviour
{
    public PickUpSpawning pickupspawner;
    public GameObject kraken, krakenpositionsparent, bubbles;
    private GameObject currentlyspawnedkraken;
    private List<Vector3> krakenpositions;
    private bool krakenisspawned, hastospawn, bubblesspawned;
    private float spawnchecktimer, bubbletimer;
    private float despawntimer;
    private int chance;
    private Vector3 spawnpos;
    private GameObject currentlyspawnedbubbles;

    void Start ()
    {
        bubblesspawned = false;
        bubbletimer = 5f;
        spawnpos = new Vector3();
        krakenisspawned = false;
        spawnchecktimer = 5;
        despawntimer = 10;
        chance = 0;
        krakenpositions = new List<Vector3>();
        foreach (Transform child in krakenpositionsparent.transform)
        {
            krakenpositions.Add(child.position);
        }
    }
	
	void Update ()
    {
        if (hastospawn)
        {
            bubbletimer -= Time.deltaTime;
            if (!bubblesspawned)
            {
                bubbleTheBubbles();
            }
            if (bubbletimer <= 0)
            {
                debubbleTheBubbles();
                spawnKraken();
                bubbletimer = 5;
            }
        }

        if (krakenisspawned)
        {
            despawntimer -= Time.deltaTime;
            if (despawntimer <= 2)
            {
                currentlyspawnedkraken.transform.GetChild(0).GetComponent<Animator>().Play("RIPKraken");  
            }
            if (despawntimer <= 0)
            {
                destroyKraken();
                despawntimer = 5;
            }
        }
        if(chance > 0 && !krakenisspawned && !hastospawn)
        {
            spawnchecktimer -= Time.deltaTime;
            
            if (spawnchecktimer <= 0)
            {
                if (chance == 1)
                {
                    if (Random.Range(0, 3) == 0)
                    {
                        spawnpos = krakenpositions[Random.Range(0, krakenpositions.Count)];
                        hastospawn = true;
                    }
                }
                if (chance == 2)
                {
                    if (Random.Range(0, 3) == 0 || Random.Range(0, 3) == 1)
                    {
                        spawnpos = krakenpositions[Random.Range(0, krakenpositions.Count)];
                        hastospawn = true;
                    }
                }
                if (chance == 3)
                {
                    if (Random.Range(0, 3) == 0 || Random.Range(0, 3) == 1 || Random.Range(0, 3) == 2)
                    {
                        spawnpos = krakenpositions[Random.Range(0, krakenpositions.Count)];
                        hastospawn = true;
                    }
                }
                if (chance == 4)
                {
                    spawnpos = krakenpositions[Random.Range(0, krakenpositions.Count)];
                    hastospawn = true;
                }
                else
                {
                    spawnchecktimer = 5;
                }
            }
        }
        if (pickupspawner.getCurrentPickups() == 3)
        {
            chance = 1;
        }
        if (pickupspawner.getCurrentPickups() == 4)
        {
            chance = 2;
        }
        if (pickupspawner.getCurrentPickups() == 5)
        {
            chance = 3;
        }
        if (pickupspawner.getCurrentPickups() == 6)
        {
            chance = 4;
        }
        if (pickupspawner.getCurrentPickups() < 3)
        {
            chance = 0;
        }
    }

    public void spawnKraken()
    {
        currentlyspawnedkraken = Instantiate(kraken);
        currentlyspawnedkraken.transform.position = spawnpos;
        currentlyspawnedkraken.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        spawnchecktimer = 5;
        krakenisspawned = true;
        hastospawn = false;
        bubblesspawned = false;
    }

    public void destroyKraken()
    {
        Destroy(currentlyspawnedkraken);
        krakenisspawned = false;
    }

    public bool krakenState()
    {
        return krakenisspawned;
    }

    public void bubbleTheBubbles()
    {
        currentlyspawnedbubbles = Instantiate(bubbles);
        currentlyspawnedbubbles.transform.position = spawnpos;
        bubblesspawned = true;
    }

    public void debubbleTheBubbles()
    {
        Destroy(currentlyspawnedbubbles);
    }
}
