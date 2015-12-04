using UnityEngine;
using System.Collections;

public class BoatUpgrade : MonoBehaviour
{

    // Use this for initialization
    public GameObject model1, model2, model3, model4, model5;
    public GameObject currentboat;
    private int state;

    void Start ()
    {
        state = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void upgradeShip()
    {
        switch (state)
        {
            case 1:

                break;

            case 2:

                break;

            case 3:

                break;

            case 4:

                break;

            case 5:

                break;
        }
    }
}
