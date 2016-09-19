using UnityEngine;
using System.Collections;

public class MinionSpawner : MonoBehaviour {

    public Transform spawnPoint;

    private Transform[] minionPrefabs;
    public Transform hemoPrefab;
    public Transform globinPrefab;

    public float delay = 5.0f;

    public Vector3 SPAWN_OFFSET = new Vector3( 0.0f, 0.1f, 0.0f );

    public bool active = true;

    private float lastSpawnTime;


    void Awake()
    {
        minionPrefabs = new Transform[2]{ hemoPrefab, globinPrefab };
    }

    // Use this for initialization
  
    void Start () {
            lastSpawnTime = Time.time;
    }
    

    // Update is called once per frame
    void Update () {
        int numBrokenWalls = 0;
        foreach (GameObject go in GameObject.Find("SpatialProcessing").GetComponent<PlaySpaceManager>().allWalls)
            if (go.GetComponent<Wall>().brokenLevel > 0)
                numBrokenWalls++;

        if (active && numBrokenWalls > 0)
        {
            if ((Time.time - lastSpawnTime) >= delay)
            {
                int num = Random.Range(0, 2);
                Instantiate(minionPrefabs[num], spawnPoint.position + SPAWN_OFFSET, Quaternion.identity);
                lastSpawnTime = Time.time;
            }
        }
    }

    void ToggleSpawn()
    {
        active = !active;
    }
}
