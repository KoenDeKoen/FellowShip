//Made by Koen Brouwers

using UnityEngine;
using System.Collections;


public class BoatCollision : MonoBehaviour
{
    private PickUpSpawning pickupspawner;
    private MenuControl menucontrols;
    //private LevelManager lvlm;
	private PirateShip ps;
    //private SpawnObstacles so;
    private BoatUpgrade boatupgrade;
    private KrakenSpawn krakenspawner;
    private bool spawnnext;
    private bool hashitkraken;
	public bool canHitPirate;
	private float pirateTime;
   	GameObject collisionParticles;
    AudioSource sfx;
    public AudioClip pirateCollision;
    public AudioClip islandCollision;
    public AudioClip krakenCollision;
    public AudioClip pickupCollision;
	GameObject currentboat;
	GameObject destruction;
    void Start()
    {
        canHitPirate = false;
        hashitkraken = true;
        pickupspawner = FindObjectOfType<PickUpSpawning>().GetComponent<PickUpSpawning>();
        menucontrols = FindObjectOfType<MenuControl>().GetComponent<MenuControl>();
        //lvlm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        krakenspawner = FindObjectOfType<KrakenSpawn>().GetComponent<KrakenSpawn>();
        //so = FindObjectOfType<SpawnObstacles>().GetComponent<SpawnObstacles>();
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
		pirateTime = 8.0f;
		collisionParticles = GameObject.FindGameObjectWithTag("CollisionParticle");
        //sfx = GameObject.Find("SfxPlayer").GetComponent<AudioSource>();
    }

    void Update()
    {
        Debug.Log(pirateTime);
        if (spawnnext && !pickupspawner.hasSpawnedNext())
        {
            spawnnext = false;
            //if (boatupgrade.returnState() < 6)
            //{
                //lvlm.increaseLevel();
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

		if(canHitPirate == false)
		{
            canHitPirate = false;
			pirateTime -= Time.deltaTime;
			if(pirateTime <= 0.0f)
			{
				canHitPirate = true;
			}
		}

		currentboat = boatupgrade.currentboat;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (!hashitkraken)
            {
                //sfx.PlayOneShot(krakenCollision);
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
        if (col.gameObject.tag == "Island")
        {
            Debug.Log("Island Collision");
			//boatupgrade.currentboat.GetComponent<Rigidbody>().isKinematic = true;
            //sfx.PlayOneShot(islandCollision);
        }

		if(col.gameObject.tag  == "Pirate")
		{
			if(canHitPirate)
			{
            	if (boatupgrade.returnState() > 0) //&& collisionParticles != null)
            	{
                    canHitPirate = false;
                    pirateTime = 8.0f;
                    destruction = Instantiate(collisionParticles);
					destruction.transform.position = new Vector3(currentboat.transform.position.x, currentboat.transform.position.y, currentboat.transform.position.z);
                    //collisionParticles.GetComponent<ParticleSystem>().Play();
                    //sfx.PlayOneShot(pirateCollision);
                	PirateShip.state = 3;
                	boatupgrade.downgradeShip();
                	pickupspawner.shipGotOuchie();
            	}
			}
		}
	}
}
