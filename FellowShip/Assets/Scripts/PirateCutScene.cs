using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PirateCutScene : MonoBehaviour {

    public RawImage Screen;
    public MovieTexture Cutscene;
    public AudioClip CutsceneAudio;
    public AudioSource Audio;

	void Clip () {
        Screen.texture = Cutscene;
        Audio.clip = CutsceneAudio;
        Audio.Play();
        Cutscene.Play();
    }
    void Update()
    {
        //start a cutscene
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clip();
        }

        //
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Cutscene.Stop();
        }
    }
}
