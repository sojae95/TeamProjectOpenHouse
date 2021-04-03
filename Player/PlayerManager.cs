using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{

    bool ishideMouse;

    public static float moveSpeed = 5.0f;
    public static float rotSpeed = 60;
    public static float Hp = 100;

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
        // Mouse Lock
        ishideMouse = false;

        state = PlayerState.idle;
        playerAnimator = GetComponent<Animator>();
        Playertr = GetComponent<Transform>();
        reloadTimmer = 2.5f;
        Hp = 100;
    }
    // Update is called once per frame
    void Update()
    {
        Mousehide();
        playerMove();
        Animation();
        if(Hp < 1)
        {
            SceneManager.LoadScene("StageScene");
        }
    }

    void playerMove() {
        float rZ = Input.GetAxis("Mouse Y");
        Playertr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * rZ);
    }

    void Mousehide() {
        if (Input.GetKeyDown(KeyCode.P)) {
            ishideMouse = !ishideMouse;
        }

        // Mouse Lock

        if (ishideMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
            // Cursor visible
            Cursor.visible = false;
        }
        else if (!ishideMouse) {
            Cursor.lockState = CursorLockMode.None;
            // Cursor visible
            Cursor.visible = true;
        }

    }

    void Animation()
    {
        //Debug.Log(state);

        if (state == PlayerState.idle) // 기본 상태일 경우
        {
            if (Input.GetKeyDown(KeyCode.R) && reloadTimmer >= 2.5f)// 장전 키를 눌렀고 리로드 시간이 2.5이상일 경우
            {

                playerAnimator.SetBool("isReLoad", true); // 리로드 상태 변수 true로 바꾸기
                state = PlayerState.reload;               // 상태 바꾸기

                Audio_Control_Script.ri_re_sound = true; //리로드 사운드 재생
              
            }
            else if (Input.GetMouseButton(1)) //마우스 오른쪽 버튼을 눌렀을 경우(조준)
            {
                playerAnimator.SetBool("isAimming", true); //조준 상태로 변경
                state = PlayerState.aimming;               //상태 바꾸기
            }
            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) // 이동 키 입력 들어 왔을 때
            {
                playerAnimator.SetBool("isWalk", true); // 이동 애니메이션으로 변경
                state = PlayerState.walk;               //상태 바꾸기
            }
            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0) // 왼쪽 마우스 버튼을 눌렀고(총알 발사) 총알의 숫자가 0 이상일 경우
            {
                playerAnimator.SetBool("isFire", true); // 발사 애니메이션으로 변경
                state = PlayerState.fire;               // 상태 바꾸기
            }
        }
        else if (state == PlayerState.walk) //걷는 상태였을 경우
        {
            if (Input.GetKeyDown(KeyCode.R)) // 장전 키를 눌렀을 경우 
            {
                playerAnimator.SetBool("isWalk", false); //걷는 상태 변수 false
                playerAnimator.SetBool("isFire", false); //발사 상태 변수 false
                playerAnimator.SetBool("isReLoad", true); //리로드 상태 변수 true
                state = PlayerState.reload;               //상태 바꾸기
            }
            else if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼을 눌렀을 경우 
            {
                playerAnimator.SetBool("isAimming", true); // 조준 애니메이션으로 변경
                state = PlayerState.aimming;               //상태 바꾸기
            }
            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0) // 왼쪽 마우스 버튼을 눌렀고(총알 발사) 총알의 숫자가 0 이상일 경우
            {
                playerAnimator.SetBool("isFire", true); // 발사 애니메이션으로 변경
                state = PlayerState.fire;               // 상태 바꾸기

            }
            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift)) //현재 이동키가 눌린 상황에서 왼쪽 Shift가 눌렸을 경우
            {
                playerAnimator.SetBool("isRun", true); // 뛰는 상태 변수 true
                
                AutomaticGunScript.fire_check = false; 

                state = PlayerState.run;               // 상태 바꾸기
            }
            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) // 어떠한 이동키도 눌리지 않았을 경우
            {
                playerAnimator.SetBool("isWalk", false); // 걷기 변수 false
                state = PlayerState.idle;                // 상태 바꾸기 - 기본
            }
            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0) // 왼쪽 마우스 버튼을 눌렀고(총알 발사) 총알의 숫자가 0 이상일 경우
            {
                playerAnimator.SetBool("isFire", true); // 발사 애니메이션으로 변경
                state = PlayerState.fire;               // 상태 바꾸기
            }

        }
        else if (state == PlayerState.run) // 뛰는 상태일 경우
        {

            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) //shift가 눌리지 않았을 경우
            {
                playerAnimator.SetBool("isRun", false); // 뛰는 상태 변수 false
                playerAnimator.SetBool("isWalk", true); // 걷는 상태 변수 true
                state = PlayerState.walk;               // 상태 바꾸기
            }
            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) // 이동키가 눌리지않았을 경우
            {
                playerAnimator.SetBool("isWalk", false); // 걷기 상태 변수 false
                playerAnimator.SetBool("isRun", false);  // 뛰기 상태 변수 false
                AutomaticGunScript.fire_check = true;   
                state = PlayerState.idle;                // 상태 바꾸기
            }

        }
        else if (state == PlayerState.reload) // 리로드 상태일 경우
        {

            AutomaticGunScript.fire_check = false;

            reloadTimmer -= Time.deltaTime; // 리로드 Timer 

            Audio_Control_Script.ri_re_sound = false;//딜레이 시간동안 사운드 재생x

            if (reloadTimmer < 0) // 리로드 타이머 <0 경우
            { // 리로드가 끝났을 때(애니메이션 길이가 3.0임)
               //Debug.Log("End!");

                reloadTimmer = 2.5f; // 리로드 애니메이션 Timer 초기화 

                playerAnimator.SetBool("isReLoad", false); //리로드 상태 변수 false

                state = PlayerState.idle;                  // 상태 바꾸기 (idle로 초기화)

                AutomaticGunScript.reload_check = false;
                AutomaticGunScript.fire_check = true;
                AutomaticGunScript.bullet_count = 50;
                
            }

        }
        else if (state == PlayerState.fire) // 발사 상태일 경우(총알 발사)
        {
            if (Input.GetKeyDown(KeyCode.R)) // R키를 눌렀을 경우(리로드)
            {
                playerAnimator.SetBool("isWalk", false); //걷기 상태 변수 false
                playerAnimator.SetBool("isFire", false); // 발사 상태 변수 false
                playerAnimator.SetBool("isReLoad", true); // 리로드 변수 true
                state = PlayerState.reload;               //상태 바꾸기
            }
            else if (!Input.GetMouseButton(0)) // 발사 버튼을 누르지 않을 경우
            {
                playerAnimator.SetBool("isFire", false); //발사 상태 변수 false
                playerAnimator.SetBool("isWalk", false); //걷기 상태 변수 false

                AutomaticGunScript.fire_check = true;
                state = PlayerState.idle;                // 상태 바꾸기(idle로 초기화)
            }
        }
        else if (state == PlayerState.aimming) // 조준 상태일 경우
        {
            if (!Input.GetMouseButton(1)) // 조준 버튼을 누르지 않았을 경우
            {
                playerAnimator.SetBool("isAimming", false); // 조준 상태 변수 false
                state = PlayerState.aimmingout;             // 상태 바꾸기(조준 안한 상태)

            }

            // 정지 애니메이션으로 바꿔줌
            playerAnimator.SetBool("isAimmingPose", true); // 정조준 상태 변수 true
            state = PlayerState.aimmingpose;               // 상태 바꾸기
        }
        else if (state == PlayerState.aimmingout)          //에이밍을 푸는 상태일 경우
        {
            state = PlayerState.idle;  // 상태 바꾸기(idle 로 초기화)
        }
        else if (state == PlayerState.aimmingpose)  // 정조준 상태일 경우
        {
            if (Input.GetKeyDown(KeyCode.R)) // R키를 눌렀을 경우 
            {
                playerAnimator.SetBool("isWalk", false);    //걷기 상태 변수 false
                playerAnimator.SetBool("isAimming", false); //조준 상태 변수 false 
                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false 
                playerAnimator.SetBool("isReLoad", true); //리로드 상태 변수 true
                state = PlayerState.reload; // 상태 바꾸기

                AutomaticGunScript.bullet_count = 50;

            }
            else if (!Input.GetMouseButton(1)) // 마우스 오른쪽 버튼을 누르지 않았을 경우
            {
                playerAnimator.SetBool("isAimming", false); // 조준 상태 변수 false
                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false
                state = PlayerState.aimmingout; // 상태 바꾸기(조준 하지 않은 상태)

            }
            else if (Input.GetMouseButton(1) && Input.GetMouseButton(0)) // 마우스 오른쪽 버튼을 누른 상태에서 왼쪽 버튼을 눌렀을 경우
            {
                playerAnimator.SetBool("isAimmingFire", true); // 정조준 발사 상태 변수 true
                state = PlayerState.aimfire; //상태 바꾸기
            }
        }
        else if (state == PlayerState.aimfire) // 정조준 발사 상태일 경우 
        {
            if (Input.GetKeyDown(KeyCode.R)) //R키를 눌렀을 때 
            {

                playerAnimator.SetBool("isWalk", false); //걷기 상태 변수 false
                playerAnimator.SetBool("isAimming", false); //조준 상태 변수 false 
                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false 
                playerAnimator.SetBool("isAimmingFire", false); // 정조준 발사 상태 변수 false

                playerAnimator.SetBool("isReLoad", true); // 리로드 상태 변수 true
                state = PlayerState.reload; // 상태 바꾸기 
            }
            else if (!Input.GetMouseButton(1)) // 오른쪽 마우스 버튼을 누르지 않았을 경우 
            {
                playerAnimator.SetBool("isAimming", false); //조준 상태 변수 false
                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false
                playerAnimator.SetBool("isAimmingFire", false); // 정조준 발사 상태 변수 false
                state = state = PlayerState.aimmingout; // 상태 바꾸기 (조준 하지 않은 상태)
            }
            else if (!Input.GetMouseButton(0)) // 마우스 왼쪽 버튼을 누르지 않았을 경우
            { 
                playerAnimator.SetBool("isAimmingPose", true); // 정조준 상태 변수 true
                playerAnimator.SetBool("isAimmingFire", false); // 정조준 발사 변수 false
                state = state = PlayerState.aimmingpose; // 상태 바꾸기 
            }
        }

    }

    //void Animation_()
    //{

    //    switch (state)
    //    {
    //        case PlayerState.idle:
    //            if (Input.GetKeyDown(KeyCode.R) && reloadTimmer >= 2.5f)// 장전 키를 눌렀고 리로드 시간이 2.5이상일 경우
    //            {

    //                playerAnimator.SetBool("isReLoad", true); // 리로드 상태 변수 true로 바꾸기
    //                state = PlayerState.reload;               // 상태 바꾸기

    //                Audio_Control_Script.ri_re_sound = true; //리로드 사운드 재생

    //            }
    //            else if (Input.GetMouseButton(1)) //마우스 오른쪽 버튼을 눌렀을 경우(조준)
    //            {
    //                playerAnimator.SetBool("isAimming", true); //조준 상태로 변경
    //                state = PlayerState.aimming;               //상태 바꾸기
    //            }
    //            else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) // 이동 키 입력 들어 왔을 때
    //            {
    //                playerAnimator.SetBool("isWalk", true); // 이동 애니메이션으로 변경
    //                state = PlayerState.walk;               //상태 바꾸기
    //            }
    //            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0) // 왼쪽 마우스 버튼을 눌렀고(총알 발사) 총알의 숫자가 0 이상일 경우
    //            {
    //                playerAnimator.SetBool("isFire", true); // 발사 애니메이션으로 변경
    //                state = PlayerState.fire;               // 상태 바꾸기
    //            }
    //            break;

    //        case PlayerState.walk:
    //            if (Input.GetKeyDown(KeyCode.R)) // 장전 키를 눌렀을 경우 
    //            {
    //                playerAnimator.SetBool("isWalk", false); //걷는 상태 변수 false
    //                playerAnimator.SetBool("isReLoad", true); //리로드 상태 변수 true
    //                state = PlayerState.reload;               //상태 바꾸기
    //            }
    //            else if (Input.GetMouseButton(1)) // 마우스 오른쪽 버튼을 눌렀을 경우 
    //            {
    //                playerAnimator.SetBool("isAimming", true); // 조준 애니메이션으로 변경
    //                state = PlayerState.aimming;               //상태 바꾸기
    //            }
    //            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0) // 왼쪽 마우스 버튼을 눌렀고(총알 발사) 총알의 숫자가 0 이상일 경우
    //            {
    //                playerAnimator.SetBool("isFire", true); // 발사 애니메이션으로 변경
    //                state = PlayerState.fire;               // 상태 바꾸기

    //            }
    //            else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W)) && Input.GetKey(KeyCode.LeftShift)) //현재 이동키가 눌린 상황에서 왼쪽 Shift가 눌렸을 경우
    //            {
    //                playerAnimator.SetBool("isRun", true); // 뛰는 상태 변수 true

    //                AutomaticGunScript.fire_check = false;

    //                state = PlayerState.run;               // 상태 바꾸기
    //            }
    //            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) // 어떠한 이동키도 눌리지 않았을 경우
    //            {
    //                playerAnimator.SetBool("isWalk", false); // 걷기 변수 false
    //                state = PlayerState.idle;                // 상태 바꾸기 - 기본
    //            }
    //            else if (Input.GetMouseButton(0) && AutomaticGunScript.bullet_count > 0) // 왼쪽 마우스 버튼을 눌렀고(총알 발사) 총알의 숫자가 0 이상일 경우
    //            {
    //                playerAnimator.SetBool("isFire", true); // 발사 애니메이션으로 변경
    //                state = PlayerState.fire;               // 상태 바꾸기
    //            }
    //            break;
    //        case PlayerState.run:
    //            if (!Input.GetKey(KeyCode.LeftShift) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) //shift가 눌리지 않았을 경우
    //            {
    //                playerAnimator.SetBool("isRun", false); // 뛰는 상태 변수 false
    //                playerAnimator.SetBool("isWalk", true); // 걷는 상태 변수 true
    //                state = PlayerState.walk;               // 상태 바꾸기
    //            }
    //            else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))) // 이동키가 눌리지않았을 경우
    //            {
    //                playerAnimator.SetBool("isWalk", false); // 걷기 상태 변수 false
    //                playerAnimator.SetBool("isRun", false);  // 뛰기 상태 변수 false
    //                AutomaticGunScript.fire_check = true;
    //                state = PlayerState.idle;                // 상태 바꾸기
    //            }
    //            break;
    //        case PlayerState.reload:
    //            AutomaticGunScript.fire_check = false;
    //            reloadTimmer -= Time.deltaTime; // 리로드 Timer 
    //            Audio_Control_Script.ri_re_sound = false;//딜레이 시간동안 사운드 재생x

    //            if (reloadTimmer < 0) // 리로드 타이머 <0 경우
    //            { // 리로드가 끝났을 때(애니메이션 길이가 3.0임)

    //                reloadTimmer = 2.5f; // 리로드 애니메이션 Timer 초기화 

    //                playerAnimator.SetBool("isReLoad", false); //리로드 상태 변수 false

    //                state = PlayerState.idle;                  // 상태 바꾸기 (idle로 초기화)

                   
    //                AutomaticGunScript.reload_check = false;
    //                AutomaticGunScript.fire_check = true;
    //                AutomaticGunScript.bullet_count = 50;
    //            }
    //            break;
    //        case PlayerState.fire:
    //            if (Input.GetKeyDown(KeyCode.R)) // R키를 눌렀을 경우(리로드)
    //            {
    //                playerAnimator.SetBool("isWalk", false); //걷기 상태 변수 false ==> walk에서 왔을 수 있기 때문에 초기화 시켜줌
    //                playerAnimator.SetBool("isFire", false); // 발사 상태 변수 false
    //                playerAnimator.SetBool("isReLoad", true); // 리로드 변수 true
    //                state = PlayerState.reload;               //상태 바꾸기
    //            }
    //            else if (!Input.GetMouseButton(0)) // 발사 버튼을 누르지 않을 경우
    //            {
    //                playerAnimator.SetBool("isFire", false); //발사 상태 변수 false
    //                playerAnimator.SetBool("isWalk", false); //걷기 상태 변수 false

    //                AutomaticGunScript.fire_check = true;
    //                state = PlayerState.idle;                // 상태 바꾸기(idle로 초기화)
    //            }
    //            break;
    //        case PlayerState.aimming:
    //            if (!Input.GetMouseButton(1)) // 조준 버튼을 누르지 않았을 경우
    //            {
    //                playerAnimator.SetBool("isAimming", false); // 조준 상태 변수 false
    //                state = PlayerState.aimmingout;             // 상태 바꾸기(조준 안한 상태) aimmingout에 대한 변수 없이 isAimming으로 처리됨
    //            }

    //            // 정지 애니메이션으로 바꿔줌
    //            playerAnimator.SetBool("isAimmingPose", true); // 정조준 상태 변수 true
    //            state = PlayerState.aimmingpose;               // 상태 바꾸기
    //            break;
    //        case PlayerState.aimmingout:
    //            playerAnimator.SetBool("isWalk", false);    //걷기 상태 변수 false

    //            state = PlayerState.idle;  // 상태 바꾸기(idle 로 초기화)
    //            break;
    //        case PlayerState.aimmingpose:
    //            if (Input.GetKeyDown(KeyCode.R)) // R키를 눌렀을 경우 
    //            {
    //                playerAnimator.SetBool("isWalk", false);    //걷기 상태 변수 false
    //                playerAnimator.SetBool("isAimming", false); //조준 상태 변수 false 
    //                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false 
    //                playerAnimator.SetBool("isReLoad", true); //리로드 상태 변수 true
    //                state = PlayerState.reload; // 상태 바꾸기

    //                AutomaticGunScript.bullet_count = 50;

    //            }
    //            else if (!Input.GetMouseButton(1)) // 마우스 오른쪽 버튼을 누르지 않았을 경우
    //            {
    //                playerAnimator.SetBool("isAimming", false); // 조준 상태 변수 false
    //                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false
    //                state = PlayerState.aimmingout; // 상태 바꾸기(조준 하지 않은 상태)
    //            }
    //            else if (Input.GetMouseButton(1) && Input.GetMouseButton(0)) // 마우스 오른쪽 버튼을 누른 상태에서 왼쪽 버튼을 눌렀을 경우
    //            {
    //                playerAnimator.SetBool("isAimmingFire", true); // 정조준 발사 상태 변수 true
    //                state = PlayerState.aimfire; //상태 바꾸기
    //            }
    //            break;
    //        case PlayerState.aimfire:
    //            if (Input.GetKeyDown(KeyCode.R)) //R키를 눌렀을 때 
    //            {

    //                playerAnimator.SetBool("isWalk", false); //걷기 상태 변수 false
    //                playerAnimator.SetBool("isAimming", false); //조준 상태 변수 false 
    //                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false 
    //                playerAnimator.SetBool("isAimmingFire", false); // 정조준 발사 상태 변수 false

    //                playerAnimator.SetBool("isReLoad", true); // 리로드 상태 변수 true
    //                state = PlayerState.reload; // 상태 바꾸기 
    //            }
    //            else if (!Input.GetMouseButton(1)) // 오른쪽 마우스 버튼을 누르지 않았을 경우 
    //            {
    //                playerAnimator.SetBool("isAimming", false); //조준 상태 변수 false
    //                playerAnimator.SetBool("isAimmingPose", false); // 정조준 상태 변수 false
    //                playerAnimator.SetBool("isAimmingFire", false); // 정조준 발사 상태 변수 false
    //                playerAnimator.SetBool("isFire", true); //정조준만 푼 상태이기 때문에 바로 idle에서 fire로 넘어가게 만들기 위함

    //                state = state = PlayerState.aimmingout; // 상태 바꾸기 (조준 하지 않은 상태)
    //            }
    //            else if (!Input.GetMouseButton(0)) // 마우스 왼쪽 버튼을 누르지 않았을 경우
    //            {
    //                playerAnimator.SetBool("isAimmingPose", true); // 정조준 상태 변수 true
    //                playerAnimator.SetBool("isAimmingFire", false); // 정조준 발사 변수 false
    //                state = state = PlayerState.aimmingpose; // 상태 바꾸기 
    //            }
    //            break;
    //    }
    //}

}
