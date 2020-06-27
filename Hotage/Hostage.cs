using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Hostage : MonoBehaviour
{
    public GameObject target; //따라갈 타겟
    public GameObject Goal;
    NavMeshAgent agent;
    public static bool moveStart;
    Animator animator;
    Vector3 lookDirection;
    [SerializeField] private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        moveStart = false;
        animator = GetComponentInChildren<Animator>();
        slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveStart == true)
        {
            agent.destination = target.transform.position - Vector3.back;
        }
        if (Vector3.Distance(this.transform.position, target.transform.position) < 1)
        {
            lookDirection = target.transform.position;
            lookDirection.y = 0.0f;
            transform.LookAt(lookDirection);
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);

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
            if (slider.value != 1)
            {
                slider.gameObject.SetActive(true);
            }
            else
            {
                moveStart = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                slider.value += Time.deltaTime;
                if (slider.value == 1)
                {
                    slider.gameObject.SetActive(false);
                    moveStart = true;
                    animator.SetBool("start", true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            slider.gameObject.SetActive(false);
            moveStart = false;
        }
    }


}
