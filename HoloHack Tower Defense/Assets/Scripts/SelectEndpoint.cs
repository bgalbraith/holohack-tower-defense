using UnityEngine;

public class SelectEndpoint : MonoBehaviour
{
    GameObject endPoint;

    void Start()
    {
        endPoint = GameObject.Find("EndPoint");
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {

        //endPoint.transform.position = transform.position;
        //GetComponent<Renderer>().material.color = Color.blue;
        //endPoint.GetComponent<AudioSource>().Play();
    }
    
}