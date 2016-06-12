using UnityEngine;
using UnityEngine.UI;

public class GiantController : MonoBehaviour {

    public bool DemoMode = true;

    public AudioClip IdleSound;
    public AudioClip HitSound;
    public AudioClip DeathSound;
    public AudioClip PunchSound;

    Animator animator;
    AudioSource audioSource;
    Text heathStatus;
    int hitPoints;
    float elapsed;
    string[] actions = new string[] {"hit1", "hit2", "death", "punchRight", "punchLeft", "slamRight", "slamLeft", "slamBig", "downGrab", "downLook"};

    // Use this for initialization
	void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();
        elapsed = 0f;
        hitPoints = 4;
        heathStatus = gameObject.GetComponentInChildren<Text>();
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
                int i = Random.Range(0, actions.Length);
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

        if (hitPoints == 4)
        {
            heathStatus.text = "80%";
        }
        else if (hitPoints == 3)
        {
            heathStatus.text = "40%";
            heathStatus.color = Color.yellow;
        }
        else if (hitPoints == 2)
        {
            heathStatus.text = "20%";
            heathStatus.color = Color.red;
        }
        else
        {
            heathStatus.text = "0%";
        }

        if (hitPoints > 1)
        {
            int choice = Random.Range(0, 2);
            if (choice == 0)
            {
                animator.SetTrigger("hit1");
            }
            else
            {
                animator.SetTrigger("hit2");
            }
        }
        else
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
