using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクター情報のベースとなるクラス
/// 攻撃、どくはこちらで実装
/// </summary>
public class Base_Chara : MonoBehaviour
{
    #region //! キャラ情報
    // ステータス
    public string charaName;
    public int hp_max;
    public int hp_now;
    public int attack;
    // 状態異常：毒
    [System.NonSerialized] public bool poison;
    [System.NonSerialized] public int dmg_poison;
    [System.NonSerialized] public int count_poison;
    [System.NonSerialized] public string txt_poison;
    #endregion

    /// <summary>
    /// 「通常攻撃」の処理
    /// </summary>
    /// <returns></returns>
    public virtual int Attack()
    {
        return attack;
    }

    /// <summary>
    /// 「状態異常：毒」の処理
    /// </summary>
    public virtual bool IsPoison()
    {
        int rnd = Random.Range(0, 10); // 0～9の範囲でランダムな整数値が返る。
        
        // 2/10の確率で毒状態になる。
        // if(rnd <= 1 && !poison) //連続で毒状態にならないようにする
        if(rnd <= 1)
        {
            poison = true;
            txt_poison = "「どく状態」(継続ダメージを受ける)";
            count_poison = 0;
        }

        int dmg4poison = 0; // 毒攻撃を受けた際のダメージ量を入力する。毒状態でないなら0。
        // 5ターン経過で毒状態から回復
        if (count_poison > 5)
        {
            poison = false;
            txt_poison = "";
        }
        
        return poison;
        /* あとで考える
        // 毒のダメージ処理
        if (poison)
        {
            dmg4poison = dmg_poison;
            count_poison++;
        }
        else
        {
            dmg4poison = 0;
        }
        */
    }
}
