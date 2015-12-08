using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour
{
    public GameObject pickupmodel;
    private GameObject currentpickup;

    void Start()
    {
        float randomrotation = Random.Range(0f, 360f);
        //print(randomrotation);
    }

    public void instPickup(Vector3 pos)
    {
        if (currentpickup == null)
        {
            currentpickup = Instantiate(pickupmodel);
        }
        currentpickup.transform.localPosition = new Vector3(pos.x, pickupmodel.transform.position.y, pos.z);
        float randomrotation = Random.Range(0f, 360f);
        currentpickup.transform.localRotation =  Quaternion.Euler(new Vector3(0, randomrotation, 0));
        //Debug.Log(randomrotation);
    }

    public void destroyPickup()
    {
        Destroy(currentpickup);
        currentpickup = null;
    }
}
