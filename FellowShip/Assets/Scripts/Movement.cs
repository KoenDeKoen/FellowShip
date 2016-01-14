//Made by Martijn Koekkoek and Koen Brouwers

using UnityEngine;
using System.Collections;
using Pillo;

public class Movement : MonoBehaviour
{
	float speed;
	float rotationSpeed;
	float rotation;
    private bool finishedmenu, pillocontrol, pillocontrolreleased;
    private BoatUpgrade boatupgrade;
    private float turningspeed, pillopressval, pilloreleaseval;
    private MenuControl mc;

	float pct1;
	float pct2;
    int turnstate;

    void Start()
    {

        pillocontrol = true;
        pillocontrolreleased = true;
        mc = FindObjectOfType<MenuControl>().GetComponent<MenuControl>();
        //ConfigureSensorRange(0x50, 0x6f);
    }

	void Awake ()
    {
        pillopressval = 0.02f;
        pilloreleaseval = pillopressval / 10;
        turnstate = 0;
        finishedmenu = false;
		rotationSpeed = 5f;
		speed = 5f;
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
        turningspeed = boatupgrade.getTurningSpeed();
        
        //Debug.Log(turningspeed);
    }

	void FixedUpdate ()
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
        checkForControlSwitch();
        if (mc.returnControlState())
        {
            if (pct1 >= pillopressval && pct2 >= pillopressval)
            {
                moveForward();
            }
            if (pct1 >= pillopressval && pct2 <= pilloreleaseval)
            {
                turnLeft();
            }
            if (pct2 >= pillopressval && pct1 <= pilloreleaseval)
            {
                turnRight();
            }
            else if (pct1 <= pilloreleaseval && pct2 <= pilloreleaseval)
            {
                stopMoving();
            }
        }
        else
        {
            if ((Input.GetKey("a") && Input.GetKey("d")))
            {
                moveForward();
                //Debug.Log(1);
            }
            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                turnLeft();
                //Debug.Log(2);
            }
            if (Input.GetKey("d") && !Input.GetKey("a"))
            {
                turnRight();
                //Debug.Log(3);
            }
            else if (!Input.GetKey("a") && !Input.GetKey("d"))
            {
                stopMoving();
                //Debug.Log(4);
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

    private void moveForward()
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

    private void turnRight()
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

    private void turnLeft()
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

    private void stopMoving()
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
    private void checkForControlSwitch()
    {
        if (Input.GetKey("q") && Input.GetKey("w") && Input.GetKey("e"))
        {
            if (pillocontrolreleased)
            {
                if (pillocontrol)
                {
                    pillocontrol = false;
                }
                else
                {
                    pillocontrol = true;
                }
            }
        }
        if (!Input.GetKey("q") || !Input.GetKey("w") || !Input.GetKey("e"))
        {
            pillocontrolreleased = true;
        }
    }

    /*private static void ConfigureSensorRange(int min, int max)
    {
        PilloSender.SensorMin = min;
        PilloSender.SensorMax = max;
    }*/
}
