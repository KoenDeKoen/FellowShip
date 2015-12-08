//Made by Koen Brouwers

using UnityEngine;
using System.Collections;


public class BoatCollision : MonoBehaviour
{
    private PickUpSpawning pickupspawner;
    private MenuControl menucontrols;
    private LevelManager lvlm;
    private SpawnObstacles so;
    private BoatUpgrade boatupgrade;
    private bool spawnnext;

    void Start()
    {
        pickupspawner = FindObjectOfType<PickUpSpawning>().GetComponent<PickUpSpawning>();
        menucontrols = FindObjectOfType<MenuControl>().GetComponent<MenuControl>();
        lvlm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        so = FindObjectOfType<SpawnObstacles>().GetComponent<SpawnObstacles>();
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
        
    }

    void Update()
    {
        if (spawnnext && !pickupspawner.hasSpawnedNext())
        {
            spawnnext = false;
            lvlm.increaseLevel();
            pickupspawner.spawnNextPickup();
            pickupspawner.setDoneSpawn();
            boatupgrade.upgradeShip();
            

        }  
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pickup")
        {
            spawnnext = true;
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
