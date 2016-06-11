using UnityEngine;
using System.Collections;

public class MinionPathFinder : MonoBehaviour {

    private Vector3 spawnPointPos;
    private Vector3 endPointPos;

    private Vector3 startDirection;

    public Transform contactPoint;
    public bool onWall = false;

    public float TARGET_DIST_EPSILON = 0.5f;
    public float WALL_DIST_EPSILON = 0.05f;


    void Awake()
    {
        spawnPointPos = GameObject.Find("SpawnPoint").transform.position;
        endPointPos = GameObject.Find("EndPoint").transform.position;

        Vector3 direction = endPointPos - spawnPointPos;
        direction.y = 0.0f;
        startDirection = direction;
    }

	// Use this for initialization
	void Start () {
        transform.forward = startDirection;
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 direction = endPointPos - contactPoint.position;
        Vector3 heading = direction;
        heading.y = 0.0f;
        
        if (onWall)
        {
            if (Vector3.Distance(endPointPos, contactPoint.position) < TARGET_DIST_EPSILON)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (heading.sqrMagnitude < WALL_DIST_EPSILON)
            {
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.forward = transform.up;
                onWall = true;
            }
        }

        

        

    }

}
