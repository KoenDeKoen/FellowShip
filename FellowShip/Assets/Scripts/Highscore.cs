using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

	public float timescore;
	public float highscore;

	public GameObject highscorepanel;
	public bool ended;

	public Text currentscore, score1, score2, score3;

	string highScoreKey = "HighScore";
	
	void Start()
	{
		timescore = 30f;
		ended = true;
		highscore = PlayerPrefs.GetInt(highScoreKey,0);
	}
	
	void Update(){

		if(ended)
		{
			highscorepanel.gameObject.SetActive(true);

			currentscore.text = "Score:" + timescore.ToString();
			score1.text = "Score: " + highscore.ToString();

			if(timescore > highscore)
			{
				PlayerPrefs.SetFloat(highScoreKey, timescore);
				PlayerPrefs.Save();
			}
		}
	}

	public void AddPoints(float time)
	{
		timescore += time;
	}
}
