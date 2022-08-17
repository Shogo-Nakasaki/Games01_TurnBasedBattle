using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 探索パート、プレイヤー用
/// 地面と接地しているかのチェック用
/// </summary>
public class check_ground : MonoBehaviour
{
    public bool isGround = false;
    private string tag_ground = "Ground";
    private bool isGround_Enter, isGround_Stay, isGround_Exit;

    /// <summary>
    /// 地面に接地しているかのチェック
    /// </summary>
    /// <returns>接地時はtrue,非接地時はfalse</returns>
    public bool IsGround()
    {
        if (isGround_Enter || isGround_Stay)
        {
            isGround = true;
        }
        else if (isGround_Exit)
        {
            isGround = false;
        }

        isGround_Enter = false;
        isGround_Stay = false;
        isGround_Exit = false;
        return isGround;
    }

    // トリガー内に入ったとき
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == tag_ground)
        {
            isGround_Enter = true;
        }
    }
    // トリガー内に入り続けているとき
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == tag_ground)
        {
            isGround_Stay = true;
        }
    }
    // トリガー内から出たとき
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == tag_ground)
        {
            isGround_Exit = true;
        }
    }
}
