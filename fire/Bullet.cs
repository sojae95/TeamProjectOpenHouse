using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject fire_Direction;

    float activeDelay = 1.0f;

    private void OnEnable()
    {
        activeDelay = 1.0f;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(player.transform.forward * 2.0f); //base 0.5f

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
