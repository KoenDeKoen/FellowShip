using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Highscore : MonoBehaviour {

	public float timescore;
	//public float second, third;
	private float t1, t2, t3, t4, t5, t6, t7, t8, t9, t10;

	public bool ended;

	public Text text1, text2, text3, text4, text5, text6, text7, text8, text9, text10;

	void Start()
	{
		ended = false;
//		highscore = PlayerPrefs.GetFloat("HighScore",0);
//		second = PlayerPrefs.GetFloat("Second",0);
//		third = PlayerPrefs.GetFloat("Third",0);
		t1 = PlayerPrefs.GetFloat("t1",0);
		t2 = PlayerPrefs.GetFloat("t2",0);
		t3 = PlayerPrefs.GetFloat("t3",0);
		t4 = PlayerPrefs.GetFloat("t4",0);
		t5 = PlayerPrefs.GetFloat("t5",0);
		t6 = PlayerPrefs.GetFloat("t6",0);
		t7 = PlayerPrefs.GetFloat("t7",0);
		t8 = PlayerPrefs.GetFloat("t8",0);
		t9 = PlayerPrefs.GetFloat("t9",0);
		t10 = PlayerPrefs.GetFloat("t10",0);
	}
	
	void Update(){

		if(ended)
		{
//			if(timescore > PlayerPrefs.GetFloat("HighScore"))
//			{
//				PlayerPrefs.SetFloat("HighScore", timescore);
//				PlayerPrefs.Save();
//			}
//
//			if(timescore < PlayerPrefs.GetFloat("HighScore"))
//			{
//				if(timescore > PlayerPrefs.GetFloat("Second"))
//				{
//					PlayerPrefs.SetFloat("Second", timescore);
//					PlayerPrefs.Save();
//				}
//			}
//
//			if(timescore < PlayerPrefs.GetFloat("Second"))
//			{
//				if(timescore > PlayerPrefs.GetFloat("Third"))
//				{
//					PlayerPrefs.SetFloat("Third", timescore);
//					PlayerPrefs.Save();
//				}
//			}

			if(timescore > PlayerPrefs.GetFloat("t10"))
			{
				PlayerPrefs.SetFloat("t10", timescore);
				PlayerPrefs.Save();
			}

			SaveTime("t10", "t9");
			SaveTime("t9", "t8");
			SaveTime("t8", "t7");
			SaveTime("t7", "t6");
			SaveTime("t6", "t5");
			SaveTime("t5", "t4");
			SaveTime("t4", "t3");
			SaveTime("t3", "t2");
			SaveTime("t2", "t1");

			text1.text = "Score: " + PlayerPrefs.GetFloat("t1",0);
			text2.text = "Score: " + PlayerPrefs.GetFloat("t2",0);
			text3.text = "Score: " + PlayerPrefs.GetFloat("t3",0);
			text4.text = "Score: " + PlayerPrefs.GetFloat("t4",0);
			text5.text = "Score: " + PlayerPrefs.GetFloat("t5",0);
			text6.text = "Score: " + PlayerPrefs.GetFloat("t6",0);
			text7.text = "Score: " + PlayerPrefs.GetFloat("t7",0);
			text8.text = "Score: " + PlayerPrefs.GetFloat("t8",0);
			text9.text = "Score: " + PlayerPrefs.GetFloat("t9",0);
			text10.text = "Score: " + PlayerPrefs.GetFloat("t10",0);
		}
	}

	public void AddPoints(float time)
	{
		timescore = time;
	}

	public void SaveTime(string place, string compare)
	{
		if(timescore < PlayerPrefs.GetFloat(place))
		{
			if(timescore > PlayerPrefs.GetFloat(compare))
			{
				PlayerPrefs.SetFloat(compare, timescore);
				PlayerPrefs.Save();
			}
		}
	}
}
