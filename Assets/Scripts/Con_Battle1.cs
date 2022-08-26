using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// ターンバトル時のメイン部分となるクラス
/// </summary>
public class Con_Battle1 : MonoBehaviour
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
    // 名前など
    private string name_player;
    private string name_enemy;
    private bool flag_angry;
    #endregion

    #region //! 各行動内容の表記関係
    [Header("UIの設定：行動内容")]
    public Text Text_show;

    /// <summary> {0}は{1}をした </summary>
    private string format_battle1 = "{0}は{1}をした";
    /// <summary> {0}は{1}のダメージ </summary>
    private string format_battle2 = "{0}は{1}のダメージ";
    /// <summary> {0}は{1}になった </summary>
    private string format_ailment1 = "{0}は{1}になった";
    /// <summary> {0}は{1}のままだ </summary>
    private string format_ailment2 = "{0}は{1}のままだ";
    /// <summary> {0}は{1}から回復した </summary>
    private string format_ailment3 = "{0}は{1}から回復した";
    /// <summary> しかし、{0}は耐えた </summary>
    private string format_guard   = "{0}は攻撃をしのいだ";
    #endregion

    #region //! シーン遷移関係
    // string txt_sc1 = "00_title"; // ゲームオーバー
    string txt_sc2 = "10_exploration"; // ゲーム継続
    #endregion

    #region //! ボタン・フラグ管理
    [System.NonSerialized]public int command = 0;
    /// <summary>ボタンが押されたかのチェック用</summary>
    [System.NonSerialized]public bool flag_A = false;
    /// <summary>各フェーズが終了したかのチェック用</summary>
    [System.NonSerialized]public bool flag_B = false;
    public GameObject tab_button = null;
    
    // 勝利判定
    private bool win = false;

    // 現在のターン
    private int fase_now = 0;
    #endregion

    private void Start()
    {
        // 変数に名前を登録
        name_player = Con_Player2.player.charaName;
        name_enemy  = Con_Enemy2.enemy.charaName;

        Debug.Log("開始");
        StartCoroutine(BattleMain());
    }

    private void Update()
    {
        // UI更新
        WriteState();
    }

    /// <summary>
    /// UI情報(キャラ)の更新処理
    /// </summary>
    private void WriteState()
    {
        // 通常ステータス：プレイヤー
        Text_p_name.text = name_player;
        Text_p_hp.text = "HP：" + Con_Player2.player.hp_now.ToString();
        Text_p_at.text = "攻撃力：" + Con_Player2.player.attack.ToString();
        // 状態異常ステータス：プレイヤー
        Text_p_state.text = "状態異常\n " + Con_Player2.player.txt_guard + Con_Player2.player.txt_poison;

        // 通常ステータス：エネミー
        Text_e_name.text = name_enemy;
        Text_e_hp.text = "HP：" + Con_Enemy2.enemy.hp_now.ToString();
        Text_e_at.text = "攻撃力：" + Con_Enemy2.enemy.attack.ToString();
        // 状態異常ステータス：エネミー
        Text_e_state.text = "状態異常\n " + Con_Enemy2.enemy.txt_angry + Con_Enemy2.enemy.txt_poison;
    }

    /// <summary>
    /// テキスト表記
    /// </summary>
    private void Write_txt(string txt)
    {
        Text_show.text = txt;
    }
    /// <summary>
    /// テキスト表記：「名前」が「○○」をした
    /// </summary>
    /// <param name="chara1">行動者</param>
    /// <param name="charaDo">行動内容</param>
    private void Write_txt(string chara1,string charaDo)
    {
        Text_show.text = string.Format(format_battle1, chara1, charaDo);
    }

    /// <summary>
    /// ダメージ処理時の表記：「名前」は「○○」のダメージ
    /// </summary>
    /// <param name="chara">キャラ名</param>
    /// <param name="dmg">ダメージ量</param>
    private void Write_txt(string chara, int dmg)
    {
        if (Con_Player2.player.guard == true && fase_now == 2)
        {
            // エネミーフェイズ中かつプレイヤーがガード時
            // Debug.Log("防御された");
            Text_show.text = string.Format(format_guard, chara);
        }
        else
        {
            // Debug.Log("防御されなかった");
            Text_show.text = string.Format(format_battle2, chara, dmg);
        }
    }

    /// <summary>
    /// 状態異常時の表記：「名前」は「○○」になった
    /// </summary>
    /// <param name="chara">キャラ名</param>
    /// <param name="status">状態異常名</param>
    private void Write_ailment(string chara, string status)
    {
        Text_show.text = string.Format(format_ailment1, chara, status);
    }

    /// <summary>
    /// 状態異常時の表記：(継続なら)「名前」は「○○」のままだ
    /// </summary>
    /// <param name="chara">キャラ名</param>
    /// <param name="status">状態異常名</param>
    /// <param name="cont">継続状態か</param>
    private void Write_ailment(string chara, string status, bool cont)
    {
        if(cont)
        {
            Text_show.text = string.Format(format_ailment2, chara, status);
        }
        else if(!cont)
        {
            Text_show.text = string.Format(format_ailment3, chara, status);
        }
        else
        {
            Write_ailment(chara, status);
        }
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
    /// ゲーム終了後の遷移
    /// </summary>
    private void Return()
    {
        if (win)
        {
            // 勝利時
            Con_Enemy2.enemy.exist = false; // 対戦した敵キャラの情報を削除

            SceneManager.LoadScene(txt_sc2); // 元のシーンに戻る
        }
        else
        {
            // 敗北時
            Con_Player2.player.exist = false; // プレイヤーの位置情報を削除

            Con_Enemy2.enemy.exist = true;   // 敵キャラが同場所に再度スポーンする
            // SceneManager.LoadScene(txt_sc1); // タイトルシーンへ戻る
            SceneManager.LoadScene(txt_sc2); // 元のシーンに戻る
        }
    }

    /// <summary>
    /// ターンバトルのメイン処理
    /// </summary>
    IEnumerator BattleMain()
    {
        // ボタンを押す入力待ち
        while (!flag_A)
        {
            // Debug.Log("待機");
            yield return new WaitForSeconds(0.5f);
        }

        // ボタンが押されると各ターン処理開始
        while (flag_A)
        {
            tab_button.SetActive(false);

            if (fase_now == 0 && !flag_B)
            {
                StartCoroutine(Turn_Start());
            }
            else if (fase_now == 1 && !flag_B)
            {
                StartCoroutine(Turn_Main_P());
            }
            else if (fase_now == 2 && !flag_B)
            {
                StartCoroutine(Turn_Main_E());
            }
            else if (fase_now == 3 && !flag_B)
            {
                StartCoroutine(Turn_End());
            }
            yield return new WaitForSeconds(0.5f);
        }

        yield return null;
    }

    /// <summary>
    /// 各ターン開始フェイズの処理
    /// </summary>
    IEnumerator Turn_Start()
    {
        flag_B = true;
        // Debug.Log("スタートフェーズ開始");

        // Debug.Log("スタートフェーズ終了");
        fase_now = 1;
        flag_B = false;

        yield return null;
    }

    /// <summary>
    /// プレイヤーフェイズの処理
    /// </summary>
    IEnumerator Turn_Main_P()
    {
        flag_B = true;
        // Debug.Log("プレイヤーフェーズ開始");
        // ガード状態の解除
        Con_Player2.player.guard = false;
        Con_Player2.player.txt_guard = "";

        // 入力内容に応じたプレイヤーの行動処理
        switch (command)
        {
            case 0:
                Debug.Log("こうげき");
                Write_txt(name_player, "「こうげき」");
                yield return new WaitForSeconds(1.0f);

                // Debug.Log("こうげき2");
                Write_txt(name_enemy, Con_Player2.player.Attack());
                Con_Enemy2.enemy.hp_now -= Con_Player2.player.Attack();
                yield return new WaitForSeconds(1.0f);

                break;
            case 1: // 回復行動
                Debug.Log("かいふく");
                Write_txt(name_player + "はポーションを飲み回復した");
                Con_Player2.player.hp_now += Con_Player2.player.Potion();
                // 最大HPを超過しないための処理
                if (Con_Player2.player.hp_now >= Con_Player2.player.hp_max)
                {
                    Con_Player2.player.hp_now = Con_Player2.player.hp_max;
                }
                yield return new WaitForSeconds(1.0f);

                break;
            case 2: // どく攻撃
                // Debug.Log("どく");
                Write_txt(name_player, "「どく攻撃」");
                Con_Enemy2.enemy.IsPoison();
                yield return new WaitForSeconds(1.0f);

                // 「どく」状態になったかどうか
                if (Con_Enemy2.enemy.poison)
                {
                    Write_ailment(name_enemy, "「どく状態」");
                    yield return new WaitForSeconds(1.0f);
                }
                else
                {
                    Write_txt(name_enemy + "には効果が無いようだ");
                    yield return new WaitForSeconds(1.0f);
                }
                break;
            case 3: // 防御行動
                // Debug.Log("ぼうぎょ");
                Write_txt(name_player, "「防御行動」");
                Con_Player2.player.Guard();
                yield return new WaitForSeconds(1.0f);

                break;
            default:
                break;
        }

        Debug.Log("プレイヤーフェーズ終了");
        fase_now = 2;
        flag_B = false;

        yield return null;
    }

    /// <summary>
    ///  エネミーフェイズの処理
    /// </summary>
    IEnumerator Turn_Main_E()
    {
        flag_B = true;
        // Debug.Log("エネミーフェーズ");
        
        // エネミーの行動処理：通常攻撃
        Write_txt(name_enemy,"「タックル」");
        yield return new WaitForSeconds(1.0f);

        // エネミーによるダメージ処理
        Write_txt(name_player, Con_Enemy2.enemy.Attack());
        if (!Con_Player2.player.guard)
        {
            Debug.Log("エネミーの攻撃が成功");
            Con_Player2.player.hp_now -= Con_Enemy2.enemy.Attack();
        }
        yield return new WaitForSeconds(1.0f);

        Con_Enemy2.enemy.count_angry++;
        fase_now = 3;
        flag_B = false;

        yield return null;
    }

    /// <summary>
    ///  各ターン終了フェイズの処理
    /// </summary>
    IEnumerator Turn_End()
    {
        flag_B = true;
        // 怒り状態になるかのチェック
        Con_Enemy2.enemy.Angry();
        if (Con_Enemy2.enemy.angry && !flag_angry)
        {
            // 怒り始め
            flag_angry = true;
            Write_ailment(name_enemy, "怒り状態");
            yield return new WaitForSeconds(1.0f);
        }
        else if(Con_Enemy2.enemy.angry && flag_angry)
        {
            // 怒り継続
            Write_ailment(name_enemy, "怒り状態", true);
            yield return new WaitForSeconds(1.0f);
        }
        else if(!Con_Enemy2.enemy.angry && flag_angry)
        {
            // 怒り終了
            Write_ailment(name_enemy, "怒り状態", false);
            flag_angry = false;
            yield return new WaitForSeconds(1.0f);
        }

        // 「どく」のダメージ処理
        if (Con_Player2.player.poison)
        {
            Write_ailment(name_player, "どく状態", true);
            yield return new WaitForSeconds(1.0f);

            Write_txt(name_player, 5);
            Con_Player2.player.hp_now -= 5;
            yield return new WaitForSeconds(1.0f);
        }
        if (Con_Enemy2.enemy.poison)
        {
            Write_ailment(name_enemy, "どく状態", true);
            yield return new WaitForSeconds(1.0f);

            Write_txt(name_enemy, 5);
            Con_Enemy2.enemy.hp_now -= 5;
            yield return new WaitForSeconds(1.0f);
        }

        // Debug.Log("エンドフェーズ");
        // 生存確認
        if(Con_Player2.player.hp_now > 0 && Con_Enemy2.enemy.hp_now > 0)
        {
            fase_now = 0;
            flag_A = false;
            tab_button.SetActive(true);
            flag_B = false;
            Text_show.text = "どうする？";
            yield return new WaitForSeconds(1.0f);
            
            // 継続時に再度ゲームを回す
            StartCoroutine(BattleMain());
        }
        else if (Con_Player2.player.hp_now <= 0)
        {
            // Debug.Log("ゲームオーバー");
            win = false;
            Write_End(name_player, name_enemy);
            yield return new WaitForSeconds(1.0f);
            Invoke(nameof(Return), 3.0f);
        }
        else if (Con_Enemy2.enemy.hp_now <= 0)
        {
            // Debug.Log("あなたの勝利です");
            win = true;
            Write_End(name_enemy, name_player);
            yield return new WaitForSeconds(1.0f);
            Invoke(nameof(Return), 3.0f);
        }

        yield return null;
    }
}
