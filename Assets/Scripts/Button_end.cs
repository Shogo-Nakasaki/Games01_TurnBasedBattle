using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// エンドシーンのボタン用
/// </summary>
public class Button_end : MonoBehaviour
{
    // 遷移先のシーン
    private string txt = "00_title";
    
    /// <summary>
    /// タイトルシーンへ進むためのボタン
    /// </summary>
    public void ToMain()
    {
        SceneManager.LoadScene(txt);
    }
}
