using UnityEngine;
using System.Collections;

public class mist : MonoBehaviour {
    public float speed;
    public float lifetime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (lifetime > 0)
        {
            lifetime -= Time.deltaTime;
        }
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
        transform.position = new Vector3(transform.position.x - speed / 2, 8, transform.position.z - speed / 2);
	}
}
