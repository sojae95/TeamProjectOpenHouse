using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static float moveSpeed = 5;
    public static float rotSpeed = 80;

    private Transform Playertr;

    // Start is called before the first frame update
    void Start()
    {
        Playertr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float rY = Input.GetAxis("Mouse X");
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        Playertr.Translate(moveDir.normalized * moveSpeed * Time.deltaTime, Space.Self);
        Playertr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * rY);
    }
}
