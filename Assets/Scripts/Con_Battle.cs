using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Con_Battle : MonoBehaviour
{
    #region //! ターンバトル時のUI関係
    [Header("UIの設定")]
    // プレイヤー
    public Text Text_p_name;
    public Text Text_p_hp;
    public Text Text_p_at;
    public Text Text_p_state;
    // エネミー
    public Text Text_e_name;
    public Text Text_e_hp;
    public Text Text_e_at;
    public Text Text_e_state;
    #endregion
    #region //! シーン遷移関係
    string txt_sc1 = "00_title"; // ゲームオーバー
    string txt_sc2 = "10_exploration"; // ゲーム継続
    #endregion

    #region //! ボタン・フラグ管理
    [System.NonSerialized]public int command = 0;
    [System.NonSerialized]public bool flag_A = false;
    public GameObject tab_button = null;
    #endregion

    private void Update()
    {
        WriteState();
        // 生存確認
        if(Con_Player2.player.hp_now <= 0)
        {
            Debug.Log("ゲームオーバー");
            SceneManager.LoadScene(txt_sc1);
        }
        if(Con_Enemy2.enemy.hp_now <= 0)
        {
            Debug.Log("あなたの勝利です");
            SceneManager.LoadScene(txt_sc2);
        }

        // 行動処理
        if(flag_A)
        {
            tab_button.SetActive(false);
            BattleMain();
            flag_A = false;
            tab_button.SetActive(true);
        }
    }

    /// <summary>
    /// UI情報(キャラ)の更新処理
    /// </summary>
    private void WriteState()
    {
        // 通常ステータス：プレイヤー
        Text_p_name.text = Con_Player2.player.charaName;
        Text_p_hp.text = "HP：" + Con_Player2.player.hp_now.ToString();
        Text_p_at.text = "攻撃力：" + Con_Player2.player.attack.ToString();
        // 状態異常ステータス：プレイヤー
        Text_p_state.text = "状態異常" + Con_Player2.player.txt_guard + Con_Player2.player.txt_poison;

        // 通常ステータス：エネミー
        Text_e_name.text = Con_Enemy2.enemy.charaName;
        Text_e_hp.text = "HP：" + Con_Enemy2.enemy.hp_now.ToString();
        Text_e_at.text = "攻撃力：" + Con_Enemy2.enemy.attack.ToString();
        // 状態異常ステータス：エネミー
        Text_e_state.text = "状態異常" + Con_Enemy2.enemy.txt_poison + Con_Enemy2.enemy.txt_angry;
    }

/// <summary>
/// ターンバトルのメイン処理
/// </summary>
private void BattleMain()
    {
        // 入力内容に応じたプレイヤーの行動処理
        switch(command)
        {
            case 0:
                Debug.Log("こうげき");
                Con_Enemy2.enemy.hp_now -= Con_Player2.player.Attack();
                break;
            case 1:
                Debug.Log("かいふく");
                Con_Player2.player.hp_now += Con_Player2.player.Potion();
                // 最大HPを超過しないための処理
                if(Con_Player2.player.hp_now >= Con_Player2.player.hp_max)
                {
                    Con_Player2.player.hp_now = Con_Player2.player.hp_max;
                }
                break;
            case 2:
                Debug.Log("どく");
                Con_Enemy2.enemy.IsPoison();
                break;
            case 3:
                Debug.Log("ぼうぎょ");
                Con_Player2.player.Guard();
                break;
            default:
                Debug.Log("えらー");
                break;
        }

        // 怒り状態のチェック
        Con_Enemy2.enemy.Angry();

        // エネミーの行動処理
        if(Con_Player2.player.guard)
        {
            Debug.Log("攻撃を防いだ");
        }
        else
        {
            Con_Player2.player.hp_now -= Con_Enemy2.enemy.Attack();
        }

        // ターン終了後の処理
        Con_Player2.player.guard = false;
        Con_Player2.player.txt_guard = "";
        // 毒状態の処理
        if(Con_Player2.player.poison)
        {
            Con_Player2.player.hp_now -= 5;
        }
        if(Con_Enemy2.enemy.poison)
        {
            Con_Enemy2.enemy.hp_now -= 5;
        }
    }
}
