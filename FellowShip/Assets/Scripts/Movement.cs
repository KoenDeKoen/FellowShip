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

	void Awake ()
    {
        finishedmenu = false;
		rotationSpeed = 5f;
		speed = 5f;
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
	}

	void Update ()
	{
        if (finishedmenu)
        {
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
