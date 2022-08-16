﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Con_Player2 : Base_Chara
{
    public static Con_Player2 player = null;

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
    public bool Guard()
    {
        bool guard = true;
        return guard;
    }
}
