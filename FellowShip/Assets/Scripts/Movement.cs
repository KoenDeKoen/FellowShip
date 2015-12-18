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
        if (Input.GetKey("a") && Input.GetKey("d"))
        {
            rotation = speed * rotationSpeed * 1.5f;
            rotation *= Time.deltaTime;
            transform.Translate(0, 0, rotation);
            //transform.Rotate(0, -rotation * 5, 0);
            switch (turnstate)
            {
                case 1:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackL");
                    //Debug.Log(1);
                    turnstate = 0;
                    break;

                case 2:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackR");
                    //Debug.Log(2);
                    turnstate = 0;
                    break;
            }
        }
		if ((Input.GetKey("a") || pct1 >= 0.05f) && !Input.GetKey("d"))
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;
            //Debug.Log(3);
            transform.Translate(0, 0, rotation);
            transform.Rotate(0, -rotation * 5, 0);
            turnstate = 1;
            if (turnstate == 2)
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltLR");
            }
            else
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltL");
            }
        }

        if ((Input.GetKey("d") || pct2 >= 0.05f) && !Input.GetKey("a"))
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;
            //Debug.Log(4);
            transform.Translate(0, 0, rotation);
            transform.Rotate(0, rotation * 5, 0);
            turnstate = 2;
            if (turnstate == 1)
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltRL");
            }
            else
            {
                transform.GetChild(0).GetComponent<Animator>().Play("BoatTiltR");
                //Debug.Log("L");
                
            }
            
            
        }
        else if((!Input.GetKey("a") && !Input.GetKey("d")))// || (pct1 <=0.002f && pct2 <= 0.002f))
        {
            //transform.GetChild(0).transform.Rotate(0, 0, 0);
            switch (turnstate)
            {
                case 1:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackL");
                    turnstate = 0;
                    break;

                case 2:
                    transform.GetChild(0).GetComponent<Animator>().Play("BoatBackR");
                    //Debug.Log("R");
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
