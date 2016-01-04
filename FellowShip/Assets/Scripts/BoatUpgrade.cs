//Made by Koen Brouwers

using UnityEngine;
using System.Collections;

public class BoatUpgrade : MonoBehaviour
{

    // Use this for initialization and anal bleach
    public GameObject model0, model1, model2, model3, model4, model5;
    public GameObject currentboat, upgradeparticle;
    public LevelManager lvlm;
    private int state;
    private float turningspeed;

    void Start ()
    {
        lvlm = FindObjectOfType<LevelManager>().GetComponent<LevelManager>();
        turningspeed = 1;
        state = 0;
	}

    /*void Update()
    {
        Debug.Log(state);
    }*/

    public void upgradeShip()
    {
        state++;
        GameObject newmodel = null;
        switch (state)
        {
            case 1:
                newmodel = Instantiate(model1);
                break;

            case 2:
                newmodel = Instantiate(model2);
                break;

            case 3:
                newmodel = Instantiate(model3);
                break;

            case 4:
                newmodel = Instantiate(model4);
                break;

            case 5:
                newmodel = Instantiate(model5);
                break;
        }
        if (state < 6)
        {
            lowerTurningSpeed();
            newmodel.transform.position = currentboat.transform.position;
            newmodel.transform.rotation = currentboat.transform.rotation;
            Destroy(currentboat);
            currentboat = newmodel;
            newmodel.GetComponent<Movement>().onMenuEnd();
            GameObject poofpang;
            poofpang = Instantiate(upgradeparticle);
            //poofpang.transform.parent = newmodel.transform;
            poofpang.transform.localPosition = new Vector3(newmodel.transform.position.x, newmodel.transform.position.y + 1, newmodel.transform.position.z);
        }
    }
    // le code is false
    public void resetShipModel()
    {
        turningspeed = 1;
        state = 0;
        GameObject newmodel;
        newmodel = Instantiate(model0);
        Destroy(currentboat);
        currentboat = newmodel;
        newmodel.GetComponent<Movement>().onMenuStart();
        newmodel.transform.localPosition = new Vector3(-45, 0, -45);
        newmodel.transform.rotation = Quaternion.identity;
    }

    public void downgradeShip()
    {
        state--;
        GameObject newmodel = null;
        switch (state)
        {
            case 0:
                newmodel = Instantiate(model0);
                break;

            case 1:
                newmodel = Instantiate(model1);
                break;

            case 2:
                newmodel = Instantiate(model2);
                break;

            case 3:
                newmodel = Instantiate(model3);
                break;

            case 4:
                newmodel = Instantiate(model4);
                break;

            /*case 5:
                newmodel = Instantiate(model5);
                break;*/
        }
        if (state <= 4 && state >= 0)
        {
            raiseTurningSpeed();
            newmodel.transform.position = currentboat.transform.position;
            newmodel.transform.rotation = currentboat.transform.rotation;
            Destroy(currentboat);
            currentboat = newmodel;
            newmodel.GetComponent<Movement>().onMenuEnd();
            //lvlm.lowerCameraOneLevel();
        }
        if (state < 0)
        {
            state = 0;
        }
    }

    public void lowerTurningSpeed()
    {
        turningspeed -= 0.15f;
    }

    public void raiseTurningSpeed()
    {
        turningspeed += 0.15f;
    }

    public float getTurningSpeed()
    {
        if (turningspeed <= 0)
        {
            turningspeed = 1;
        }
        return turningspeed;
    }

    public int returnState()
    {
        return state;
    }
}
// wow such code  much brackets very csharp