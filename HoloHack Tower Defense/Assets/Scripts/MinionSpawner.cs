using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

    //public Transform spawnPoint;

    private Transform[] minionPrefabs;
    public Transform hemoPrefab;
    public Transform globinPrefab;

    private Transform[] spawnPoints;
    public Transform hemoSpawnPoint;
    public Transform globinSpawnPoint;

    public float delay = 6.0f;

    public Vector3 SPAWN_OFFSET = new Vector3( 0.0f, 0.35f, 0.0f );

    public bool active = true;


    void Awake()
    {
        minionPrefabs = new Transform[2]{ hemoPrefab, globinPrefab };
        spawnPoints = new Transform[2]{ hemoSpawnPoint, globinSpawnPoint };
    }

    // Use this for initialization
    IEnumerator Start () {
        while (active)
        {
            yield return new WaitForSeconds(delay);

            int num = Random.Range(0, 2);
            Instantiate(minionPrefabs[num], spawnPoints[num].position + SPAWN_OFFSET, Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void ToggleSpawn()
    {
        active = !active;
    }
}
