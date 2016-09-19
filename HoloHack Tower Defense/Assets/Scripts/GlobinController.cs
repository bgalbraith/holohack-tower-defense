using UnityEngine;
using System.Collections;

public class GlobinController : Minion {

    public Animator anim;
    public Transform missle;

    public float missleVelocity = 5.0f;
    public float missleTime = 0.22f;

    void Awake()
    {
        base.MinionType = MinionType.GLOBIN;
        anim = GetComponentInChildren<Animator>();
        spawnPoint = GameObject.Find("SpawnPoint_G").transform;
        endPoint = GameObject.Find("EndPoint_G").transform;

    }

    public override void StartAction()
    {
        anim.SetTrigger("walk");

    }

    public override IEnumerator IntermediateAction()
    {
        GetComponent<MoveForward>().enabled = false;
        anim.SetBool("bazookahFire", true);

        yield return new WaitForSeconds(missleTime);

        missle.gameObject.SetActive(true);
        missle.GetComponent<Rigidbody>().velocity = missle.transform.forward * missleVelocity;
        missle.transform.parent = null;

        yield return new WaitForSeconds(4.5f - missleTime);

        base.onForwardTraversal = !base.onForwardTraversal;

        EndAction();
    }

    public override void EndAction()
    {
        GetComponent<MoveForward>().enabled = true;
        anim.SetTrigger("walk");

    }
}
