﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Control_Script : MonoBehaviour
{


    //Fire sound
    public AudioSource Fire_audio;
    public AudioClip fire_sound;
    static public bool fi_sound =false;


    //Rifle Reload sound
    public AudioSource Rifle_Reload_audio;
    public AudioClip rifle_reload_sound;
    static public bool ri_re_sound = false;





    // Start is called before the first frame update
    void Start()
    {
        this.Fire_audio = Fire_audio.gameObject.AddComponent<AudioSource>();
        this.Fire_audio.clip = fire_sound;
        this.Fire_audio.loop = false;
        this.Fire_audio.volume = 0.2f;


        this.Rifle_Reload_audio = Rifle_Reload_audio.gameObject.AddComponent<AudioSource>();
        this.Rifle_Reload_audio.clip = rifle_reload_sound;
        this.Rifle_Reload_audio.loop = false;
        this.Rifle_Reload_audio.volume = 0.4f;
    }

    // Update is called once per frame
    void Update()
    {
        if (fi_sound ==true){
            Fire_sound_play();//발사 사운드 재생
        }

        if (ri_re_sound == true){
            Rifle_sound_play();
        }


    }

    void Fire_sound_play() //발사 사운드 재생
    {
        Fire_audio.Play();
        fi_sound = false;
    }

    void Rifle_sound_play()
    {
        Rifle_Reload_audio.Play();

        ri_re_sound = false;
    }


}
