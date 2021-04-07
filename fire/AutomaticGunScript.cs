using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticGunScript : MonoBehaviour
{

    

    public GameObject Bullet; //총알 생성위치
    public Transform firePos; //총알 발사 위치

    ArrayList objArray = new ArrayList();

    public Camera targetingCam; //목표위치 정중앙에서 발사되는 카메라
    public Queue<Transform> Targets; //Marked objects(enemies)

    private float attackCoolTime;
    private float timer;

    private Vector3 preBulletPosition;

    static public bool reload_check;
    static public bool fire_check;


    private int bullet_MAX = 10;
    static public int bullet_count;  // 현재 총알 개수
    List<GameObject> Bullets = null; // 총알을 오브젝트 풀링할 list 
    public GameObject bullet;        // 총알 오브젝트 받아두는 변수  
    //


    float Max_Distance = 20.0f;

    void Start()
    {
        attackCoolTime = 0.2f; //단발 쿨타임
        timer = 0.5f; //다음 단발 시간 체크
        bullet_count = 50; // 탄창 50발로 고정
        reload_check = false;
        fire_check = true;
        // Attack();

        ////불릿 list 할당
        //Bullets = new List<GameObject>(); 

        //for (int i = 0; i < bullet_MAX; i++) { 
        //    GameObject bullet_Obj = Instantiate(bullet);
        //    Bullets.Add(bullet_Obj);
        //    Bullets[i].SetActive(false);
        //}

        Create_Bullet();

    }

    void Update()
    {

        if (bullet_count <= 0)
            fire_check = false;

        if (bullet_count <= 49 && bullet_count >= 0)
            //Debug.Log(bullet_count);

        timer += Time.deltaTime; //단발 시간 체크
        if (Input.GetMouseButtonDown(0) && bullet_count >= 0 
            && reload_check==false
            && fire_check == true
            &&  timer>= attackCoolTime) //마우스 왼쪽 버튼 && 단발 시간 되었을 때
        {
            timer = 0; //단발 시간 초기화

            //총알 생성
            //GameObject obj = Instantiate(Bullet, firePos.transform.position, firePos.transform.rotation);

            Fire_Bullet();



            if (bullet_count >= 0)
            {
                bullet_count--;
                Audio_Control_Script.fi_sound = true;
            }

           // Destroy(obj, 0.5f);
            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * Max_Distance, Color.blue, 0.3f); //끝에 0.8f는 보여질 시간

            //레이캐스트 충돌 처리 하려다 일단 보류


            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                if (hit.collider.gameObject.CompareTag("Target"))
                {

                    Destroy(hit.collider.gameObject);
                    //objArray.Add(obj);

                }
                if (hit.collider.gameObject.CompareTag("ExplosiveBarrel"))
                {

                    ExplosiveBarrelScript.explode = true;

                }


                if (hit.collider.gameObject.CompareTag("enemy"))
                {
                    // public GameObject enemy_1;
                    //hit.collider.
                    //Army enemy_1 = hit.collider.gameObject.GetComponent<Army>();
                    ////enemy_1.enemy_

                    //enemy_1.enemy_hp -= 30.0f;
                    // if (hit.collider.gameObject.GetComponents<Army>().)
                    //{
                    //    //Dead_enemy();

                    //}

                    if (Army.enemy_hp <= 0)
                    {
                        Destroy(hit.collider.gameObject);
                        Army.enemy_hp = 100.0f;
                    }


                    Debug.Log("처맞냐");
                    Army.enemy_hp -= 30.0f;

                }


                //    var hitObject = hit.transform;


                //    if (hit.transform.CompareTag("Target"))
                //    {
                //        Destroy(hit.gameobjecctCompareTag("Target"));
                //    }


                //    if (hitObject.tag == "Target" )
                //    {



                //        //Targets.Enqueue(hitObject);
                //        //Debug.Log("targets enqueue : " + hitObject);
                //        Destroy(gameObject);
                //   }
            }
        }

        else if (Input.GetMouseButton(0) && bullet_count >= 0
            && reload_check == false
            && fire_check == true
            && timer >= attackCoolTime)// && bullet_count <= 50) //연발 최대 50발까지
        {
            //timer = 0; //연발 시간 초기화
            timer = 0;
            //GameObject obj = Instantiate(Bullet, firePos.transform.position, firePos.transform.rotation);

            Fire_Bullet();

            //Destroy(obj, 2.5f);

            if (bullet_count >= 0)
            {


                bullet_count--; // 발사된 총알 카운트
                Audio_Control_Script.fi_sound = true;//발사 사운드 온
            }


            RaycastHit hit;
            Debug.DrawRay(transform.position, transform.forward * Max_Distance, Color.blue, 0.3f); //끝에 0.8f는 보여질 시간

            //레이캐스트 충돌 처리 하려다 일단 보류


            if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
            {
                //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
                if (hit.collider.gameObject.CompareTag("Target"))
                {

                    Destroy(hit.collider.gameObject);
                    //objArray.Add(obj);

                }


                if (hit.collider.gameObject.CompareTag("enemy"))
                {
                    Debug.Log("처맞냐");
                    if (Army.enemy_hp <= 0)
                    {
                       
                        Destroy(hit.collider.gameObject);
                        Army.enemy_hp = 100;
                    }
                   
                    Army.enemy_hp -= 30.0f;

                }

            }
        }

        //if (timer >= 5.0f) //연발 가능 대기시간 5초
        //{
        //    bullet_count = 0;
        //}

    }

    //////
    //아직 Target태그 충돌 안됨
    //////
    //void OnCollisionEnter(Collision collision) //충돌 처리
    //{
    //    if (collision.gameObject.CompareTag("Target"))
    //    {
    //        Destroy(collision.gameObject);
    //    }     
    //}

    public void ReLoad_Bullet()
    {
        bullet_count = 50;

    }

    static public void ReLoad_state_check()
    {

        if (reload_check == false)
            reload_check = true;

        else if (reload_check == true)
            reload_check = false;


    }

    void Create_Bullet() {
        //불릿 list 할당
        Bullets = new List<GameObject>();

        for (int i = 0; i < bullet_MAX; i++)
        {
            GameObject bullet_Obj = Instantiate(bullet);
            Bullets.Add(bullet_Obj);
            Bullets[i].SetActive(false);
        }
    }

    void Fire_Bullet() {
        foreach (GameObject bullet in Bullets) {
            if (bullet.activeInHierarchy == false) {
                bullet.transform.position = firePos.position;
                bullet.transform.rotation = firePos.rotation;
                bullet.SetActive(true);
                break;
            }
        }
    }


    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "enemy")
    //    {
    //        Army.enemy_hp -= 20;
    //    }
    //}
}



