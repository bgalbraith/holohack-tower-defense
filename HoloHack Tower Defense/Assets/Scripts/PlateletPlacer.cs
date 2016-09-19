using UnityEngine;
using System.Collections;

public class PlateletPlacer : MonoBehaviour {

    private int currentCount;

    void Awake()
    {
        currentCount = 0;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlaceInNextPlateletLocation( Transform platelet)
    {
        int numberPlaces = transform.childCount;
        int id = currentCount % numberPlaces;
        string nameOfPlace = "PlateletA_" + id.ToString();
        platelet.position = transform.GetChild(id).transform.position;
        platelet.rotation = transform.GetChild(id).transform.rotation;
        currentCount++;
    }
}
