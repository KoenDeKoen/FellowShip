using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PirateCutScene : MonoBehaviour {

    public RawImage Screen;
    public MovieTexture Cutscene;
    public AudioClip CutsceneAudio;
    public AudioSource Audio;
    private float timeLeft = 0;

	public void Clip (MovieTexture Cut, AudioClip Audi) {
        Screen.texture = Cut;
        Audio.clip = Audi;
        Audio.Play();
        Cutscene.Play();
        timeLeft = Cut.duration;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            Screen.enabled = true;
            timeLeft -= Time.deltaTime;
        }
        else
        {
            Screen.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clip(Cutscene, CutsceneAudio);
        }
    }
}
