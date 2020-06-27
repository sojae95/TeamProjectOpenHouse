using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void ChangeTutorialScene()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ChaneStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }


}
