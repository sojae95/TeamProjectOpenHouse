using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    private float dist = 0;
    private float height = 0;
    private float dampTrace = 20.0f;
  
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camPosUpdate();
    }

    void camPosUpdate() {
        //transform.position = Vector3.Lerp(target.position - (target.forward * +dist) + (Vector3.up * height), transform.position,
        //     Time.deltaTime * dampTrace);

        transform.rotation = target.rotation;

        transform.LookAt(target.position);
    }
}
