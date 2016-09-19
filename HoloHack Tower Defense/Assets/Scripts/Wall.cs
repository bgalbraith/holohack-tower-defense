using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

    public int brokenLevel;
    private GameObject endPoint;

    void Start()
    {
        endPoint = GameObject.Find("EndPoint");
    }

    public void breakWall()
    {
        if (brokenLevel < 4)
            brokenLevel++;

        gameObject.GetComponent<Renderer>().material.color = Color.cyan;

        endPoint.transform.position = gameObject.transform.position;
        endPoint.GetComponent<AudioSource>().Play();
    }

    public void repairWall()
    {
        if (brokenLevel > 0)
            brokenLevel--;
        gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }

    void OnSelect()
    {
        breakWall();
    }

}
