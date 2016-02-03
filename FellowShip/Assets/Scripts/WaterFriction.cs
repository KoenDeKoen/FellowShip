using UnityEngine;
using System.Collections;

public class WaterFriction : MonoBehaviour {
    public float Strength;
    private bool swap = false;
	void Update ()
    {
        if (swap == false)
        {
            transform.Rotate(0, 0, Strength);
            if (transform.eulerAngles.z > 195)
            {
                swap = true;
            }
        }

        if (swap == true)
        {
            transform.Rotate(0, 0, -Strength);
            if (transform.eulerAngles.z < 165)
            {
                swap = false;
            }
        }
    }
}
