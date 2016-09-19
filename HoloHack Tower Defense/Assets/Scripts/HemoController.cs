using UnityEngine;
using System.Collections;

public class HemoController : Minion {

    public Animator anim;
    public Transform platelet;

    void Awake()
    {
        base.MinionType = MinionType.HEMO;
        anim = GetComponentInChildren<Animator>();
        spawnPoint = GameObject.Find("SpawnPoint_H").transform;
        endPoint = GameObject.Find("EndPoint_HNew").transform;

    }

    public override void StartAction()
    {
        anim.SetTrigger("runPlatelet");

    }

    public override IEnumerator IntermediateAction()
    {
        GetComponent<MoveForward>().enabled = false;
        anim.SetBool("putPlatelet", true);

        yield return new WaitForSeconds(0.33f);

        platelet.parent = null;
        endPoint.GetComponentInChildren<PlateletPlacer>().PlaceInNextPlateletLocation(platelet);         

        yield return new WaitForSeconds(0.92f);

        base.onForwardTraversal = !base.onForwardTraversal;

        EndAction();
    }

    public override void EndAction()
    {
        GetComponent<MoveForward>().enabled = true;
        anim.SetTrigger("runBack");

    }
}
