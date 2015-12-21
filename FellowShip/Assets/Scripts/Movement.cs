//Made by Martijn Koekkoek and Koen Brouwers

using UnityEngine;
using System.Collections;
using Pillo;

public class Movement : MonoBehaviour
{
	float speed;
	float rotationSpeed;
	float rotation;
    private bool finishedmenu;
    private BoatUpgrade boatupgrade;
    private float turningspeed;

	float pct1;
	float pct2;
    int turnstate;

	void Awake ()
    {
        
        turnstate = 0;
        finishedmenu = false;
		rotationSpeed = 5f;
		speed = 5f;
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
        turningspeed = boatupgrade.getTurningSpeed();
        //Debug.Log(turningspeed);
    }

	void Update ()
	{
		pct1 = PilloController.GetSensor (Pillo.PilloID.Pillo1);
		pct2 = PilloController.GetSensor (Pillo.PilloID.Pillo2);

        if (finishedmenu)
        {
            Move();
        }
	}

    private void Move()
    {
        if ((Input.GetKey("a") && Input.GetKey("d")))// || (pct1 >= 0.05f && pct2 >= 0.05f))
        {
            rotation = speed * rotationSpeed * 1.5f;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, rotation);
            switch (turnstate)
            {
                case 1:
                    
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackR");
                    turnstate = 0;
                    break;

                case 2:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackL");
                    turnstate = 0;
                    break;
            }
        }
		if ((Input.GetKey("a")/* || pct1 >= 0.05f*/) && (!Input.GetKey("d") /*|| pct2 <= 0.002f*/))
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, rotation);
            transform.Rotate(0, (-rotation * 5) * turningspeed, 0);
            turnstate = 1;
            if (turnstate == 2)
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltRL");
                
            }
            else
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltR");
            }
        }

        if ((Input.GetKey("d") /*|| pct2 >= 0.05f*/) && (!Input.GetKey("a") /*|| pct1 >= 0.05f*/))
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, rotation);
            transform.Rotate(0, (rotation * 5) * turningspeed, 0);
            turnstate = 2;
            if (turnstate == 1)
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltLR");
            }
            else
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltL");
                
            }
            
            
        }
        else if((!Input.GetKey("a") && !Input.GetKey("d")) /*|| (pct1 <= 0.002f && pct2 <= 0.002f)*/)
        {
            switch (turnstate)
            {
                case 1:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackR");
                    
                    turnstate = 0;
                    break;

                case 2:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackL");
                    turnstate = 0;
                    break;
            }
        }
    }

    public void onMenuEnd()
    {
        finishedmenu = true;
    }

    public void onMenuStart()
    {
        finishedmenu = false;
    }

    public void resetBoatPos()
    {
        boatupgrade.resetShipModel();
        transform.position = new Vector3(-45,transform.position.y, -45);
        transform.rotation = Quaternion.identity;
    }
}
