using UnityEngine;
using System.Collections;

public class GiantController : MonoBehaviour {

    public bool DemoMode = true;

    Animator animator;
    float elapsed;
    string[] actions = new string[] {"hit1", "hit2", "death", "punchRight", "punchLeft", "slamRight", "slamLeft", "slamBig", "downGrab", "downLook"};

    // Use this for initialization
	void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        elapsed = 0f;
	}
	
	// Update is called once per frame
	void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 5.0f)
        {
            elapsed = 0f;
            int i = Random.Range(0, 9);
            animator.SetTrigger(actions[i]);
        }
	}
}
