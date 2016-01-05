//Made by Martijn Koekkoek and Koen Brouwers

using UnityEngine;
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
	public bool finished;
    private bool finishednewscore;

	float pct1;
	float pct2;
		
	public string playerName;
		
	public Dictionary<string, float> scores;
		
		// Use this for initialization
	void Start ()
	{
        //PlayerPrefs.DeleteAll();
        finishednewscore = false;
		ended = false;
		doneName = false;
		playerName = "";
		DisplayHighscore();
		scores = new Dictionary<string, float>();
        for (int i = 0; i < text.Length; i++)
        {
            float score = PlayerPrefs.GetFloat("PScore" + i);
            string name = PlayerPrefs.GetString("PName" + i);
            if (!scores.ContainsKey(name))
            {
                scores.Add(name, score);
            }
            
        }
	}
		
	// Update is called once per frame
	void Update () 
	{
		pct1 = PilloController.GetSensor (Pillo.PilloID.Pillo1);
		pct2 = PilloController.GetSensor (Pillo.PilloID.Pillo2);
	}
		
	void OnGUI()
	{
		if(ended)
		{
            tt.stopCounting();
            if (doneName)
            {
                if (!finishednewscore)
                {
                    EnterNewScore();
                    DisplayHighscore();
                }
                
                
                if ((Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.D)) || pct1 >= 0.05 && pct2 >= 0.05)
                {
                    finished = true;
                    
                }
            }
            else
            {
                WriteName();
            }
		}
	}

//	public void Check()
//	{
//		for(int i=0; i<11; i++)
//		{
//			float currentScore = float.Parse(tt.returnTimeInString());
//			string score = "PScore"+i.ToString();
//			if(currentScore > PlayerPrefs.GetFloat("PScore9"))
//			{
//				doneName = true;
//			}
//		}
//	}
		
	public void DisplayHighscore()
	{
		for(int i=0; i<text.Length; i++)
		{
			float score = PlayerPrefs.GetFloat("PScore"+i);
			string name = PlayerPrefs.GetString("PName"+i);

//			if(string.IsNullOrEmpty(name))
//			{
//				name = "xxx";
//
//			}
//			
//			if(score == 0f)
//			{
//				score = 1000f;
//
//			}
			
			text[i].text = (i)+": "+ name +" "+ score;
		}
	}
		
	public void EnterNewScore()
	{
		float currentScore = float.Parse(tt.returnTimeInString());
		string playername = playerName;
		//float oldScore = currentScore;
			
		if (!scores.ContainsKey(playername))
		{
            //Debug.Log(playername);
			scores.Add (playername, currentScore);
		}

		// Loop through keys.
		scores = scores.OrderBy(i => i.Value).ToDictionary(i => i.Key, i => i.Value);
		List<string> keyList = new List<string>(scores.Keys);
        //Debug.Log(keyList.Count);
		for(int i=0; i<keyList.Count; i++)
		{
			string scoreKey = "PScore"+i.ToString();
		//float score = PlayerPrefs.GetFloat(scoreKey);
//		if(oldScore < score)
//		{
			string name = "PName"+i.ToString();

            //string tmpName = PlayerPrefs.GetString(name);
            //float tmpScore = score;
            //Debug.Log(keyList[i]);
            //if (PlayerPrefs.GetString(name) != "")
            //{
                PlayerPrefs.SetString(name, keyList[i]);
                PlayerPrefs.SetFloat(scoreKey, scores[keyList[i]]);
            //}
            finishednewscore = true;
		//oldScore = score;
		//playername = tmpName;
//				}
		}
	}
		
	public void WriteName()
	{
		Event e = Event.current;
		if (e.isKey)
		{
			if(e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString().Length == 1)
			{
				//Debug.Log ( e.keyCode.ToString());
				playerName = playerName + e.keyCode.ToString();
			}
				
			if(e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString() == "Backspace")
			{
                //Debug.Log(playerName.Length);
                if (playerName.Length - 1 != -1)
                {
                    
                    playerName = playerName.Substring(0, playerName.Length - 1);
                }
			}
				
			if(e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString() == "Space")
			{
				playerName = playerName + " ";
			}

			if (e.type == EventType.KeyDown && e.keyCode.ToString() != "None" && e.keyCode.ToString() == "Return")
			{
				doneName = true;
				nameText.text = playerName;
                finishednewscore = false;
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