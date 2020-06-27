using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyBullet : MonoBehaviour
{

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindGameObjectWithTag("Player");

    
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 vec = this.transform.position - player.transform.position;
        vec.Normalize();
        vec.y = 0.0f;


        transform.Translate(vec * 0.2f);
    }
}
