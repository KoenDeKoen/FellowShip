using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

	public float timescore, highscore;
	public float second, third;

	public GameObject highscorepanel;
	public bool ended;

	public Text currentscore, score1, score2, score3;

	void Start()
	{
		ended = true;
		highscore = PlayerPrefs.GetFloat("HighScore",0);
		second = PlayerPrefs.GetFloat("Second",0);
		third = PlayerPrefs.GetFloat("Third",0);
	}
	
	void Update(){

		if(ended)
		{
			highscorepanel.gameObject.SetActive(true);

			if(timescore > PlayerPrefs.GetFloat("HighScore"))
			{
				PlayerPrefs.SetFloat("HighScore", timescore);
				PlayerPrefs.Save();
			}

			if(timescore < PlayerPrefs.GetFloat("HighScore"))
			{
				if(timescore > PlayerPrefs.GetFloat("Second"))
				{
					PlayerPrefs.SetFloat("Second", timescore);
					PlayerPrefs.Save();
				}
			}

			if(timescore < PlayerPrefs.GetFloat("Second"))
			{
				if(timescore > PlayerPrefs.GetFloat("Third"))
				{
					PlayerPrefs.SetFloat("Third", timescore);
					PlayerPrefs.Save();
				}
			}

			currentscore.text = "Score:" + timescore.ToString();
			score1.text = "Score: " + PlayerPrefs.GetFloat("HighScore",0);
			score2.text = "Score: " + PlayerPrefs.GetFloat("Second",0);
			score3.text = "Score: " + PlayerPrefs.GetFloat("Third",0);
		}
	}

	public void AddPoints(float time)
	{
		timescore += time;
	}
}
