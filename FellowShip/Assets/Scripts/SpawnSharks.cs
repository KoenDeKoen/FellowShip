//Made by Koen Brouwers
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnSharks : MonoBehaviour
{
    public GameObject sharkspawnlocationsparent, sharkprefab;
    private List<Vector3> sharkspawnlocations;
    private Vector3 currentsharkposition;
    private GameObject currentlyspawnedshark;

	void Start ()
    {
        sharkspawnlocations = new List<Vector3>();
        foreach (Transform child in sharkspawnlocationsparent.transform)
        {
            sharkspawnlocations.Add(child.position);
        }
    }

    public void resetSharks()
    {
        Destroy(currentlyspawnedshark);
        currentsharkposition = sharkspawnlocations[Random.Range(0, sharkspawnlocations.Count)];
        currentlyspawnedshark = Instantiate(sharkprefab);
        if (Random.value >= 0.5f)
        {
            currentlyspawnedshark.transform.GetChild(0).GetComponent<Animator>().SetBool("Clockwise", true);
        }
        else
        {
            currentlyspawnedshark.transform.GetChild(0).GetComponent<Animator>().SetBool("Clockwise", false);
        }
        currentlyspawnedshark.transform.GetChild(0).GetComponent<Animator>().SetBool("Go", true);
        currentlyspawnedshark.transform.position = currentsharkposition;
        currentlyspawnedshark.transform.rotation = Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0));
    }
}
