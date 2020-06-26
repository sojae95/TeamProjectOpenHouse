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

    private Vector3 targetPosition; // PlayerPos
    private Animator ArmyAnimator;
    public float attackDist = 4.0f;

    private bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        ArmyAnimator = GetComponent<Animator>();


        StartCoroutine(this.CheckState());

    }

    // Update is called once per frame
    void Update()
    {
        if (bIsAiStart == true)
        {
            targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(targetPosition);

        }
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
                break;
            case CurrentState.dead:
                break;
            case CurrentState.attack:
                ArmyAnimator.SetBool("Shoot", true);
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
}