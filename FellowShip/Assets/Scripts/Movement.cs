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

	float pct1;
	float pct2;

	void Awake ()
    {
        finishedmenu = false;
		rotationSpeed = 5f;
		speed = 5f;
        boatupgrade = FindObjectOfType<BoatUpgrade>().GetComponent<BoatUpgrade>();
	}

	void Update ()
	{
		pct1 = PilloController.GetSensor (Pillo.PilloID.Pillo1);
		pct2 = PilloController.GetSensor (Pillo.PilloID.Pillo2);

        if (finishedmenu)
        {
            Move();
        }
	}

    private void Move()
    {
		if (Input.GetKey("a") || pct1 >= 0.05f)
        {
            rotation = speed * rotationSpeed;
            rotation *= Time.deltaTime;

            transform.Translate(0, 0, rotation);
            transform.Rotate(0, -rotation * 5, 0);
        }

		if (Input.GetKey("d") || pct2 >= 0.05f)
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
