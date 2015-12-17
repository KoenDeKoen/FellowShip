﻿//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pillo;

public class MenuControl : MonoBehaviour
{
    private int state, mode;
    public Button startbtn, optionsbtn, calibrationbtn, quitbtn;
    public GameObject mainmenupanel, modeselectpanel, highscorepanel;
    private float time;
    private Movement movement;
    public PickUpSpawning pickupspawning;
    private bool inmenu;
    public TimeTrial tt;
    public LevelManager lvlm;
    public SpawnObstacles so;
    public SpawnableSpots ss;
    public BoatUpgrade boatupgrade;
	public NewHighscore nhs;

	float pct1;
	float pct2;
    // Use this for initialization
    void Start ()
    {
        mode = 0;
        inmenu = true;
        time = 1;
        state = 1;
        startbtn.Select();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		pct1 = PilloController.GetSensor (Pillo.PilloID.Pillo1);
		pct2 = PilloController.GetSensor (Pillo.PilloID.Pillo2);

        if (pickupspawning.checkForDone())
        {
            if (mode == 1)
            {
                state = 1;
                startbtn.Select();
                pickupspawning.resetGame();
                mainmenupanel.SetActive(true);
                //movement.resetBoatPos();
                boatupgrade.resetShipModel();
                movement.onMenuStart();
                inmenu = true;
                //ss.resetSize();
                lvlm.resetSize();
                so.resetObstacles();
                //changeState(0);
                //ss.resetSize();
            }

            if (mode == 2)
            {
                tt.stopCounting();
                highscorepanel.SetActive(true);
                highscorepanel.GetComponentInChildren<Text>().text = tt.returnTimeInString();
				nhs.ended = true;
				if(nhs.finished == true)
				{
					state = 1;
					highscorepanel.SetActive(false);
					startbtn.Select();
					pickupspawning.resetGame();
					mainmenupanel.SetActive(true);
					boatupgrade.resetShipModel();
					movement.onMenuStart();
					inmenu = true;
					lvlm.resetSize();
					so.resetObstacles();
					nhs.finished = false;
					nhs.doneName = false;
					nhs.ended = false;
				}
            }
        }
        if (inmenu)
        {
            
            checkForPresses();
        }
    }

    private void checkForPresses()
    {
        if (Input.GetKey("a") || pct1 >= 0.05f)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                if (state < 4)
                {
                    changeState(1);
                }
                else
                {
                    changeState(-3);
                }
                time = 1;
            }
        }
		else if (Input.GetKey("d") || pct2 >= 0.05f)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                selectButton();
                time = 1;
            }
        }
        else
        {
            time = 1;
        }
    }

    private void changeState(int amount)
    {
        state += amount;
        switch (state)
        {
            case 1:
                startbtn.Select();
                break;

            case 2:
                optionsbtn.Select();
                break;

            case 3:
                calibrationbtn.Select();
                break;

            case 4:
                quitbtn.Select();
                break;
        }
    }

    private void selectButton()
    {
        movement = FindObjectOfType<Movement>().GetComponent<Movement>();
        switch (state)
        {
            case 1:
                movement.onMenuEnd();
                mainmenupanel.SetActive(false);
                modeselectpanel.SetActive(true);
                break;

            case 2:
                //load options
                break;

            case 3:
                //load calibration
                break;

            case 4:
                //quit
                break;
        }
    }

    public void startTimeTrial()
    {
        movement = FindObjectOfType<Movement>().GetComponent<Movement>();
        mode = 2;
        modeselectpanel.SetActive(false);
        pickupspawning.resetGame();
        pickupspawning.spawnFirstPickup();
        inmenu = false;
        tt.startCounting();
    }

    public void startNormalMode()
    {
        movement = FindObjectOfType<Movement>().GetComponent<Movement>();
        mode = 1;
        modeselectpanel.SetActive(false);
        pickupspawning.resetGame();
        pickupspawning.spawnFirstPickup();
        movement.onMenuEnd();
        //pickupspawning.spawnNextPickup();
        inmenu = false;
    }
}
