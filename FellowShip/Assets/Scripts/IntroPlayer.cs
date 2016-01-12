using UnityEngine;
using System.Collections;

public class IntroPlayer : MonoBehaviour
{
    public GameObject parent;
    public MenuControl mc;
	// Use this for initialization
	
	// Update is called once per frame

    public void introFinished()
    {
        mc.setIntroFinished();
        parent.SetActive(false);
    }
}
