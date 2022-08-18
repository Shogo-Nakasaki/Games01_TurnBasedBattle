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

    private Vector3 vec_enemy;

    private void Start()
    {
        // 2回目以降で起動
        if(Con_Enemy2.enemy.exist2)
        {
            // enemyのexistがtrue:座標更新、enemyのexistがfalse：破壊
            if (Con_Enemy2.enemy.exist)
            {
                this.transform.position = new Vector3(Con_Enemy2.enemy.pos_x,
                                                      Con_Enemy2.enemy.pos_y,
                                                      Con_Enemy2.enemy.pos_z);
                Con_Enemy2.enemy.exist = false;
            }
            else if (!Con_Enemy2.enemy.exist)
            {
                Destroy(this.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        vec_enemy = this.transform.position;

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
            // Debug.Log("敵「攻撃を開始」");
            // 各キャラクターの座標情報の保存をする
            Con_Player2.player.exist = true;
            Con_Enemy2.enemy.exist2 = true;
            Con_Enemy2.enemy.pos_x = vec_enemy.x;
            Con_Enemy2.enemy.pos_y = vec_enemy.y;
            Con_Enemy2.enemy.pos_z = vec_enemy.z;

            SceneManager.LoadScene(scene_battle);
        }
    }
}
