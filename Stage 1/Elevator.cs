using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool moveStart = false;
    Vector3 MoveDirection = new Vector3(0, 0, 0
        );
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(moveStart == true)
        {
         
            MoveDirection = new Vector3(0, 0.1f, 0);

        }

        this.transform.Translate(MoveDirection);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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

