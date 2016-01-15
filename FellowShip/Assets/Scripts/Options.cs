//Made by Koen Brouwers.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour
{ 
    private int musicenabled;
    //private float musicvolume;
    private bool changingcheckbox;
    public GameObject musiccheckbox;
    public UnityEngine.UI.Slider volumeslider;
	// Use this for initialization
	void Start ()
    {
        changingcheckbox = false;
        musicenabled = PlayerPrefs.GetInt("MusicEnabled", 1);
        //musicvolume = PlayerPrefs.GetFloat("MusicVolume", 50);
        //musicvolume = 0f;
	}

    public void onValueChanged(float volume)
    {
        //Debug.Log(volume);
        //musicvolume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void changeMusicState()
    {
        if (!changingcheckbox)
        {
            Debug.Log("hai");
            if (musicenabled == 1)
            {
                musicenabled = 2;
                PlayerPrefs.SetInt("MusicEnabled", 2);
            }
            else
            {
                musicenabled = 1;
                PlayerPrefs.SetInt("MusicEnabled", 1);
            }
        }
        else
        {
            changingcheckbox = false;
        }
        
    }

    public void updateCheckBoxes()
    {
        
        if (PlayerPrefs.GetInt("MusicEnabled", 1) == 1)
        {
            if (!musiccheckbox.GetComponent<Toggle>().isOn)
            {
                changingcheckbox = true;
                musiccheckbox.GetComponent<Toggle>().isOn = true;
            }
            //changingcheckbox = true;
            
        }
        else
        {
            if (musiccheckbox.GetComponent<Toggle>().isOn)
            {
                changingcheckbox = true;
                musiccheckbox.GetComponent<Toggle>().isOn = false;
            }
                  
        }
        volumeslider.value = PlayerPrefs.GetFloat("MusicVolume", 50);
    }
}
