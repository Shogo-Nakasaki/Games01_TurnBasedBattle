using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_battle : MonoBehaviour
{
    public Con_Battle main;

    public void Attack()
    {
        Debug.Log("「こうげき」ボタンが押された");
        main.command = 0;
        main.flag_A = true;
    }
}
