using UnityEngine;

public class DragDrop : MonoBehaviour {

    // Am I being dragged?
    public bool selected;

    private HoloToolkit.Unity.GazeManager cursor;
    private HoloToolkit.Unity.GestureManager gm;

    // Use this for initialization
    void Start () {
        selected = false;
        cursor = GameObject.Find("Cursor").GetComponent<HoloToolkit.Unity.GazeManager>();
        gm = GameObject.Find("Cursor").GetComponent<HoloToolkit.Unity.GestureManager>();
        transform.position = GameObject.Find("MainCamera").transform.position;
        GetComponent<Renderer>().material.color = Color.red;
    }
	
	// Update is called once per frame
	void Update () {
	    if(selected)
        {
            transform.position = cursor.Position + new Vector3(0.0f, 0.15f, 0.0f);
        }
	}

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        if(!selected)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
            GetComponent<MinionSpawner>().active = false;
            selected = true;
            gm.OverrideFocusedObject = gameObject;
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = false;
            }
        }
        else if(transform.position.y < 0.1)
        {
            GetComponent<Renderer>().material.color = Color.red;
            GetComponent<MinionSpawner>().active = true;
            selected = false;
            gm.OverrideFocusedObject = null;
            foreach (Collider c in GetComponents<Collider>())
            {
                c.enabled = true;
            }
        }
    }
}
