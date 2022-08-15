using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵キャラ用
/// 索敵範囲内にプレイヤーがいるかのチェック用
/// </summary>
public class check_player : MonoBehaviour
{
    public bool isPlayer = false;
    private string tag_player = "Player";
    private bool isPlayer_Enter, isPlayer_Stay, isPlayer_Exit;
    /// <summary>
    /// プレイヤーが判定内にいるかのチェック
    /// </summary>
    /// <returns>プレイヤーがいるとtrue,いないとfalse</returns>
    public bool IsPlyaer()
    {
        if (isPlayer_Enter || isPlayer_Stay)
        {
            isPlayer = true;
        }
        else if(isPlayer_Exit)
        {
            isPlayer = false;
        }

        isPlayer_Enter = false;
        isPlayer_Stay = false;
        isPlayer_Exit = false;
        return isPlayer;
    }

    // トリガー内に入ったとき
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.tag == tag_player)
        {
            isPlayer_Enter = true;
        }
    }
    // トリガー内に入り続けているとき
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == tag_player)
        {
            isPlayer_Stay = true;
        }

    }
    // トリガー内から出たとき
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == tag_player)
        {
            isPlayer_Exit = true;
        }

    }
}
