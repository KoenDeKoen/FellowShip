using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Pillo;

public class PilloCalibration : MonoBehaviour {
	
	public MenuControl mc;
	public Text infoText;
	public bool pillo1, pillo2, pressed;
	public float timer;
	float pct1, pct2;
	int selectedPillo;
	int m_state;
	
	void Start () {
		pillo1 = false;
		pillo2 = false;
		pressed = true;
		m_state = 0;
	}
	
	// Update is called once per frame
	void Update () {
		pct1 = PilloController.GetSensor(Pillo.PilloID.Pillo1);
		pct2 = PilloController.GetSensor(Pillo.PilloID.Pillo2);

		//checks if calibrationpanel is active
		if(mc.incalibration)
		{
			//prevents calibration without input
			if((pct1 <= 0.2f && pct2 <= 0.2f))
			{
				pressed = false;
			}
		}

		//goes through serveral funtions to calibrate the pillos
		if(!pressed)
		{
			switch(m_state)
			{
			case 1:
				CheckForSelection();
				break;
			case 2:
				DoCountdown();
				break;
			case 3:
				DoMaximumCalibration();
				break;
			case 4:
				DoMinimumCalibration();
				break;
			case 5:
				DoCalibrationComplete();
				break;
			}
		}
	}

	//checks which pillo is pressed and starts the calibration
	void CheckForSelection()
	{
		if(PilloController.GetSensor(Pillo.PilloID.Pillo1,false) > PilloController.GetCalibratedMinimum(Pillo.PilloID.Pillo1))
		{
			selectedPillo = 0;
			timer += Time.deltaTime;
		}
		else
			if(PilloController.GetSensor(Pillo.PilloID.Pillo2,false) > PilloController.GetCalibratedMinimum(Pillo.PilloID.Pillo2))
		{
			selectedPillo = 1;
			timer += Time.deltaTime;
		}
		else
		{
			timer = 0.0f;
		}
		if(timer >= 2.0f)
		{
			StartCountdown();
		}
	}

	void StartCountdown()
	{
		m_state = 2;
		timer = 0.0f;
	}

	//checks which pillo is pressed and countdown 
	void DoCountdown()
	{
		timer += Time.deltaTime;
		infoText.text = "Starting calibration for Pillo " + (selectedPillo + 1).ToString () + " in " + Mathf.CeilToInt (3.0f-timer).ToString () + "\n Hold Pillo " +(selectedPillo + 1).ToString () + " as tight as possible!" ;
		if (timer > 3.0f)
			SwitchToMaximumCalibration ();
	}

	void SwitchToMaximumCalibration()
	{
		m_state = 3;
		timer = 0.0f;
		infoText.text = "Keep holding Pillo " + (selectedPillo + 1).ToString () + " as tight as possible!";
	}

	//set the pillos maximum
	void DoMaximumCalibration()
	{
		timer += Time.deltaTime;
		if (timer > 2.0f)
		{
			PilloController.SetCalibratedMaximum (PilloController.GetSensor ((Pillo.PilloID)selectedPillo, false), (Pillo.PilloID)selectedPillo);
			SwitchToMinimumCalibration();
		}
	}

	void SwitchToMinimumCalibration()
	{
		m_state = 4;
		timer = 0.0f;
		infoText.text = "Let go of Pillo " + (selectedPillo + 1).ToString () + " now!";
	}

	//set the pillos minimum
	void DoMinimumCalibration ()
	{
		timer += Time.deltaTime;
		if (timer > 3.0f)
		{
			PilloController.SetCalibratedMinimum (PilloController.GetSensor ((Pillo.PilloID)selectedPillo, false), (Pillo.PilloID)selectedPillo);
			SwitchToCalibrationComplete();
		}
	}
	//checks if pillo are already calibrated or not
	void SwitchToCalibrationComplete()
	{
		m_state = 5;
		timer = 0.0f;
		infoText.text = "Pillo " + (selectedPillo + 1).ToString () + " is calibrated, well done!";
		if((selectedPillo + 1) == 1)
		{
			pillo1 = true;
		}
		if((selectedPillo + 1) == 2)
		{
			pillo2 = true;
		}
	}

	//if all pillos are set saves values and return to menu, if not return to first function
	void DoCalibrationComplete()
	{
		if(pillo1 == false || pillo2 == false)
		{
			timer += Time.deltaTime;
			if (timer > 3.0f)
			{
				SwitchToWaitingForSelection();
			}
		}
		if(pillo1 == true && pillo2 == true)
		{
			PilloController.SaveCalibrationValues ();
			BackToMenu();
		}
		Debug.Log("Pillo1 "+ pillo1);
		Debug.Log("Pillo2 "+ pillo2);
	}

	//saves the calibration and requests to press first pillo
	void SwitchToWaitingForSelection()
	{
		m_state = 1;
		PilloController.SaveCalibrationValues ();
		timer = 0.0f;
		infoText.text = "Press the Pillo you want to calibrate";
	}
	//function to return to the menu with a countdown
	void BackToMenu()
	{
		timer += Time.deltaTime;
		infoText.text = "Going back to the main menu in " +  (int)timer;
		if (timer > 3.0f)
		{
			mc.disableCalibration();
			timer = 0.0f;
		}
	}

	public void StartCalibration()
	{
		m_state = 1;
	}
}
