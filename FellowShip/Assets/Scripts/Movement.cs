using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
	float speed;
	float rotationSpeed;
	float rotation;

	// Use this for initialization
	void Start ()
    {
		rotationSpeed = 5f;
		speed = 5f;
	}

	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey("a"))
		{
			rotation = speed * rotationSpeed;
			rotation *= Time.deltaTime;

			transform.Translate(0, 0, rotation);
			transform.Rotate(0, -rotation * 5, 0);
		}

		if(Input.GetKey("d"))
		{
			rotation = speed * rotationSpeed;
			rotation *= Time.deltaTime;
			
			transform.Translate(0, 0, rotation);
			transform.Rotate(0, rotation * 5, 0);
		}
	}
}
