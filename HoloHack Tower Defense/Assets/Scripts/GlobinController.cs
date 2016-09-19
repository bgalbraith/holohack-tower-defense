using UnityEngine;
using System.Collections;

public class GlobinController : Minion
{

    public Animator anim;

    void Awake()
    {
        base.MinionType = MinionType.GLOBIN;
        anim = GetComponentInChildren<Animator>();
        //spawnPoint = GameObject.Find("SpawnPoint_G").transform;
        //endPoint = GameObject.Find("EndPoint_G").transform;

    }

    public override void StartAction()
    {
        anim.SetTrigger("walk");

    }

    public override IEnumerator IntermediateAction()
    {
        base.onForwardTraversal = false;

        GetComponent<MoveForward>().isActive = false;
        anim.SetBool("bazookahFire", true);

        yield return new WaitForSeconds(4.5f);



        EndAction();
    }

    public override void EndAction()
    {
        base.intermediateActionOver = true;
        GetComponent<MoveForward>().isActive = true;
        anim.SetTrigger("walk");

    }
}
