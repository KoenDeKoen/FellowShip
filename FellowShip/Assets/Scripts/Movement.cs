//Made by Martijn Koekkoek and Koen Brouwers

using UnityEngine;
using System.Collections;
using Pillo;

public class Movement : MonoBehaviour
{
	float speed;
	float rotationSpeed;
	float rotation;
    private bool finishedmenu;
    private BoatUpgrade boatupgrade;

	// Use this for initialization
	void Awake ()
    {
        finishedmenu = false;
		rotationSpeed = 5f;
		speed = 5f;
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
	}

	// Update is called once per frame
	void Update ()
	{
        //Debug.Log("hoi");
        if (finishedmenu)
        {
            //Debug.Log("hoi2");
            Move();
        }
	}

    private void Move()
    {
        if (Input.GetKey("a"))
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;

            transform.Translate(0, 0, rotation);
            transform.Rotate(0, -rotation * 5, 0);
        }

        if (Input.GetKey("d"))
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;

            transform.Translate(0, 0, rotation);
            transform.Rotate(0, rotation * 5, 0);
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
}
