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

    // 探索パートでの座標情報
    [System.NonSerialized] public float pos_x;
    [System.NonSerialized] public float pos_y;
    [System.NonSerialized] public float pos_z;
    [System.NonSerialized] public bool exist = false;
    #endregion

    /// <summary>
    /// 「通常攻撃」の処理
    /// </summary>
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
        if(rnd >= 1)
        {
            poison = true;
            txt_poison = "「どく状態」(継続ダメージを受ける)\n";
            count_poison = 0;
        }
        
        return poison;
    }
}
