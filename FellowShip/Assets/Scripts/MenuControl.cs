//Made by Koen Brouwers

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pillo;
using System.Collections.Generic;

public class MenuControl : MonoBehaviour
{
    private int state, mode, tutorialstate, tutorialscreen;
    public Sprite normalbtnsprite, highlightedsprite, tutorialfirstpage, tutorialsecondpage;
    public GameObject startbtn, optionsbtn, calibrationbtn, quitbtn, tutorialbtn, tutorialmenubtn, tutorialnextbtn, optionsmenubtn;
    public GameObject mainmenupanel, modeselectpanel, highscorepanel, optionspanel, ingamepanel, tutorialpanel;
    private float time, pillopressval, pilloreleaseval, pct1avarage, pct2avarage;
    private Movement movement;
    public PickUpSpawning pickupspawning;
    private bool inmenu, buttonpressed, pillocontrol,pillocontrolreleased, donewithintro, firstmenupress, intutorial, inoptions;
    public TimeTrial tt;
    public LevelManager lvlm;
    public SpawnObstacles so;
    public SpawnableSpots ss;
    public BoatUpgrade boatupgrade;
	public NewHighscore nhs;
	public PirateShip ps;
    public Options options;
	public CompassAim ca;
    public GameObject timetext;
	public GameObject compassBg;
    public UnityEngine.UI.Slider player1slider, player2slider;
    private List<float> pct1smoother, pct2smoother;
    private GameObject currentselectedbutton;
    private Color selectedcolor, notselectedcolor;
    //private float[] pct1smoother, pct2smoother;
    //public CameraManager cm;
    //private ParticleSystem particlesys;

	float pct1;
	float pct2;
    // Use this for initialization
    void Start ()
    {
        inoptions = false;
        intutorial = false;
        tutorialscreen = 1;
        tutorialstate = 0;
        notselectedcolor = new Color((1f/255f)*84f, (1f / 255f) * 47f, (1f / 255f) * 13f);
        selectedcolor = new Color(1f,1f,1f);
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
        pillocontrolreleased = true;
        pillocontrol = true;
        buttonpressed = false;
        mode = 0;
        inmenu = true;
        time = 1;
        state = 1;
		ca.goalsAvailable = false;
		compassBg.SetActive(false);
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
                    pickupspawning.resetGame();
                    mainmenupanel.SetActive(true);
                    boatupgrade.resetShipModel();
                    movement.onMenuStart();
                    inmenu = true;
                    lvlm.setSmallLevel();
                    so.resetObstacles();
                    ps.DestoryShip();
                    ingamepanel.SetActive(false);
					ca.goalsAvailable = false;
					compassBg.SetActive(false);
                }

                if (mode == 2)
                {
                    tt.stopCounting();
                    highscorepanel.SetActive(true);
                    highscorepanel.GetComponentInChildren<Text>().text = tt.returnTimeInString();
                    nhs.ended = true;
					ca.goalsAvailable = false;
					compassBg.SetActive(false);
                    if (nhs.finished == true)
                    {
                        timetext.SetActive(false);
                        state = 1;
                        highscorepanel.SetActive(false);
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
                    }
                }
                firstmenupress = true;

            }
            if (mode == 2 && !inmenu)
            {
                timetext.GetComponent<Text>().text = "Time: " + tt.returnTimeInString();
            }
            if (inmenu || intutorial)
            {
                checkForPresses();
            }
            if (!inmenu && pillocontrol && !intutorial)
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
                //Debug.Log("hai");
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
        //Debug.Log("Tutorial: " + intutorial + " currentbutton: " + currentselectedbutton + " State: " + tutorialstate);
        if (currentselectedbutton != null)
        {
            currentselectedbutton.GetComponent<Image>().sprite = normalbtnsprite;
            currentselectedbutton.GetComponentInChildren<Text>().color = notselectedcolor;
        }

        //print(startbtn.GetComponentInChildren<Text>().color);
        if (inmenu)
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
                    startbtn.GetComponent<Image>().sprite = highlightedsprite;
                    startbtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = startbtn;
                    break;

                case 2:
                    tutorialbtn.GetComponent<Image>().sprite = highlightedsprite;
                    tutorialbtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = tutorialbtn;
                    break;

                case 3:
                    optionsbtn.GetComponent<Image>().sprite = highlightedsprite;
                    optionsbtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = optionsbtn;
                    break;

                case 4:
                    calibrationbtn.GetComponent<Image>().sprite = highlightedsprite;
                    calibrationbtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = calibrationbtn;
                    break;

                case 5:
                    quitbtn.GetComponent<Image>().sprite = highlightedsprite;
                    quitbtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = quitbtn;
                    break;
            }
        }
        if (intutorial)
        {
            if (!truenumber)
            {
                tutorialstate += amount;
            }
            else
            {
                tutorialstate = amount;
            }
            switch (tutorialstate)
            {

                case 1:
                    //Debug.Log("hai");
                    tutorialnextbtn.GetComponent<Image>().sprite = highlightedsprite;
                    tutorialnextbtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = tutorialnextbtn;
                    break;

                case 2:
                    //Debug.Log("hai2");
                    tutorialmenubtn.GetComponent<Image>().sprite = highlightedsprite;
                    tutorialmenubtn.GetComponentInChildren<Text>().color = selectedcolor;
                    currentselectedbutton = tutorialmenubtn;
                    break;

            }
        }
    }

    private void selectButton()
    {
        if (inmenu)
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
                    startTutorial();
                    break;

                case 3:
                    enableOptions();
                    mainmenupanel.SetActive(false);
                    break;

                case 4:
                    //load calibration
                    //alles = true; (Nikya logic)
                    break;

                case 5:
                    Application.Quit();
                    break;
            }
        }
        if (intutorial)
        {
            switch (tutorialstate)
            {
                case 1:
                    switch (tutorialscreen)
                    {
                        case 0:
                            tutorialpanel.GetComponent<Image>().sprite = tutorialfirstpage;
                            tutorialscreen++;
                            //Debug.Log("1");
                            break;

                        case 1:
                            tutorialpanel.GetComponent<Image>().sprite = tutorialsecondpage;
                            tutorialscreen++;
                            //Debug.Log("2");
                            break;

                        case 2:
                            backToMenuFromTutorial();
                            break;
                    }
                    break;

                case 2:
                    backToMenuFromTutorial();
                    break;
            }
        }
        time = 1;
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
		ca.goalsAvailable = true;
		compassBg.SetActive(true);
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
		ca.goalsAvailable = true;
		compassBg.SetActive(true);
    }

    private void buttonPressed()
    {
        if (!firstmenupress)
        {
           //Debug.Log("LIES");
            //Debug.Log("hai");
            buttonpressed = true;
            time -= Time.deltaTime;
            if (time <= 0)
            {
                //Debug.Log("LIES");
                selectButton();
                time = 1;
                firstmenupress = true;
            }
        }
    }

    private void buttonReleased()
    {
        Debug.Log(firstmenupress);
        if (!firstmenupress)
        {
            buttonpressed = false;
            if (inmenu)
            {
                if (state < 5)
                {
                    changeState(1, false);
                }
                else
                {
                    changeState(1, true);
                }
            }
            if (intutorial)
            {
                if (tutorialstate < 2)
                {
                    changeState(1, false);
                }
                else
                {
                    changeState(1, true);
                }
            }
            time = 1;
        }
        else
        {
            firstmenupress = false;
            buttonpressed = false;
        }
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
        optionspanel.SetActive(true);
        optionsmenubtn.GetComponentInChildren<Text>().color = notselectedcolor;
        options.updateCheckBoxes();
    }
    public void disableOptions()
    {
        inmenu = true;
        optionspanel.SetActive(false);
        mainmenupanel.SetActive(true);
        changeState(1,true);
    }

    public void setIntroFinished()
    {
        donewithintro = true;
        mainmenupanel.SetActive(true);
        changeState(1, true);
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
            pct1smoother.RemoveAt(0);
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
            pct2smoother.RemoveAt(0);
        }
    }

    private void startTutorial()
    {
        tutorialbtn.GetComponentInChildren<Text>().color = notselectedcolor;
        tutorialbtn.GetComponent<Image>().sprite = normalbtnsprite;
        inmenu = false;
        intutorial = true;
        mainmenupanel.SetActive(false);
        tutorialpanel.SetActive(true);
        tutorialpanel.GetComponent<Image>().sprite = tutorialfirstpage;
        currentselectedbutton = null;
        changeState(1, true);
        tutorialscreen = 0;
    }

    private void backToMenuFromTutorial()
    {
        intutorial = false;
        inmenu = true;
        mainmenupanel.SetActive(true);
        tutorialpanel.SetActive(false);
        currentselectedbutton = null;
        changeState(1, true);
    }

    public void changeOptionTextColorOnHoverOver()
    {
        optionsmenubtn.GetComponentInChildren<Text>().color = selectedcolor;
    }

    public void changeOptionTextColorOnHoverExit()
    {
        optionsmenubtn.GetComponentInChildren<Text>().color = notselectedcolor;
    }
}
