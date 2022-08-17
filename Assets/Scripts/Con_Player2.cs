using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラ情報(プレイヤー)を扱うクラス
/// ターンバトル時のプレイヤーの処理はここ
/// </summary>
public class Con_Player2 : Base_Chara
{
    public static Con_Player2 player = null;
    // 追加ステータス「ぼうぎょ」
    [System.NonSerialized] public bool guard = false;
    [System.NonSerialized] public string txt_guard;

    private void Awake()
    {
        // インスタンス化関係
        if(player == null)
        {
            player = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // プレイヤー情報更新
        player.charaName = "あかぎ";
        player.hp_max = 100;
        player.hp_now = player.hp_max;
        player.attack = 10;
    }

    /// <summary>
    /// 「回復」の処理
    /// </summary>
    /// <returns></returns>
    public int Potion()
    {
        int cure = 80;

        // 回復でHP超過しないようにする処理

        return cure;
    }

    /// <summary>
    /// 「防御」の処理
    /// </summary>
    /// <returns></returns>
    public void Guard()
    {
        guard = true;
        txt_guard = "「防御態勢」(攻撃無効化)";
    }
}
