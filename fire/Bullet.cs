using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    public GameObject player; //플레이어 방향 가져오기위해서

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(player.transform.forward *2.0f); //base 0.5f
        
    }

    //////
    //아직 Target태그 충돌 안됨
    //////

    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Target"))
    //    {

    //        Destroy(collision.gameObject);
    //    }

    //    Destroy(this.gameObject);
    //}
}
