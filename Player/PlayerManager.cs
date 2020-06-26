using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //=다음주 목요일(25일)까지 만들 것=
    //플레이어 달리기, 리로딩, 총쏘기 스테이터스 및 애니메이션 추가
    //마우스 상하로 움직이는거 최대한 완성시켜 봅니다.(모델링이 이상해서 지금은 넣어놓지 않았습니다. )



    public static float moveSpeed = 5;
    public static float rotSpeed = 60;

    private Animator playerAnimator;
    private Transform Playertr;
    public enum PlayerState
    {
        idle,
        walk,
        run,
        reload,
        fire,
        aimming,
        aimfire
        // 앉기, 뛰기(보류)
    }

    PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.idle;
        playerAnimator = GetComponent<Animator>();
        Playertr = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        Animation();
        playerMove();
    }

    void playerMove() {
        float rZ = Input.GetAxis("Mouse Y");
        Playertr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * rZ);
    }

    void shootGun() { 
    
    }

    void Animation()
    {
        Debug.Log(state);


        if (state == PlayerState.idle)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
            }
            else if (Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", true);
                state = PlayerState.aimming;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                playerAnimator.SetBool("isWalk", true);
                state = PlayerState.walk;
            }
            else if (Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isFire", true);
                state = PlayerState.fire;
            }
        }
        else if (state == PlayerState.walk)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
            }
            else if (Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", true);
                state = PlayerState.aimming;
            }
            else if (Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isFire", true);
                state = PlayerState.fire;
            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
            {
                //playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isRun", true);
                state = PlayerState.run;
            }
            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
            {
                playerAnimator.SetBool("isWalk", false);
                state = PlayerState.idle;
            }
            else if (Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isFire", true);
                state = PlayerState.fire;
            }

        }
        else if (state == PlayerState.run)
        {

            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
            {
                playerAnimator.SetBool("isRun", false);
                playerAnimator.SetBool("isWalk", true);
                state = PlayerState.walk;
            }
            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
            {
                playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isRun", false);
                state = PlayerState.idle;
            }

        }
        else if (state == PlayerState.reload)
        {
            playerAnimator.SetBool("isReLoad", false);
            state = PlayerState.idle;
        }
        else if (state == PlayerState.fire)
        {
            if (!Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isFire", false);
                playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isRun", false);
                state = PlayerState.idle;

            }
        }
        else if (state == PlayerState.aimming) {
            if (!Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", false);
                Debug.Log("에이밍 풀림");
                state = PlayerState.idle;
            }
        }


    }

}
