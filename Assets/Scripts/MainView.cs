using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //유니티에서 제공하는 Scene 관련 패키지

public class MainView : MonoBehaviour
{
    public void SceneChange()
    {
        SceneManager.LoadScene("SampleScene");

    }
}