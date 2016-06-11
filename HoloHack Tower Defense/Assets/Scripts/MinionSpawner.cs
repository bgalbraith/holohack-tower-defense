using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

    public Transform spawnPoint;
    public Transform minionPrefab;
    public float delay = 1.0f;

    public bool active = true;

    // Use this for initialization
    IEnumerator Start () {
        while (active)
        {
            yield return new WaitForSeconds(1);
            Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
