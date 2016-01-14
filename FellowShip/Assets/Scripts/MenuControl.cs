//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pillo;
using System.Collections.Generic;

public class MenuControl : MonoBehaviour
{
    private int state, mode;
    public Button startbtn, optionsbtn, calibrationbtn, quitbtn;
    public GameObject mainmenupanel, modeselectpanel, highscorepanel, optionspanel, ingamepanel;
    private float time, pillopressval, pilloreleaseval, pct1avarage, pct2avarage;
    private Movement movement;
    public PickUpSpawning pickupspawning;
    private bool inmenu, buttonpressed, pillocontrol,pillocontrolreleased, inoptions, donewithintro, firstmenupress;
    public TimeTrial tt;
    public LevelManager lvlm;
    public SpawnObstacles so;
    public SpawnableSpots ss;
    public BoatUpgrade boatupgrade;
	public NewHighscore nhs;
	public PirateShip ps;
    public Options options;
	//public CompassAim ca;
    public GameObject timetext;
	//public GameObject compassBg;
    public UnityEngine.UI.Slider player1slider, player2slider;
    private List<float> pct1smoother, pct2smoother;
    //private float[] pct1smoother, pct2smoother;
    //public CameraManager cm;
    //private ParticleSystem particlesys;

	float pct1;
	float pct2;
    // Use this for initialization
    void Start ()
    {
        pct1smoother = new List<float>();
        pct2smoother = new List<float>();
        pct1avarage = 0f;
        pct2avarage = 0f;
        //particlesys.startSize = 0.1f;
        
        //pct1smoother = new float[10];
        //pct2smoother = new float[10];
        pillopressval = 0.01f;
        pilloreleaseval = pillopressval / 10;
        firstmenupress = false;
        donewithintro = false;
        inoptions = false;
        pillocontrolreleased = true;
        pillocontrol = true;
        buttonpressed = false;
        mode = 0;
        inmenu = true;
        time = 1;
        state = 1;
        startbtn.Select();
		//compassBg.SetActive(false);
        ConfigureSensorRange(0x50, 0x6f);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (donewithintro)
        {
            pct1 = PilloController.GetSensor(Pillo.PilloID.Pillo1);
            pct2 = PilloController.GetSensor(Pillo.PilloID.Pillo2);
            checkForControlSwitch();
            if (pickupspawning.checkForDone())
            {
                if (mode == 1)
                {
                    state = 1;
                    startbtn.Select();
                    pickupspawning.resetGame();
                    mainmenupanel.SetActive(true);
                    boatupgrade.resetShipModel();
                    movement.onMenuStart();
                    inmenu = true;
                    lvlm.setSmallLevel();
                    so.resetObstacles();
                    ps.DestoryShip();
                    ingamepanel.SetActive(false);
					//compassBg.SetActive(false);
					//ca.goalsAvailable = false;
                }

                if (mode == 2)
                {
                    tt.stopCounting();
                    highscorepanel.SetActive(true);
                    highscorepanel.GetComponentInChildren<Text>().text = tt.returnTimeInString();
                    nhs.ended = true;
                    if (nhs.finished == true)
                    {
                        timetext.SetActive(false);
                        state = 1;
                        highscorepanel.SetActive(false);
                        startbtn.Select();
                        pickupspawning.resetGame();
                        mainmenupanel.SetActive(true);
                        boatupgrade.resetShipModel();
                        movement.onMenuStart();
                        inmenu = true;
                        lvlm.setSmallLevel();
                        ingamepanel.SetActive(false);
                        so.resetObstacles();
                        nhs.finished = false;
                        nhs.doneName = false;
                        nhs.ended = false;
                        tt.resetCounter();
						//compassBg.SetActive(false);
						//ca.goalsAvailable = false;
                    }
                }
                firstmenupress = true;

            }
            if (mode == 2 && !inmenu)
            {
                //try
                //{
                timetext.GetComponent<Text>().text = "Time: " + tt.returnTimeInString();
                //}
                //catch {/*lol so ugly*/ }

            }
            if (inmenu)
            {
                checkForPresses();
            }
            if (!inmenu && pillocontrol)
            {
                updateSliderValues();
            }
        }
    }

    private void checkForPresses()
    {
        if (pillocontrol)
        {
            if (pct1 >= pillopressval || pct2 >= pillopressval)
            {
                buttonPressed();
            }
            if (pct2 <= pilloreleaseval && pct1 <= pilloreleaseval && buttonpressed)
            {
                buttonReleased();
            }
        }
        else
        {
            if (Input.GetKey("a") || Input.GetKey("d"))
            {
                buttonPressed();
            }
            if (!Input.GetKey("d") && !Input.GetKey("a") && buttonpressed)
            {
                buttonReleased();
            }
        }
    }

    //if adding to the current state, truenumber = false. if the amount is the desired state then truenumber = true
    private void changeState(int amount, bool truenumber)
    {
        if (!truenumber)
        {
            state += amount;
        }
        else
        {
            state = amount;
        }
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
                inmenu = false;
                mainmenupanel.SetActive(false);
                modeselectpanel.SetActive(true);
                ingamepanel.SetActive(true);
                break;

            case 2:
                enableOptions();
                mainmenupanel.SetActive(false);
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
        lvlm.setSmallLevel();
        timetext.SetActive(true);
		//compassBg.SetActive(true);
		//ca.goalsAvailable = true;

    }

    public void startNormalMode()
    {
        movement = FindObjectOfType<Movement>().GetComponent<Movement>();
        mode = 1;
        modeselectpanel.SetActive(false);
        pickupspawning.resetGame();
        pickupspawning.spawnFirstPickup();
        movement.onMenuEnd();
        lvlm.setSmallLevel();
        inmenu = false;
		ps.shipspawned = false;
		//compassBg.SetActive(true);
		//ca.goalsAvailable = true;
    }

    private void buttonPressed()
    {
        if (!firstmenupress)
        {
            buttonpressed = true;
            time -= Time.deltaTime;
            if (time <= 0)
            {
                selectButton();
                time = 1;
            }
        }
    }

    private void buttonReleased()
    {
        if (!firstmenupress)
        {
            buttonpressed = false;
            if (state < 4)
            {
                changeState(1, false);
            }
            else
            {
                changeState(-3, false);
            }
            time = 1;
        }
        firstmenupress = false;
    }

    private void checkForControlSwitch()
    {
        if (Input.GetKey("q") && Input.GetKey("w") && Input.GetKey("e") && pillocontrolreleased)
        {
                if (pillocontrol)
                {
                    pillocontrol = false;
                }
                else
                {
                    pillocontrol = true;
                }
                pillocontrolreleased = false;
        }
		if ((!Input.GetKey("q") || !Input.GetKey("w") || !Input.GetKey("e")) && !pillocontrolreleased)
		{
			pillocontrolreleased = true;
		}
    }

    public bool returnControlState()
    {
        return pillocontrol;
    }

    private void enableOptions()
    {
        inmenu = false;
        inoptions = true;
        optionspanel.SetActive(true);
        options.updateCheckBoxes();
    }
    public void disableOptions()
    {
        inmenu = true;
        inoptions = false;
        optionspanel.SetActive(false);
        mainmenupanel.SetActive(true);
        changeState(1,true);
    }

    public void setIntroFinished()
    {
        donewithintro = true;
        mainmenupanel.SetActive(true);
    }

    private static void ConfigureSensorRange(int min, int max)
    {
        PilloSender.SensorMin = min;
        PilloSender.SensorMax = max;
    }

    private void updateSliderValues()
    {
        addToP1Smoother(pct1);
        addToP2Smoother(pct2);
        player1slider.value = pct1avarage;
        player2slider.value = pct2avarage;
    }

    private void addToP1Smoother(float val)
    {
        //Debug.Log(pct1smoother.Count);
        if (pct1smoother.Count < 10)
        {
            pct1smoother.Add(val);
            //Debug.Log(pct1smoother[pct1smoother.Count-1]);
        }
        if (pct1smoother.Count == 10)
        {
            float tempval = 0f;
            for (int i = 0; i < pct1smoother.Count; i++)
            {
                
                tempval += pct1smoother[i];
                
            }
            pct1avarage = tempval / pct1smoother.Count;
            pct1smoother = new List<float>();
        }
    }

    private void addToP2Smoother(float val)
    {
        if (pct2smoother.Count < 10)
        {
            pct2smoother.Add(val);
        }
        if (pct2smoother.Count == 10)
        {
            float tempval = 0f;
            for (int i = 0; i < pct2smoother.Count; i++)
            {
                tempval += pct2smoother[i];
            }
            pct2avarage = tempval / pct2smoother.Count;
            pct2smoother = new List<float>();
        }
    }
}
