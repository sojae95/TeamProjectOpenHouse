using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    TextMeshProUGUI textmeshPro;
    float time = 0;
    void Awake()
    {
         textmeshPro = GetComponent<TextMeshProUGUI>();
   
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(time < 3.0f)
      {
          textmeshPro.color = new Color(192.0f/255.0f, 133.0f / 255.0f, 0.0f / 255.0f, time / 3);
      }
      else
      {
            time = 0;  
      }

      time += Time.deltaTime;

      if (Input.GetKeyDown(KeyCode.Space))
      {
            SceneManager.LoadScene("StageScene");
      }
          


    }
}
