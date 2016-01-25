using UnityEngine;
using System.Collections;

public class levelIndicator : MonoBehaviour {

	public RectTransform indicator;
	float startMarker;
	float endMarker;
	private float[] posMarker;
	private int state;

	public BoatUpgrade bu;
	// Use this for initialization
	void Start ()
	{
		posMarker = new float[7];
		posMarker[0] = -47.6f;
		posMarker[1] = -30.2f;
		posMarker[2] = -11f;
		posMarker[3] = 8.3f;
		posMarker[4] = 27.6f;
		posMarker[5] = 48f;
		posMarker[6] = 48f;

		startMarker = posMarker[0];
	}
	
	// Update is called once per frame
	void Update ()
	{
		state = bu.returnState();
		endMarker = posMarker[state];

		if(startMarker == endMarker)
		{
			startMarker = posMarker[state];
		}

		indicator.localPosition = new Vector3(Mathf.Lerp(startMarker, endMarker, Time.time),-10,0);
	}
}
