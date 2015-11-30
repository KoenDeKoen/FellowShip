using UnityEngine;
using System.Collections;


public class BoatCollision : MonoBehaviour
{
    private PickUpSpawning pickupspawner;
    private bool spawnnext;

    void Start()
    {
        pickupspawner = FindObjectOfType<PickUpSpawning>().GetComponent<PickUpSpawning>();
    }

    void Update()
    {
        if (spawnnext && !pickupspawner.hasSpawnedNext())
        {
            spawnnext = false;
            pickupspawner.spawnNextPickup();
            pickupspawner.setDoneSpawn();
        }  
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("lol");
        spawnnext = true;
    }
}
