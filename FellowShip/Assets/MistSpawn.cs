using UnityEngine;
using System.Collections;

public class MistSpawn : MonoBehaviour {
    private Vector3 minsize;
    private Vector3 maxsize;
    private float Spawntime = 0;
    public GameObject Mist;

    void Start()
    {
        minsize = GetComponent<BoxCollider>().center - GetComponent<BoxCollider>().size;
        maxsize = GetComponent<BoxCollider>().center + GetComponent<BoxCollider>().size;
        for (int p = 0; p < 10; p++)
        {
            float begin = Random.Range(-400.00f, 00.00f);
            int size = Random.Range(5, 8);
            float localpos = Random.Range(minsize.z, maxsize.z - 150);
            for (int i = 0; i < size; i++)
            {
                GameObject m = Instantiate((Mist), new Vector3(begin + Random.Range(-30.00f, 30.00f), 1 + size * 1.5f, localpos), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
                m.transform.parent = gameObject.transform;
            }
        }
    }

	void Update () {
        
        if (Spawntime > 0)
        {
            Spawntime -= Time.deltaTime;
        }
        else
        {
            int size = Random.Range(5, 8);
            float localpos = Random.Range(minsize.z, maxsize.z);
            for (int i = 0; i < size; i++)
            {  
                GameObject m = Instantiate((Mist), new Vector3(transform.position.x + Random.Range(-30.00f,30.00f), 1 + size *1.5f, localpos), Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))) as GameObject;
                m.transform.parent = gameObject.transform;
            }
            Spawntime = 4;
        }
    }
}
