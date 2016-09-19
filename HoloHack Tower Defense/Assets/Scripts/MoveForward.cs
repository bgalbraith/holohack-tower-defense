using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {

    public float speed = 4.0f;
    public bool isActive = false;

	// Use this for initialization
	IEnumerator Start () {

        yield return new WaitForSeconds(4.0f);
        isActive = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
            transform.position += transform.forward * Time.deltaTime * speed;
    }
}
