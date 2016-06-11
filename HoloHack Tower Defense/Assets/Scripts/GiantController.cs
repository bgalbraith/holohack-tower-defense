using UnityEngine;
using System.Collections.Generic;

public class GiantController : MonoBehaviour {

    public bool DemoMode = true;

    public AudioClip IdleSound;
    public AudioClip HitSound;
    public AudioClip DeathSound;
    public AudioClip PunchSound;

    Animator animator;
    AudioSource audioSource;
    int hitPoints;
    float elapsed;
    string[] actions = new string[] {"hit1", "hit2", "death", "punchRight", "punchLeft", "slamRight", "slamLeft", "slamBig", "downGrab", "downLook"};

    // Use this for initialization
	void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        elapsed = 0f;
        hitPoints = 3;
	}
	
	// Update is called once per frame
	void Update()
    {
        if (DemoMode)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 5.0f)
            {
                elapsed = 0f;
                int i = Random.Range(0, actions.Length - 1);
                string action = actions[i];
                animator.SetTrigger(action);
                audioSource.loop = false;

                if (action == "hit1" || action == "hit2")
                {
                    audioSource.clip = HitSound;
                }
                else if (action == "death")
                {
                    audioSource.clip = DeathSound;
                }
                else if (action != "downLook" && action != "downGrab")
                {
                    audioSource.clip = PunchSound;
                }
                audioSource.Play();
            }
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = IdleSound;
            audioSource.loop = true;
            audioSource.Play();
        }
	}

    public void OnSelect()
    {
        if (hitPoints == 3)
        {
            int choice = Random.Range(0, 2);
            if (choice == 0)
            {
                animator.SetTrigger("hit1");
            }
            else if (choice == 1)
            {
                animator.SetTrigger("hit2");
            }
            else
            {
                animator.SetTrigger("death");
            }
        }
        else if (hitPoints == 2)
        {
            animator.SetTrigger("hit2");
        }
        else if (hitPoints == 1)
        {
            animator.SetTrigger("death");
        }

        if (hitPoints > 0)
        {
            if (!DemoMode)
            {
                hitPoints -= 1;
            }
            elapsed = 0f;           
            audioSource.loop = false;
            audioSource.clip = DeathSound;
            audioSource.Play();
        }
    }
}
