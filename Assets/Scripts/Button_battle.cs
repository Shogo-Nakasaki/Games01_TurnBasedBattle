using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ターンバトル時のボタン用
/// </summary>
public class Button_battle : MonoBehaviour
{
    public Con_Battle main;

    public void B_Attack()
    {
        Debug.Log("「こうげき」ボタンが押された");
        main.command = 0;
        main.flag_A = true;
    }

    public void B_Potion()
    {
        Debug.Log("「かいふく」ボタンが押された");
        main.command = 1;
        main.flag_A = true;
    }

    public void B_Poison()
    {
        Debug.Log("「どく」ボタンが押された");
        main.command = 2;
        main.flag_A = true;
    }

    public void B_Guard()
    {
        Debug.Log("「ぼうぎょ」ボタンが押された");
        main.command = 3;
        main.flag_A = true;
    }
}
