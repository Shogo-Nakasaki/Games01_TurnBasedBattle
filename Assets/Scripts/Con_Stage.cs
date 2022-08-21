using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ステージ関係を扱うクラス
/// 今回はゴールのみ
/// </summary>
public class Con_Stage : MonoBehaviour
{
    public check_player c_player_4goal;
    private bool isPlayer;

    private string scene_end  = "01_end";

    private void Update()
    {
        isPlayer = c_player_4goal.IsPlyaer();
        if(isPlayer)
        {
            Debug.Log("ゴール");
            SceneManager.LoadScene(scene_end);
        }
    }
}
