using UnityEngine;
using System.Collections;

public class MissleDestroy : MonoBehaviour {

    public Transform explosion;
    public float explosionTime = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollsionEnter( Collision collision)
    {
        Destroy(gameObject);
        GameObject explosionTemp = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        Destroy(explosionTemp, explosionTime);
    }
}
