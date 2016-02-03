using UnityEngine;
using System.Collections;

public class WaterFriction : MonoBehaviour {
    public float Strength;
    private int Begin = 180;
    private bool swap = false;
	void Update ()
    {
        /*
        if (gameObject.name != ("boat_01"))
        {
            Begin = 360;
        }
        if (swap == false)
        {
            transform.Rotate(0, 0, Strength);
            if (transform.eulerAngles.z > Begin + 15)
            {
                swap = true;
            }
        }

        if (swap == true)
        {
            transform.Rotate(0, 0, -Strength);
            if (transform.eulerAngles.z < Begin - 15)
            {
                swap = false;
            }
        */
    }
}
