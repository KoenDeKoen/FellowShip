using UnityEngine;
using System.Collections;

public class TimeTrial : MonoBehaviour
{

    // Use this for initialization
    private float time;
    private bool start;

	public Highscore hs;
	void Start ()
    {
        start = true;
	    time = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (start)
        {
            time += Time.deltaTime;
			hs.AddPoints(time);
        }
	}

    public string returnTimeInString()
    {
        string somestring = time.ToString();
        int dot = somestring.IndexOf(".");
        string doubletime = somestring.Substring(0, dot) + somestring.Substring(dot,3);
        return doubletime;

    }

    public void startCounting()
    {
        start = true;
    }

    public void stopCounting()
    {
        start = false;
    }
}
