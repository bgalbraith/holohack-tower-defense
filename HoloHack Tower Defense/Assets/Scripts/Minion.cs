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


    private MinionType minionType;
    public MinionType MinionType
    {
        get {   return minionType;  }
        set {   minionType = value; }
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
        //spawnPoint = GameObject.Find("SpawnPoint").transform;
        //endPoint = GameObject.Find("EndPoint").transform;

        Vector3 direction = endPoint.position - spawnPoint.position;
        direction.y = 0.0f;
        startDirection = direction;

        transform.forward = startDirection;
        currentSurfaceNormal = transform.up;

        transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

        StartAction();

    }

    // Update is called once per frame
    void Update()
    {
        if( transform.position.y <= 0.2f)
        {
            transform.position = new Vector3( transform.position.x, 0.35f, transform.position.z);
        }

    }

    
    public void OnCollisionStay(Collision collision)
    {


        Vector3 direction = ((onForwardTraversal) ? endPoint.position : spawnPoint.position) - transform.position;
        direction.y = 0.0f;


        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.rotation = Quaternion.LookRotation((onForwardTraversal) ? collision.contacts[0].otherCollider.transform.up : -collision.contacts[0].otherCollider.transform.up, collision.contacts[0].normal);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Ceiling"))
        {
            transform.GetComponent<Rigidbody>().useGravity = false;
            transform.rotation = Quaternion.LookRotation(direction, collision.contacts[0].normal);
        }
        else if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            transform.GetComponent<Rigidbody>().useGravity = true;
            transform.rotation = Quaternion.LookRotation(direction, collision.contacts[0].normal);
        }

    }



}

public enum MinionType
{
    HEMO,
    GLOBIN
} 
