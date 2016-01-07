//Made by Koen Brouwers.

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Options : MonoBehaviour
{ 
    private bool musicenabled;
    private float musicvolume;
    //public Slider volumeslider;
	// Use this for initialization
	void Start ()
    {
        musicenabled = true;
        musicvolume = 0f;
	}

    public void onValueChanged(float volume)
    {
        Debug.Log(volume);
        musicvolume = volume;
    }


}
