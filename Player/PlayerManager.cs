using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    //플레이어 이동:  FPS - 전후좌우(조이스틱) + 뛰기(보류) + 앉기(최대한 마지막), 점프, 총 쏘기
    //플레이어 애니메이션:  기본적으로 있는거 + 앉기(제작 - 최대한 마지막에), 총관련 애니메이션
    //Player Manager(스크립트) 만들어서 플레이어에 관한 내용들 넣어두고 GM에서 관리
    //Player Manager 스크립트에 들어갈 내용: HP, 속도, 키입력, 키입력에 따른 애니메이션

    public enum PlayerState
    {
        idle,
        walk,
        shoot
        // 앉기, 뛰기(보류)
    }

    PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.idle;
    }
    // Update is called once per frame
    void Update()
    {

    }

    void playerMove() { 
    
    }

    void shootGun() { 
    
    }
}
