using UnityEngine;
using System.Collections;


public class BoatCollision : MonoBehaviour
{
    private PickUpSpawning pickupspawner;
    private MenuControl menucontrols;
    private bool spawnnext;

    void Start()
    {
        pickupspawner = FindObjectOfType<PickUpSpawning>().GetComponent<PickUpSpawning>();
        menucontrols = FindObjectOfType<MenuControl>().GetComponent<MenuControl>();
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
        if (other.tag == "Pickup")
        {
            spawnnext = true;
            //Debug.Log("huh");
        }
        if (other.tag == "NormalMode")
        {
            menucontrols.startNormalMode();
        }
        if (other.tag == "TimeTrialMode")
        {
            menucontrols.startTimeTrial();
        }
    }
}
