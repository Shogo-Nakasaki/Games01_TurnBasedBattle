using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー用
/// 地面と接地しているかのチェック用
/// </summary>
public class check_ground : MonoBehaviour
{
    public bool isGround = false;
    private string tag_ground = "Ground";
    private bool isGround_Enter, isGround_Stay, isGround_Exit;

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

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == tag_ground)
        {
            isGround_Enter = true;
        }
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.tag == tag_ground)
        {
            isGround_Stay = true;
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == tag_ground)
        {
            isGround_Exit = true;
        }
    }
}
