using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class BulletCount : MonoBehaviour
{
    public Text BulletCountText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletCountText.text = AutomaticGunScript.bullet_count.ToString() + "/50";
    }
}
