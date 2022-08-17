using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ターンバトル時のメイン部分となるクラス
/// </summary>
public class Con_Battle : MonoBehaviour
{
    #region //! プレイヤー・エネミーの情報関係
    [Header("UIの設定：プレイヤー")]
    public Text Text_p_name;
    public Text Text_p_hp;
    public Text Text_p_at;
    public Text Text_p_state;
    [Header("UIの設定：エネミー")]
    public Text Text_e_name;
    public Text Text_e_hp;
    public Text Text_e_at;
    public Text Text_e_state;
    #endregion
    #region //! 各行動内容の情報関係
    [Header("UIの設定：行動内容")]
    public Text Text_show;

    /// <summary> {0}は{1}をした </summary>
    private string format_battle1 = "{0}は{1}をした";
    /// <summary> {0}は{1}のダメージ </summary>
    private string format_battle2 = "{0}は{1}のダメージ";
    /// <summary> {0}は{1}だ </summary>
    private string format_status  = "{0}は{1}だ";
    /// <summary> しかし、{0}は耐えた </summary>
    private string format_guard   = "しかし、{0}は耐えた";
    #endregion
    #region //! シーン遷移関係
    string txt_sc1 = "00_title"; // ゲームオーバー
    string txt_sc2 = "10_exploration"; // ゲーム継続
    #endregion
    #region //! ボタン・フラグ管理
    [System.NonSerialized]public int command = 0;
    [System.NonSerialized]public bool flag_A = false;
    public GameObject tab_button = null;
    
    // 勝利判定
    private bool win = false;

    // 現在のターン
    private int fase_now = 0;
    #endregion

    private void Update()
    {
        // UI更新
        WriteState();

        // 行動処理
        if(flag_A)
        {
            StartCoroutine(BattleMain());
            flag_A = false;
        }

    }

    /// <summary>
    /// UI情報(キャラ)の更新処理
    /// </summary>
    private void WriteState()
    {
        // 通常ステータス：プレイヤー
        Text_p_name.text = Con_Player2.player.charaName;
        Text_p_hp.text = "HP：" + Con_Player2.player.hp_now.ToString();
        Text_p_at.text = "攻撃力：" + Con_Player2.player.attack.ToString();
        // 状態異常ステータス：プレイヤー
        Text_p_state.text = "状態異常" + Con_Player2.player.txt_guard + Con_Player2.player.txt_poison;

        // 通常ステータス：エネミー
        Text_e_name.text = Con_Enemy2.enemy.charaName;
        Text_e_hp.text = "HP：" + Con_Enemy2.enemy.hp_now.ToString();
        Text_e_at.text = "攻撃力：" + Con_Enemy2.enemy.attack.ToString();
        // 状態異常ステータス：エネミー
        Text_e_state.text = "状態異常" + Con_Enemy2.enemy.txt_poison + Con_Enemy2.enemy.txt_angry;
    }

    /// <summary>
    /// テキスト表記
    /// </summary>
    private void Write_txt(string txt)
    {
        Text_show.text = txt;
    }


    private void Write_txt(string chara1,string charaDo)
    {
        Text_show.text = string.Format(format_battle1, chara1, charaDo);
    }
    /// <summary>
    /// 各ターンの行動状態の表記：基本
    /// </summary>
    /// <param name="chara1">攻撃側</param>
    /// <param name="chara2">被攻撃側</param>
    /// <param name="charaDo">攻撃内容</param>
    /// <param name="dmg">ダメージ量</param>
    private void Write_txt(string chara1,string chara2,string charaDo,int dmg)
    {
        Write_txt(chara1,charaDo);
        
        // プレイヤーが防御状かつ被攻撃側のとき
        if(Con_Player2.player.guard == true && chara2 == Con_Player2.player.name)
        {
            Text_show.text = string.Format(format_guard, chara2);
        }
        else
        {
            Write_txt(chara2, dmg);
        }
    }
    /*
    /// <summary>
    /// 各ターンの行動内容の表記：状態異常
    /// </summary>
    /// <param name="chara">キャラ名</param>
    /// <param name="charaStatus">状態異常内容</param>
    private void Write_txt(string chara, string charaStatus)
    {
        Text_show.text = string.Format(format_status, chara, charaStatus);
    }
    */

    /// <summary>
    /// ダメージ処理時の表記
    /// </summary>
    /// <param name="chara">キャラ名</param>
    /// <param name="dmg">ダメージ量</param>
    private void Write_txt(string chara, int dmg)
    {
        Text_show.text = string.Format(format_battle2, chara, dmg);
    }
    /// <summary>
    /// ゲーム終了時の表記.
    /// 「chara1は力尽きた。chara2の勝利です。」
    /// </summary>
    /// <param name="chara1">勝利者</param>
    /// <param name="chara2">敗北者</param>
    private void Write_End(string chara1,string chara2)
    {
        Text_show.text = chara1 + "は力尽きた\n" + chara2 + "の勝利です。";
    }

    /// <summary>
    ///  各ターン終了フェイズの処理
    /// </summary>
    private void Turn_End()
    {
        // 毒のダメージ処理
        if (Con_Player2.player.poison)
        {
            Write_txt(Con_Player2.player.charaName, "毒状態");
            Write_txt(Con_Player2.player.charaName, 5);
            Con_Player2.player.hp_now -= 5;
        }
        if (Con_Enemy2.enemy.poison)
        {
            Write_txt(Con_Enemy2.enemy.charaName, "毒状態");
            Write_txt(Con_Enemy2.enemy.charaName, 5);
            Con_Enemy2.enemy.hp_now -= 5;
        }

        // 生存確認
        if (Con_Player2.player.hp_now <= 0)
        {
            // Debug.Log("ゲームオーバー");
            win = false;
            Write_End(Con_Player2.player.charaName,Con_Enemy2.enemy.charaName);
            Invoke(nameof(Return), 3.0f);
        }

        if (Con_Enemy2.enemy.hp_now <= 0)
        {
            // Debug.Log("あなたの勝利です");
            win = true;
            Write_End(Con_Enemy2.enemy.charaName, Con_Player2.player.charaName);
            Invoke(nameof(Return), 3.0f);
        }
        Debug.Log("エンドフェーズ");
        tab_button.SetActive(true);
    }

    /// <summary>
    /// ゲーム終了後の遷移
    /// </summary>
    /// <param name="win">勝利したかどうか</param>
    private void Return()
    {
        if (win)
        {
            // 元のシーンに戻る
            SceneManager.LoadScene(txt_sc2);
        }
        else
        {
            // タイトルシーンに戻る
            SceneManager.LoadScene(txt_sc1);
        }
    }

    /// <summary>
    /// ターンバトルのメイン処理
    /// </summary>
    IEnumerator BattleMain()
    {
        tab_button.SetActive(false);

        StartCoroutine(Turn_Start());
        yield return new WaitForSeconds(1.0f);
        
        StartCoroutine(Turn_Main_P());
        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Turn_Main_E());
        yield return new WaitForSeconds(1.0f);
        
        Turn_End();
        Text_show.text = "どうする？";

        yield return null;
    }

    /// <summary>
    /// 各ターン開始フェイズの処理
    /// </summary>
    IEnumerator Turn_Start()
    {
        Debug.Log("スタートフェーズ");

        yield return null;
    }

    /// <summary>
    /// プレイヤーフェイズの処理
    /// </summary>
    IEnumerator Turn_Main_P()
    {
        Debug.Log("プレイヤーフェーズ");
        // ガード状態の解除
        Con_Player2.player.guard = false;
        Con_Player2.player.txt_guard = "";

        // 入力内容に応じたプレイヤーの行動処理
        switch (command)
        {
            case 0: // 通常攻撃
                Debug.Log("こうげき");
                Write_txt(Con_Player2.player.charaName, "「こうげき」");
                Con_Enemy2.enemy.hp_now -= Con_Player2.player.Attack();
                Write_txt(Con_Enemy2.enemy.charaName,Con_Player2.player.Attack());
                break;
            case 1: // 回復行動
                Debug.Log("かいふく");
                Write_txt(Con_Player2.player.charaName + "はポーションを飲み回復した");
                Con_Player2.player.hp_now += Con_Player2.player.Potion();
                // 最大HPを超過しないための処理
                if (Con_Player2.player.hp_now >= Con_Player2.player.hp_max)
                {
                    Con_Player2.player.hp_now = Con_Player2.player.hp_max;
                }
                break;
            case 2: // どく攻撃
                Debug.Log("どく");
                Write_txt(Con_Player2.player.charaName, "「どく攻撃」");
                Con_Enemy2.enemy.IsPoison();
                // 毒状態になったかどうか
                if(Con_Enemy2.enemy.poison)
                {
                    Write_txt(Con_Enemy2.enemy.charaName, "「どく状態」");
                }
                else
                {
                    Write_txt(Con_Enemy2.enemy.charaName + "には効果が無いようだ");
                }
                break;
            case 3: // 防御行動
                Debug.Log("ぼうぎょ");
                Write_txt(Con_Player2.player.charaName, "「防御行動」");
                Con_Player2.player.Guard();
                break;
            default:
                break;
        }

        yield return null;
    }

    /// <summary>
    ///  エネミーフェイズの処理
    /// </summary>
    IEnumerator Turn_Main_E()
    {
        Debug.Log("エネミーフェーズ");
        // 怒り状態のチェック
        Con_Enemy2.enemy.Angry();


        // エネミーの行動処理：通常攻撃
        Write_txt(Con_Enemy2.enemy.charaName, Con_Player2.player.charaName, "「タックル」", Con_Enemy2.enemy.Attack());
        if (Con_Player2.player.guard)
        {
            Debug.Log("攻撃を防いだ");
        }
        else
        {
            Con_Player2.player.hp_now -= Con_Enemy2.enemy.Attack();
        }

        yield return null;
    }
}
