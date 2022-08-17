using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラ情報(エネミー)を扱うクラス
/// ターンバトル時のエネミーの処理はここ
/// </summary>
public class Con_Enemy2 : Base_Chara
{
    public static Con_Enemy2 enemy = null;
    // 追加ステータス「いかり」
    [System.NonSerialized] public bool angry = false;
    [System.NonSerialized] public int count_angry;
    [System.NonSerialized] public string txt_angry;

    private void Awake()
    {
        // インスタンス化関係
        if(enemy == null)
        {
            enemy = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // プレイヤー情報更新
        enemy.charaName = "もんすたー";
        enemy.hp_max = 300;
        enemy.hp_now = enemy.hp_max;
        enemy.attack = 30;
    }

    /// <summary>
    /// 「通常攻撃」の処理
    /// </summary>
    /// <returns></returns>
    public override int Attack()
    {
        int dmg = 0;
        // 怒り状態なら攻撃力2倍
        if(angry)
        {
            dmg = attack * 2;
        }
        else
        {
            dmg = attack;
        }
        return dmg;
    }

    /// <summary>
    /// 「怒り状態」の処理
    /// </summary>
    public void Angry()
    {
        // 怒り状態になるかどうかのチェック
        if(!angry)
        {
            if (enemy.hp_now <= enemy.hp_max / 2)
            {
                angry = true;
                txt_angry = "「怒り状態」(攻撃力2倍)";
                count_angry = 0;
            }
            else
            {
                angry = false;
                txt_angry = "";
            }
        }
        
        // 怒り状態で3回以上行動すると怒りが解ける
        if(count_angry >= 3)
        {
            angry = false;
            txt_angry = "";
        }
    }
}
