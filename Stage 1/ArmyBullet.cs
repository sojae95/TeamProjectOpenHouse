using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyBullet : MonoBehaviour
{
    private GameObject playerPos;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player");


    }

    // Update is called once per frame
    void Update()
    {
        
      
        Vector3 vec = this.transform.position - playerPos.transform.position;

    

        vec.Normalize();
        //vec.y = 0.0f;


        transform.Translate(vec * 0.2f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerManager.Hp -= 20;
        }
    }
}
