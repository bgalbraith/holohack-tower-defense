using UnityEngine;
using System.Collections;

public class HemoController : Minion
{

    public Animator anim;

    void Awake()
    {
        base.MinionType = MinionType.HEMO;
        anim = GetComponentInChildren<Animator>();
        //spawnPoint = GameObject.Find("SpawnPoint_H").transform;
        //endPoint = GameObject.Find("EndPoint_H").transform;

    }

    public override void StartAction()
    {
        anim.SetTrigger("runPlatelet");

    }

    public override IEnumerator IntermediateAction()
    {
        base.onForwardTraversal = false;
        GetComponent<MoveForward>().enabled = false;
        anim.SetBool("putPlatelet", true);

        yield return new WaitForSeconds(1.25f);



        EndAction();
    }

    public override void EndAction()
    {
        base.intermediateActionOver = true;
        GetComponent<MoveForward>().enabled = true;
        
        anim.SetTrigger("runBack");

    }
}
