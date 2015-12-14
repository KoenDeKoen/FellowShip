using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KrakenSpawn : MonoBehaviour {

    // Use this for initialization
    public GameObject kraken, krakenpositionsparent;
    private GameObject currentlyspawnedkraken;
    private List<Vector3> krakenpositions;
    private bool krakenisspawned;
    private float spawnchecktimer;
    private float despawntimer;

    void Start ()
    {
        krakenisspawned = false;
        spawnchecktimer = 5;
        despawntimer = 5;
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
            if (despawntimer <= 0)
            {
                destroyKraken();
                despawntimer = 5;
            }
        }
        else
        {
            spawnchecktimer -= Time.deltaTime;
            if (spawnchecktimer <= 0)
            {
                if (Random.Range(0, 2) == 1)
                {
                    spawnKraken();
                    spawnchecktimer = 5;
                }
                else
                {
                    spawnchecktimer = 5;
                }
            }
        }
	}

    public void spawnKraken()
    {
        currentlyspawnedkraken = Instantiate(kraken);
        currentlyspawnedkraken.transform.position = krakenpositions[Random.Range(0, krakenpositions.Count)];
        currentlyspawnedkraken.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
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
