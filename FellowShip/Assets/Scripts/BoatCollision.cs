//Made by Koen Brouwers

using UnityEngine;
using System.Collections;


public class BoatCollision : MonoBehaviour
{
    private PickUpSpawning pickupspawner;
    private MenuControl menucontrols;
    private LevelManager lvlm;
	private PirateShip ps;
    //private SpawnObstacles so;
    private BoatUpgrade boatupgrade;
    private KrakenSpawn krakenspawner;
    private bool spawnnext;
    private bool hashitkraken;

    void Start()
    {
        hashitkraken = true;
        pickupspawner = FindObjectOfType<PickUpSpawning>().GetComponent<PickUpSpawning>();
        menucontrols = FindObjectOfType<MenuControl>().GetComponent<MenuControl>();
        lvlm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        krakenspawner = FindObjectOfType<KrakenSpawn>().GetComponent<KrakenSpawn>();
        //so = FindObjectOfType<SpawnObstacles>().GetComponent<SpawnObstacles>();
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
    }

    void Update()
    {
        if (spawnnext && !pickupspawner.hasSpawnedNext())
        {
            spawnnext = false;
            //if (boatupgrade.returnState() < 6)
            //{
                lvlm.increaseLevel();
                pickupspawner.spawnNextPickup();
                pickupspawner.setDoneSpawn();
                boatupgrade.upgradeShip();
            //} 
        }
        if (hashitkraken && !krakenspawner.krakenState())
        {
            //Debug.Log(hashitkraken + ", " + krakenspawner.krakenState());
            hashitkraken = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!hashitkraken)
            {
                //Debug.Log("wtf");
                hashitkraken = true;
                if (boatupgrade.returnState() > 0)
                {
                    //Debug.Log(boatupgrade.returnState());
                    boatupgrade.downgradeShip();
                    
                    pickupspawner.shipGotOuchie();
                }
            }
        }
        if (other.tag == "Pickup")
        {
            spawnnext = true;
        }
		if (other.tag == "Wall")
		{
			Debug.Log("Wall hit");
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

	void OnCollisionEnter(Collision col)
	{
		if(col.collider.tag  == "Pirate")
		{
            if (boatupgrade.returnState() > 0)
            {
                PirateShip.state = 3;
                boatupgrade.downgradeShip();
                pickupspawner.shipGotOuchie();
                //Debug.Log("Pirate");
            }
		}
	}
}
