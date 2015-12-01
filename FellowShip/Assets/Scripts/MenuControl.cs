﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    private int state;
    public Button startbtn, optionsbtn, calibrationbtn, quitbtn;
    public GameObject mainmenupanel, modeselectpanel;
    private float time;
    public Movement movement;
    public PickUpSpawning pickupspawning;
    private bool inmenu;
    // Use this for initialization
    void Start ()
    {
        inmenu = true;
        time = 1;
        state = 1;
        startbtn.Select();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (pickupspawning.checkForDone())
        {
            state = 1;
            startbtn.Select();
            pickupspawning.resetGame();
            mainmenupanel.SetActive(true);
            movement.resetBoatPos();
            movement.onMenuStart();
            inmenu = true;
        }
        if (inmenu)
        {
            checkForPresses();
        }
    }

    private void checkForPresses()
    {
        if (Input.GetKey("a"))
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
        else if (Input.GetKey("d"))
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

    }

    public void startNormalMode()
    {
        modeselectpanel.SetActive(false);
        pickupspawning.resetGame();
        pickupspawning.spawnFirstPickup();
        inmenu = false;
    }
}
