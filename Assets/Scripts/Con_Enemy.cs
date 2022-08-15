using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Con_Enemy : MonoBehaviour
{
    #region //! 索敵・接敵関係
    [Header("索敵判定")] public check_player c_player1;
    [Header("接敵判定")] public check_player c_player2;
    private bool isPlayer1; // プレイヤーがいるかどうか
    private bool isPlayer2; // プレイヤーと接敵したかどうか
    #endregion

    private string scene_battle = "20_turnbattle";

    // Update is called once per frame
    void Update()
    {
        isPlayer1 = c_player1.IsPlyaer();
        isPlayer2 = c_player2.IsPlyaer();
        // 索敵範囲内にプレイヤー：プレイヤーへ近づく
        if(isPlayer1)
        {
            Debug.Log("敵「索敵範囲内にプレイヤーを検知」");
        }

        // 接敵範囲内にプレイヤー：バトルシーンへ移行
        if(isPlayer2)
        {
            Debug.Log("敵「攻撃を開始」");
            SceneManager.LoadScene(scene_battle);
        }
    }
}
