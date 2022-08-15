using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button_title : MonoBehaviour
{
    public GameObject credit;
    private string txt = "10_exploration";
    
    /// <summary>
    /// メインタブへ進むためのボタン
    /// </summary>
    public void ToMain()
    {
        SceneManager.LoadScene(txt);
    }

    /// <summary>
    /// クレジットタブを開けるためのボタン
    /// </summary>
    public void Open_Credit()
    {
        credit.SetActive(true);
    }

    /// <summary>
    /// クレジットタブを閉じるためのボタン
    /// </summary>
    public void Close_Credit()
    {
        credit.SetActive(false);
    }
}
