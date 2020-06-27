using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //=다음주 목요일(25일)까지 만들 것=
    //플레이어 달리기, 리로딩, 총쏘기 스테이터스 및 애니메이션 추가
    //마우스 상하로 움직이는거 최대한 완성시켜 봅니다.(모델링이 이상해서 지금은 넣어놓지 않았습니다. )



    public static float moveSpeed = 5.0f;
    public static float rotSpeed = 60;

    private Animator playerAnimator;
    private Transform Playertr;

    float reloadTimmer;
    public enum PlayerState
    {
        idle,
        walk,
        run,
        reload,
        fire,
        aimming,
        aimmingout,
        aimmingpose,
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
        reloadTimmer = 2.9f;
    }
    // Update is called once per frame
    void Update()
    {

        playerMove();
        Animation();
    }

    void playerMove() {
        float rZ = Input.GetAxis("Mouse Y");
        Playertr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * rZ);
    }


    void Animation()
    {
        //Debug.Log(state);

        if (state == PlayerState.idle)
        {
            if (Input.GetKeyDown(KeyCode.R) && reloadTimmer >= 2.9f)//
            {

                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
                 //AutomaticGunScript.reload_check = true;
                //AutomaticGunScript.bullet_count = 50;

                //if(playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("isReLoad") ==true)
                //{
                    Audio_Control_Script.ri_re_sound = true; //리로드 사운드 재생
                //}

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
            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0)  // playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("isReLoad") == false)
            {
                playerAnimator.SetBool("isFire", true);
                state = PlayerState.fire;   
            }
        }
        else if (state == PlayerState.walk)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isFire", false);
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;

                //AutomaticGunScript.reload_check = true;
                //AutomaticGunScript.bullet_count = 50;

            }
            else if (Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", true);
                state = PlayerState.aimming;
            }
            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0)
            {
                playerAnimator.SetBool("isFire", true);
                state = PlayerState.fire;
         
                // AutomaticGunScript.ReLoad_state_check();
            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift))
            {
                //playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isRun", true);


                AutomaticGunScript.fire_check = false;

                state = PlayerState.run;
            }
            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)))
            {
                playerAnimator.SetBool("isWalk", false);
                state = PlayerState.idle;
            }
            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0)
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
                AutomaticGunScript.fire_check = true;
                state = PlayerState.idle;
            }

        }
        else if (state == PlayerState.reload)
        {

            //playerAnimator.SetBool("isReLoad", false);
            AutomaticGunScript.fire_check = false;

            reloadTimmer -= Time.deltaTime;
            Audio_Control_Script.ri_re_sound = false;//딜레이 시간동안 사운드 재생x
            //Debug.Log("reloading");
            //AutomaticGunScript.fire_check = false;


            if (reloadTimmer < 0)
            { // 리로드가 끝났을 때(애니메이션 길이가 3.0임)
              //Debug.Log("End!");

                reloadTimmer = 2.9f;

                playerAnimator.SetBool("isReLoad", false);

                state = PlayerState.idle;


                AutomaticGunScript.reload_check = false;
                AutomaticGunScript.fire_check = true;
                AutomaticGunScript.bullet_count = 50;
                
            }

            //if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Reload") || playerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime<0.8f)
            //{
            //    Debug.Log("reloading");
            //}
            //else{
            //    Debug.Log("End!");
            //    playerAnimator.SetBool("isReLoad", false);
            //    state = PlayerState.idle;
            //}

            //AutomaticGunScript.reload_check = false;

            //state = PlayerState.idle;
        }
        else if (state == PlayerState.fire)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isFire", false);
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
            }
            else if (!Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isFire", false);
                playerAnimator.SetBool("isWalk", false);

                AutomaticGunScript.fire_check = true;
                state = PlayerState.idle;

            }
        }
        else if (state == PlayerState.aimming)
        {
            if (!Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", false);
                state = PlayerState.aimmingout;

            }

            // 정지 애니메이션으로 바꿔줌
            playerAnimator.SetBool("isAimmingPose", true);
            state = PlayerState.aimmingpose;
        }
        else if (state == PlayerState.aimmingout)
        {
            state = PlayerState.idle;
        }
        else if (state == PlayerState.aimmingpose)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isAimming", false);
                playerAnimator.SetBool("isAimmingPose", false);
                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;

                AutomaticGunScript.bullet_count = 50;

            }
            else if (!Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", false);
                playerAnimator.SetBool("isAimmingPose", false);
                state = PlayerState.aimmingout;

            }
            else if (Input.GetMouseButton(1) && Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isAimmingFire", true);
                state = PlayerState.aimfire;
            }
        }
        else if (state == PlayerState.aimfire)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {

                playerAnimator.SetBool("isWalk", false);
                playerAnimator.SetBool("isAimming", false);
                playerAnimator.SetBool("isAimmingPose", false);
                playerAnimator.SetBool("isAimmingFire", false);

                playerAnimator.SetBool("isReLoad", true);
                state = PlayerState.reload;
            }
            else if (!Input.GetMouseButton(1))
            {
                playerAnimator.SetBool("isAimming", false);
                playerAnimator.SetBool("isAimmingPose", false);
                playerAnimator.SetBool("isAimmingFire", false);
                state = state = PlayerState.aimmingout;
            }
            else if (!Input.GetMouseButton(0))
            {
                playerAnimator.SetBool("isAimmingPose", true);
                playerAnimator.SetBool("isAimmingFire", false);
                state = state = PlayerState.aimmingpose;
            }
        }

    }

}
