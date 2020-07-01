using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eleva : MonoBehaviour
{
  
    float speed = 3.0f;
    bool moveStart;
    public Transform MaxPos;
    public Transform MinPos;
    bool dirOfElevaY; //  true Up / false Down
    public GameObject hostage;

    // Start is called before the first frame update
    void Start()
    {
        moveStart = false;
        dirOfElevaY = true;
    }

    // Update is called once per frameq
    void Update()
    {
        Debug.Log(moveStart);
        CheckDirOfEleva();
        MoveEleva();
        //if (moveStart == true)
        //{
        //    this.gameObject.transform.Translate(new Vector3(0, 1, 0)*speed*Time.deltaTime);
        //}

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            moveStart = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            moveStart = false;
        }
    }

    void CheckDirOfEleva() {
        if (MaxPos.position.y < this.gameObject.transform.position.y)
        {
            dirOfElevaY = false;
            moveStart = false;
            this.gameObject.transform.position = MaxPos.position;


        }
        else if (MinPos.position.y > this.gameObject.transform.position.y) {
            dirOfElevaY = true;
            moveStart = false;
            this.gameObject.transform.position = MinPos.position;
        }
    }

    void MoveEleva() {


        if (moveStart == true)
        {
            if (dirOfElevaY == true)
            {
                this.gameObject.transform.Translate(new Vector3(0, 1, 0) * speed * Time.deltaTime);

            }
            else if (dirOfElevaY == false) {
                this.gameObject.transform.Translate(new Vector3(0, -1, 0) * speed * Time.deltaTime);
            }
        }
    }

}
