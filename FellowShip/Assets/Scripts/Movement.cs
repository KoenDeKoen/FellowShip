using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public Rigidbody boat;

	float speed;
	float direction;


	// Use this for initialization
	void Start ()
    {
		boat = GetComponent<Rigidbody>();
		speed = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.A))
		{
			boat.velocity = new Vector3(-10,0,0);
		}
		else if(Input.GetKeyUp(KeyCode.A))
		{
			boat.velocity = new Vector3(0,0,0);
		}

		if(Input.GetKeyDown(KeyCode.D))
		{
			boat.velocity = new Vector3(10,0,0);
		}
		else if(Input.GetKeyUp(KeyCode.D))
		{
			boat.velocity = new Vector3(0,0,0);
		}

		if(Input.GetKeyDown(KeyCode.W))
		{
			boat.velocity = new Vector3(0,0,10);
		}
		else if(Input.GetKeyUp(KeyCode.W))
		{
			boat.velocity = new Vector3(0,0,0);
		}
		
		if(Input.GetKeyDown(KeyCode.S))
		{
			boat.velocity = new Vector3(0,0,-10);
		}
		else if(Input.GetKeyUp(KeyCode.S))
		{
			boat.velocity = new Vector3(0,0,0);
		}
	}
}
