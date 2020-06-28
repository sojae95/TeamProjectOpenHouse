using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static float moveSpeed = 5;
    private Vector3 moveDir;
    public static float rotSpeed = 80;
    private Transform Playertr;

    private CharacterController characterController;

    public float gravity = 20.0F;

    // Start is called before the first frame update
    void Start()
    {
        Playertr = GetComponent<Transform>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float rY = Input.GetAxis("Mouse X");
        
        moveDir = (Vector3.forward * v) + (Vector3.right * h);
        moveDir.y -= gravity * Time.deltaTime;

        //로컬 좌표의 forward 방향으로 움직이게 적용시켜주는 코드
        moveDir = Playertr.rotation * moveDir;

        characterController.Move(moveDir * moveSpeed * Time.deltaTime);
        Playertr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * rY);

    }

}
