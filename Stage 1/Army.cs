using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Army : MonoBehaviour
{
    private bool bIsAiStart = false;
    public Transform target;
    public enum CurrentState { idle, attack, dead };
    public CurrentState curState = CurrentState.idle;

    private ArmyFire armyfire;
    private Vector3 targetPosition; // PlayerPos
    private Animator ArmyAnimator;
    public float attackDist = 4.0f;


    private readonly float damping = 10.0f;


    private bool isDead = false;


    static public float enemy_hp = 90.0f;





    // Start is called before the first frame update
    void Start()
    {
        armyfire = GetComponent<ArmyFire>();
        ArmyAnimator = GetComponent<Animator>();


        StartCoroutine(this.CheckState());

    }

    // Update is called once per frame
    void Update()
    {
        if (bIsAiStart == true)
        {

            Quaternion rot = Quaternion.LookRotation(target.transform.position - this.transform.position);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * damping);
        }

        //if (enemy_hp <= 0.0f)
        //{
        //    Destroy(this.gameObject);
        //}




    }

    IEnumerator CheckState()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.1f);

            float dist = Vector3.Distance(target.transform.position, this.transform.position);

            if (bIsAiStart)
            {
                curState = CurrentState.attack;
            }
            else
            {
                curState = CurrentState.idle;
            }

            StartCoroutine(this.CheckStateForAction());
        }

    }

    IEnumerator CheckStateForAction()
    {
        switch (curState)
        {
            case CurrentState.idle:
                ArmyAnimator.SetBool("Shoot", false);
                armyfire.isFire = false;
                break;
            case CurrentState.dead:
                armyfire.isFire = false;
                break;
            case CurrentState.attack:
                ArmyAnimator.SetBool("Shoot", true);
                if (armyfire.isFire == false) armyfire.isFire = true;
                break;

        }
        yield return null;
    }


    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bIsAiStart = true;
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bIsAiStart = false;
        }

    }

    void Dead_enemy()
    {
        Destroy(this.gameObject);

    }

}