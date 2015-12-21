using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KrakenSpawn : MonoBehaviour {

    public PickUpSpawning pickupspawner;
    // Use this for initialization
    public GameObject kraken, krakenpositionsparent;
    private GameObject currentlyspawnedkraken;
    private List<Vector3> krakenpositions;
    private bool krakenisspawned, hastodespawn;
    private float spawnchecktimer;
    private float despawntimer, submergetimer;
    private int chance;

    void Start ()
    {
        krakenisspawned = false;
        spawnchecktimer = 5;
        despawntimer = 7;
        chance = 0;
        krakenpositions = new List<Vector3>();
        foreach (Transform child in krakenpositionsparent.transform)
        {
            krakenpositions.Add(child.position);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (krakenisspawned)
        {
            despawntimer -= Time.deltaTime;
            /*if (despawntimer <= 2)
            {
                //currentlyspawnedkraken.transform.GetChild(0).GetComponent<Animator>().Play("RIPKraken");
                
            }*/
            if (despawntimer <= 0)
            {
                destroyKraken();
                despawntimer = 5;
            }
        }
        if(chance > 0)
        {
            //Debug.Log("meh");
            spawnchecktimer -= Time.deltaTime;
            
            if (spawnchecktimer <= 0)
            {
                if (chance == 1)
                {
                    //Debug.Log(1);
                    if (Random.Range(0, 3) == 0)
                    {
                        spawnKraken();
                        spawnchecktimer = 5;
                    }
                }
                if (chance == 2)
                {
                    //Debug.Log(2);
                    if (Random.Range(0, 3) == 0 || Random.Range(0, 3) == 1)
                    {
                        spawnKraken();
                        spawnchecktimer = 5;
                    }
                }
                if (chance == 3)
                {
                    //Debug.Log(3);
                    if (Random.Range(0, 3) == 0 || Random.Range(0, 3) == 1 || Random.Range(0, 3) == 2)
                    {
                        spawnKraken();
                        spawnchecktimer = 5;
                    }
                }
                if (chance == 4)
                {
                   //Debug.Log(4);
                    //if (Random.Range(0, 3) == 0 || Random.Range(0, 3) == 1 || Random.Range(0, 3) == 2 || Random.Range(0, 3) == 3)
                    //{
                    spawnKraken();
                        spawnchecktimer = 5;
                    //}
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
        currentlyspawnedkraken.transform.position = krakenpositions[Random.Range(0, krakenpositions.Count)];
        currentlyspawnedkraken.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
        currentlyspawnedkraken.transform.GetChild(0).GetComponent<Animator>().Play("ReleaseTheKraken");
        krakenisspawned = true;
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
}
