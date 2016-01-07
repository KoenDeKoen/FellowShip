//Made by Koen Brouwers.

using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{

    public GameObject playerboat, usedcamera;
    private float time;
    private bool hastolerpcamera;
    private Vector3 newcamerapos, startcameraposition;

    void Start()
    {
        startcameraposition = usedcamera.transform.position;
        hastolerpcamera = false;
        time = 5;
    }

    void Update()
    {
        if (playerboat != null)
        {
            usedcamera.transform.position = new Vector3(playerboat.transform.position.x, usedcamera.transform.position.y, playerboat.transform.position.z -50);
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
                time = 5;
            }
        }
    }

    public void lowerCameraOneLevel()
    {
        time = 5;
        float multiplier = 1.1F;
        hastolerpcamera = true;
        newcamerapos = new Vector3(usedcamera.transform.localPosition.x / multiplier, usedcamera.transform.localPosition.y / (multiplier), usedcamera.transform.localPosition.z / (multiplier));
    }

    public void raiseCameraOneLevel()
    {
        time = 5;
        float multiplier = 1.1f;
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
