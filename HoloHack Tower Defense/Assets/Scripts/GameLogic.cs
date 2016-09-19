using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameLogic : MonoBehaviour {
    private bool started = false;
    private float startTime;
    private float targetTimer;
    private GameObject endPoint;
    private List<GameObject> allWalls;

	// Use this for initialization
	void Start () {
        endPoint = GameObject.Find("EndPoint");
	}
	
	// Update is called once per frame
	void Update () {
        if(!started && GameObject.Find("SpatialProcessing").GetComponent<PlaySpaceManager>().cubesCreated)
        {
            startTime = Time.time;
            targetTimer = Time.time;
            started = true;
        }

        else if(started)
        {
            if((Time.time - targetTimer) > 10f)
            {
                allWalls = GameObject.Find("SpatialProcessing").GetComponent<PlaySpaceManager>().allWalls;
                GameObject wallToBreak = allWalls[Random.Range(0, allWalls.Count)];

                wallToBreak.GetComponent<Wall>().breakWall();

                targetTimer = Time.time;
            }
        }
	
	}
}
