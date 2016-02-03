using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PirateCutScene : MonoBehaviour {

    public RawImage Screen;
    public MovieTexture Cutscene;
    public AudioClip CutsceneAudio;
    public AudioSource Audio;

	public void Clip (MovieTexture Cut, AudioClip Audi) {
        Screen.texture = Cutscene;
        Audio.clip = CutsceneAudio;
        Audio.Play();
        Cutscene.Play();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Clip(Cutscene, CutsceneAudio);
        }
    }
}
