using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_title : MonoBehaviour
{
    private string txt = "10_exploration";
    public void ToMain()
    {
        SceneManager.LoadScene(txt);
    }

}
