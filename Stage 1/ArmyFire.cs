using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmyFire : MonoBehaviour
{
    private AudioSource Audio;
    private Transform playerpos;

    private float nextFire = 0.0f;
    private readonly float fireRate = 0.1f;

    public bool isFire = true;
    public AudioClip fireSfx;

    private Animator ArmyAnimator;


    public GameObject Bullet;
    public Transform firepos;
    // Start is called before the first frame update
    void Start()
    {
        ArmyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //bool adasd = ArmyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot");

        if (ArmyAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shoot_SingleShot_AR"))
        {
            if (isFire)
            {
                if (Time.time >= nextFire)
                {
                    Fire();
                    nextFire = Time.time + fireRate + Random.Range(0.0f, 0.3f);
                }
            }
        }
    }

    private void Fire()
    {
        // Audio.PlayOneShot(fireSfx, 1.0f);
        GameObject _Bullet = Instantiate(Bullet, firepos.position, firepos.rotation);

        Destroy(_Bullet, 3.0f);
    }
}
