using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public GameObject pickupmodel;
    private GameObject currentpickup;

    public void instPickup(Vector3 pos)
    {
        currentpickup = Instantiate(pickupmodel);
        currentpickup.transform.localPosition = new Vector3(pos.x, pickupmodel.transform.position.y, pos.z);
    }

    public void destroyPickup()
    {
        Destroy(currentpickup);
    }
}
