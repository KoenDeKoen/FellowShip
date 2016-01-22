//Made by Martijn Koekkoek and Koen Brouwers

using UnityEngine;
using System.Collections;
using Pillo;

public class Movement : MonoBehaviour
{
	float speed;
	float rotationSpeed;
	float rotationRight;
	float rotationLeft;
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
		//booleans to check if pillos are used
        pillocontrol = true;
        pillocontrolreleased = true;

        mc = FindObjectOfType<MenuControl>().GetComponent<MenuControl>();
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
    }

	void FixedUpdate ()
	{
		//update value of pillos inside
		pct1 = PilloController.GetSensor (Pillo.PilloID.Pillo1);
		pct2 = PilloController.GetSensor (Pillo.PilloID.Pillo2);

		//rotation values of the ships(numbers are speed)
		rotationLeft = pct1 * 8;
		rotationRight = pct2 * 8;

        if (finishedmenu)
        {
            Move();
        }
	}

	//function that allows the use of both the pillo and the keyboard as control to move the player
    private void Move()
    {
        checkForControlSwitch();
		//allows player to use the pillo as control
        if (mc.returnControlState())
        {
            if (pct1 >= pillopressval && pct2 >= pillopressval)
            {
                moveForwardPillo();
            }
            if (pct1 >= pillopressval && pct2 <= pilloreleaseval)
            {
				turnLeftPillo();
			}
			if (pct2 >= pillopressval && pct1 <= pilloreleaseval)
            {
				turnRightPillo();
			}
			else if (pct1 <= pilloreleaseval && pct2 <= pilloreleaseval)
            {
                stopMoving();
            }
        }
		//allows player to use the keyboard as control
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

	//function for the forward movement of the ship(for keyboard)
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
	//function for the forward movement of the ship(for pillo)
	private void moveForwardPillo()
	{
		rotation = speed * rotationSpeed * 1.5f;
		rotation *= Time.deltaTime;
		transform.Translate(0, 0, rotation);
		transform.Rotate(0, (rotation * (rotationRight - rotationLeft)) * turningspeed, 0);
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
	//function for the turning movement to the right for the ship(for keyboard)
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
	//function for the turning movement to the right for the ship(for pillo)
	private void turnRightPillo()
	{
		rotation = speed * rotationRight;
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
	//function for the turning movement to the left for the ship(for keyboard)
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
	//function for the turning movement to the left for the ship(for pillo)
	private void turnLeftPillo()
	{
		rotation = speed * rotationLeft;
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
	//function that change animation back to stationary
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
	//function changes the control between pillo and keyboard
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
