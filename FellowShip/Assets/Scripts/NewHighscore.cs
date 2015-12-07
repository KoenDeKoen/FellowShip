﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class NewHighscore : MonoBehaviour {

	public TimeTrial tt;

	public Text[] text;
	public Text nameText;
	
	public bool doneName;
	public bool ended;

	public string playerName;
	public float timescore;

	public Dictionary<string, float> scores;

	// Use this for initialization
	void Start ()
	{
		ended = false;
		doneName = false;
		playerName = "";
		DisplayHighscore();
		scores = new Dictionary<string, float>();
		for (int i = 0; i < 10; i++) {
			float score = PlayerPrefs.GetFloat("PScore"+i);
			string name = PlayerPrefs.GetString("PName"+i);
			scores.Add(name, score);
		}

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if(ended)
		{
			WriteName();
			if(doneName)
			{
				EnterNewScore();
			}
		}
	}

	public void AddPoints(float time)
	{
		timescore = time;
	}

	public void DisplayHighscore()
	{
		for(int i=0; i<text.Length; i++)
		{
			string name = "PName"+i.ToString();
			string score = "PScore"+i.ToString();
			text[i].text = (i+1)+": "+ PlayerPrefs.GetString(name)+" "+ PlayerPrefs.GetFloat(score);
		}
	}

	public void EnterNewScore()
	{
		float currentScore = float.Parse(tt.returnTimeInString());
		float oldScore = currentScore;
		string playername = playerName;

		if (!scores.ContainsKey(playername))
		{
			scores.Add (playername, currentScore);
		}
		// Loop through keys.
		scores = scores.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
		List<string> keyList = new List<string>(scores.Keys);

		for(int i=0; i<text.Length; i++)
		{
			string scoreKey = "PScore"+i.ToString();
			float score = PlayerPrefs.GetFloat(scoreKey);
//			if(oldScore < score)
//			{
				string name = "PName"+i.ToString();
				string tmpName = PlayerPrefs.GetString(name);
				//float tmpScore = score;
				PlayerPrefs.SetString(name, keyList[i]);
				PlayerPrefs.SetFloat(scoreKey, scores[keyList[i]]);
				oldScore = score;
				playername = tmpName;
//			}
		}
	}

	public void WriteName()
	{
		Event e = Event.current;
		if (e.isKey)
		{
			if(e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString().Length == 1)
			{
				Debug.Log ( e.keyCode.ToString());
				playerName = playerName + e.keyCode.ToString();
			}
			
			if(e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString() == "Backspace")
			{
				playerName = playerName.Substring(0,playerName.Length-1);
			}
			
			if(e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString() == "Space")
			{
				playerName = playerName + " ";
			}
			if (e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString() == "Return")
			{
				doneName = true;
				nameText.text = playerName;
			}
			if(nameText != null)
			{
				nameText.text = playerName;
			}
		}
	}
	
	public string getName()
	{
		return playerName;
	}

}
