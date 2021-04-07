using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject fire_Direction;

    float activeDelay = 1.0f;
    float bullet_Speed;

    private void OnEnable()
    {
        activeDelay = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        bullet_Speed = -5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.transform.forward * bullet_Speed * Time.deltaTime);
        activeDelay -= Time.deltaTime;
        active_Check();
    }

    void active_Check() {
        if (activeDelay < 0) {
            this.gameObject.SetActive(false);
        }
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
