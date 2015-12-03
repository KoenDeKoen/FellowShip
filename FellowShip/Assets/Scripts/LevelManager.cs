using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{

    // Use this for initialization
    private int sizestate;
    public GameObject wallL, wallR, wallU, wallD, seafloor;
    public PickUpSpawning pickupspawning;
    public Camera maincamera;
    private bool hastolerpcamera;
    private Vector3 newcamerapos, startwallL, startwallR, startwallU, startwallD, startscalewallL, 
        startscalewallR, startscalewallU, startscalewallD, startscaleseafloor, startcamerapos;

	void Start ()
    {
        hastolerpcamera = false;
        sizestate = 1;
        startwallL = wallL.transform.localPosition;
        startwallR = wallR.transform.localPosition;
        startwallU = wallU.transform.localPosition;
        startwallD = wallD.transform.localPosition;
        startscalewallL = wallL.transform.localScale;
        startscalewallR = wallR.transform.localScale;
        startscalewallU = wallU.transform.localScale;
        startscalewallD = wallD.transform.localScale;
        startscaleseafloor = seafloor.transform.localScale;
        startcamerapos = maincamera.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(hastolerpcamera)
        {
            maincamera.transform.localPosition = Vector3.MoveTowards(maincamera.transform.localPosition, newcamerapos, Time.deltaTime * 8);
            if (maincamera.transform.localPosition.x >= newcamerapos.x && maincamera.transform.localPosition.z <= newcamerapos.z)
            {
                hastolerpcamera = false;
            }
        }
	}

    public void raiseSizeByOne()
    {
        sizestate++;
    }

    public void increaseLevel()
    {
        float multiplier = 1.2F;
        hastolerpcamera = true;
        newcamerapos = new Vector3(maincamera.transform.localPosition.x, maincamera.transform.localPosition.y * multiplier, maincamera.transform.localPosition.z * multiplier);
        wallL.transform.localPosition = new Vector3(wallL.transform.position.x * multiplier, wallL.transform.position.y, wallL.transform.position.z);
        wallR.transform.localPosition = new Vector3(wallR.transform.position.x * multiplier, wallR.transform.position.y, wallR.transform.position.z);
        wallU.transform.localPosition = new Vector3(wallU.transform.position.x, wallU.transform.position.y, wallU.transform.position.z * multiplier);
        wallD.transform.localPosition = new Vector3(wallD.transform.position.x, wallD.transform.position.y, wallD.transform.position.z * multiplier);
        seafloor.transform.localScale = new Vector3(seafloor.transform.localScale.x * multiplier, seafloor.transform.localScale.y, seafloor.transform.localScale.z * multiplier);
        wallL.transform.localScale = new Vector3(wallL.transform.localScale.x * multiplier, wallL.transform.localScale.y, wallL.transform.localScale.z * multiplier);
        wallD.transform.localScale = new Vector3(wallD.transform.localScale.x * multiplier, wallD.transform.localScale.y, wallD.transform.localScale.z * multiplier);
        wallU.transform.localScale = new Vector3(wallU.transform.localScale.x * multiplier, wallU.transform.localScale.y, wallU.transform.localScale.z * multiplier);
        wallR.transform.localScale = new Vector3(wallR.transform.localScale.x * multiplier, wallR.transform.localScale.y, wallR.transform.localScale.z * multiplier);
        pickupspawning.improveSpawnRange(wallL.transform.localPosition.x + 10, wallD.transform.localPosition.x - 10, wallU.transform.localPosition.z - 10, wallD.transform.localPosition.z + 10);
    }

    public void resetSize()
    {
        wallL.transform.localPosition = startwallL;
        wallR.transform.localPosition = startwallR;
        wallU.transform.localPosition = startwallU;
        wallD.transform.localPosition = startwallD;
        seafloor.transform.localScale = startscaleseafloor;
        wallL.transform.localScale = startscalewallL;
        wallD.transform.localScale = startscalewallR;
        wallU.transform.localScale = startscalewallU;
        wallR.transform.localScale = startscalewallD;
        maincamera.transform.localPosition = startcamerapos;
        hastolerpcamera = false;
    }
}
