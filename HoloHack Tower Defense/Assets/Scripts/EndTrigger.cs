using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider other )
    {
        
        if( other.gameObject.tag == "Minion")
        {
            StartCoroutine( other.gameObject.GetComponent<Minion>().IntermediateAction() );
        }
    }
        
    


}
