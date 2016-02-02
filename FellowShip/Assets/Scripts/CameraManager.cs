//Made by Koen Brouwers.

using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public GameObject playerboat, usedcamera;
    private float time;
    private bool hastolerpcamera;
    private Vector3 newcamerapos, startcameraposition;
    private float multiplier = 1.1f;

    void Start()
    {
        startcameraposition = usedcamera.transform.position;
        hastolerpcamera = false;
        time = 1;
    }

    void Update()
    {
        if (playerboat != null)
        {
            usedcamera.transform.position = new Vector3(playerboat.transform.position.x, usedcamera.transform.position.y, playerboat.transform.position.z - 50);
            //Debug.Log("X: " + usedcamera.transform.position.x + "Y: " + usedcamera.transform.position.y + "Z: " + usedcamera.transform.position.z);
        }
        lerpThatCamera();
    }

    public void attachCameraToPlayer()
    {
        //usedcamera.transform.SetParent(playerboat.transform);
    }

    public void detachCameraFromPlayer()
    {
        usedcamera.transform.SetParent(null);
    }

    public void lerpThatCamera()
    {
        if (hastolerpcamera)
        {
            time -= Time.deltaTime;
            usedcamera.transform.localPosition = Vector3.MoveTowards(usedcamera.transform.localPosition, newcamerapos, Time.deltaTime * 25);
            if (time <= 0)
            {
                hastolerpcamera = false;
                time = 1;
            }
        }
    }

    public void lowerCameraOneLevel()
    {
        time = 1;
        hastolerpcamera = true;
        newcamerapos = new Vector3(usedcamera.transform.localPosition.x / multiplier, usedcamera.transform.localPosition.y / (multiplier), usedcamera.transform.localPosition.z / (multiplier));
    }

    public void raiseCameraOneLevel()
    {
        time = 1;
        hastolerpcamera = true;
        newcamerapos = new Vector3(usedcamera.transform.localPosition.x * multiplier, usedcamera.transform.localPosition.y * (multiplier), usedcamera.transform.localPosition.z * (multiplier));
    }

    public void setNewPlayerBoat(GameObject boat)
    {
        playerboat = boat;
        //attachCameraToPlayer();
    }

    public void resetCamera()
    {
        usedcamera.transform.position = startcameraposition;
    }

}
