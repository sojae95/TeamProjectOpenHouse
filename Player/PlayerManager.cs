using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //=다음주 목요일(25일)까지 만들 것=
    //플레이어 달리기, 리로딩, 총쏘기 스테이터스 및 애니메이션 추가
    //마우스 상하로 움직이는거 최대한 완성시켜 봅니다.(모델링이 이상해서 지금은 넣어놓지 않았습니다. )

    public static float moveSpeed = 5;
    public static float rotSpeed = 3;

    private Animator playerAnimator;

    public enum PlayerState
    {
        idle,
        walk,
        run,
        reload
        // 앉기, 뛰기(보류)
    }

    PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.idle;
        playerAnimator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        checkAnimation();
        playerMove();
    }

    void playerMove() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);

        transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0));

    }

    void shootGun() { 
    
    }

    void checkAnimation()
    {
        Debug.Log(state);


        if (state == PlayerState.idle)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
            {
                playerAnimator.SetBool("isWalk", true);
                state = PlayerState.walk;
            }
        }
        else if (state == PlayerState.walk){
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
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

        } 
        else if (state == PlayerState.run){
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isReLoad", true);
                playerAnimator.SetBool("isRun", false);
                state = PlayerState.reload;
            }
            else if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
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
        else if (state == PlayerState.reload){
            //playerAnimator.SetBool("isReLoad", false);
            //state = PlayerState.idle;

            Debug.Log("리로드 진행중");
            if (!playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReloadOutOfAmmo"))
            {
                playerAnimator.SetBool("isReLoad", false);
                state = PlayerState.idle;
                Debug.Log("리로드  끝");
                   
            }

        }


    }

}
