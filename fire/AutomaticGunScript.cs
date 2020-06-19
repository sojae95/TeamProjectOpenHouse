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

    private int bullet_count;
    void Start()
    {
        attackCoolTime = 0.5f; //단발 쿨타임
        timer = 0.5f; //다음 단발 시간 체크
       // Attack();

    }

    void Update()
    {

        timer += Time.deltaTime; //단발 시간 체크
        if (Input.GetMouseButtonDown(0) && timer >= attackCoolTime) //마우스 왼쪽 버튼 && 단발 시간 되었을 때
        {
            timer = 0; //단발 시간 초기화

            //총알 생성
            GameObject obj  = Instantiate(Bullet, firePos.transform.position, firePos.transform.rotation);
            objArray.Add(obj);

        
            Destroy(obj, 2.5f);


            
            //레이캐스트 충돌 처리 하려다 일단 보류
            //RaycastHit hit;

            //if (Physics.Raycast(targetingCam.transform.position, targetingCam.transform.forward, out hit, 100f))
            //{
            //    var hitObject = hit.transform;



            //    if (hitObject.tag == "Target" )
            //    {
            //        Targets.Enqueue(hitObject);
            //        Debug.Log("targets enqueue : " + hitObject);
            //        Destroy(gameObject);
            //    }
            //}
        }

        else if (Input.GetMouseButton(0) && bullet_count <= 50) //연발 최대 50발까지
        {
            //timer = 0; //연발 시간 초기화
            timer = 0;
            GameObject obj = Instantiate(Bullet, firePos.transform.position, firePos.transform.rotation);
            Destroy(obj, 2.5f);

            bullet_count++; // 발사된 총알 카운트

        }

        if (timer >= 5.0f) //연발 가능 대기시간 5초
        {
            bullet_count = 0;
        }

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
          
}
