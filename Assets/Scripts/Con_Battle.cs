using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    #region //!フラグ管理
    [System.NonSerialized]public int command = 0;
    [System.NonSerialized]public bool flag_A = false;
    #endregion

    private void Update()
    {
        WriteState();
        if(flag_A)
        {
            BattleMain();
            flag_A = false;
        }
    }

    /// <summary>
    /// UI情報の更新処理
    /// </summary>
    private void WriteState()
    {
        // 通常ステータス：プレイヤー
        Text_p_name.text = Con_Player2.player.charaName;
        Text_p_hp.text = "HP：" + Con_Player2.player.hp_now.ToString();
        Text_p_at.text = "攻撃力：" + Con_Player2.player.attack.ToString();
        // 状態異常ステータス：プレイヤー
        Text_p_state.text = "状態異常" + Con_Player2.player.txt_poison;

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
                break;
            case 2:
                Debug.Log("どく");
                break;
            case 3:
                Debug.Log("ぼうぎょ");
                break;
            default:
                Debug.Log("えらー");
                break;
        }

        // 怒り状態のチェック
        Con_Enemy2.enemy.Angry();

        // エネミーの行動処理
        Con_Player2.player.hp_now -= Con_Enemy2.enemy.Attack();
    }
}
