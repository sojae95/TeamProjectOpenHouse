using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Hostage : MonoBehaviour
{
    public GameObject target; //따라갈 타겟
    public GameObject Goal;
    NavMeshAgent agent;
    bool moveStart;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveStart = false;  
    }

    // Update is called once per frame
    void Update()
    {
        if (moveStart == true)
        {
            agent.destination = target.transform.position;
        }

       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goal")
        {
            moveStart = false;
            agent.destination = Goal.transform.position;
        }
        else if (other.tag == "Player")
        {
            moveStart = true;
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            moveStart = false;
        }
    }
}
