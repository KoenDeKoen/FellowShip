using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour
{
    private int state;
    public Button startbtn, optionsbtn, calibrationbtn, quitbtn;
	// Use this for initialization
	void Start ()
    {
        state = 1;
        startbtn.Select();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown("a") && Input.GetKeyDown("d"))
        {
            switch (state)
            {
                case 1:
                    startbtn.Select();
                    
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
        else if (Input.GetKeyDown("a"))
        {
            if (state < 4)
            {
                changeState(1);
            }
            else
            {
                changeState(-3);
            }
        }
        else if (Input.GetKeyDown("d"))
        {
            if (state > 1)
            {
                changeState(-1);
            }
            else
            {
                changeState(3);
            }
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
                //load options
                break;

            case 3:
                calibrationbtn.Select();
                //load calibration
                break;

            case 4:
                quitbtn.Select();
                //quit
                break;
        }
    }
}
