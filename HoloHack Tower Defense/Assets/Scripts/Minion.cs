using UnityEngine;
using System.Collections;

public abstract class Minion : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform endPoint;

    private Vector3 startDirection;

    private Vector3 currentSurfaceNormal;
    public float surfaceNormalThreshold = 0.01f;

    public bool onForwardTraversal = true;
    public bool intermediateActionOver = false;
    private GameObject endWall;


    private MinionType minionType;
    public MinionType MinionType
    {
        get { return minionType; }
        set { minionType = value; }
    }

    abstract public void StartAction();
    abstract public IEnumerator IntermediateAction();
    abstract public void EndAction();


    void Awake()
    {

    }

    // Use this for initialization
    public virtual void Start()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        //endPoint = GameObject.Find("EndPoint").transform;
        float closestDistance = 999f;
        float goDistance;
        foreach (GameObject go in GameObject.Find("SpatialProcessing").GetComponent<PlaySpaceManager>().allWalls)
        {
            if (go.GetComponent<Wall>().brokenLevel > 0)
            {
                goDistance = Vector3.Distance(spawnPoint.position, go.transform.position);
                if (goDistance < closestDistance)
                {
                    closestDistance = goDistance;
                    endPoint = go.transform;
                    endWall = go;
                }
            }
        }

        Vector3 direction = endPoint.position - spawnPoint.position;
        direction.y = 0.0f;
        startDirection = direction;

        transform.forward = startDirection.normalized;
        currentSurfaceNormal = transform.up;

        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        transform.GetComponent<Rigidbody>().useGravity = true;

        StartAction();
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.y < GameObject.Find("SpatialProcessing").GetComponent<PlaySpaceManager>().floorY)
        {
            transform.position = new Vector3(transform.position.x, GameObject.Find("SpatialProcessing").GetComponent<PlaySpaceManager>().floorY, transform.position.z);
        }

        if (onForwardTraversal)
        {
            if (Vector3.Distance(transform.position, endPoint.position) < 2f)
            {
                endWall.GetComponent<Wall>().repairWall();
                Destroy(gameObject);

                //StartCoroutine(IntermediateAction());
            }

            else if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(endPoint.position.x, endPoint.position.z)) < 2.2f)
            {
                transform.GetComponent<Rigidbody>().useGravity = false;
                transform.forward = (endPoint.position - transform.position).normalized;
                /*
                if(transform.forward.magnitude < 2)
                {
                    transform.forward = transform.forward.normalized * 2f;
                }
                */
            }  
        }
        
        else if(intermediateActionOver)
        {
            transform.forward = (spawnPoint.position - transform.position).normalized;

            if(Vector3.Distance(transform.position, spawnPoint.position) < 0.4f)
            {
                Destroy(gameObject);
            }
        }
        
    }
}

public enum MinionType
{
    HEMO,
    GLOBIN
} 
