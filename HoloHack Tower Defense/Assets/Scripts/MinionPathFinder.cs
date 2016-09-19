using UnityEngine;
using System.Collections;

[RequireComponent( typeof( Minion ) )]
public class MinionPathFinder : MonoBehaviour {

    private Transform spawnPoint;
    private Transform endPoint;

    private Vector3 startDirection;

    private Vector3 currentSurfaceNormal;
    public float surfaceNormalThreshold = 0.01f;

    public bool forwardTraversal = true;


    void Awake()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        endPoint = GameObject.Find("EndPoint").transform;
    }

	// Use this for initialization
	void Start () {

        Vector3 direction = endPoint.position - spawnPoint.position;
        direction.y = 0.0f;
        startDirection = direction;

        transform.forward = startDirection;
        currentSurfaceNormal = transform.up;

        //transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
	}
	
	// Update is called once per frame
	void Update () {
    }

    public void OnCollisionEnter(Collision collision) {    
        if( forwardTraversal ){

            foreach (ContactPoint contact in collision.contacts)
            {
                Debug.Log(Vector3.Dot(contact.normal, transform.up));

                if( Vector3.Dot( contact.normal, transform.up ) <= surfaceNormalThreshold)
                {
                    currentSurfaceNormal = contact.normal;
                    transform.forward = transform.up;
                    //transform.up = currentSurfaceNormal;

                    break;
                }
            }
        }
    }

}
