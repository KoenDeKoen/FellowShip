using UnityEngine;
using System.Collections;

public class GolfScript : MonoBehaviour {
    public MeshFilter Meshfilter;
    public float Smooth;
    public float Density;
	// Use this for initialization
	void Start () {
        Meshfilter = transform.GetComponent<MeshFilter>();
	}
	
	// Update is called once per frame
	void Update () {
        Mesh M = Meshfilter.mesh;
        Vector3[] V = M.vertices;
        for (int i = 0; i < V.Length; i++)
        {
            V[i].y = Mathf.Sin(Time.time * Vector2.Distance(new Vector2(V[i].x, V[i].z), Vector2.zero) * Density) * Smooth;
        }
        M.vertices = V;
        Meshfilter.mesh = M;
	}
}
