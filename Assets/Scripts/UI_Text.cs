/**
 * @file    UI_Text.cs
 * @brief   文章の出力処理を記載。
 *          以前2つのファイルで書いていたことをこれ1つにまとめる。
 *          拡張性とかは今回は無視で書く。→とりあえず完成だけを目指すため。
 * @author  ShogoN
 * @date    2022.08.22
 */
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text : MonoBehaviour
{
    #region //! テキストの表記関係
    public Text Text_name;
    public Text Text_talk;

    public bool playing = false;
    public float textSpeed = 0.1f;
    #endregion

    #region //! csvファイルの読み込み関係
    // string name_csv = "Story_1";
    TextAsset csvFile;
    public int height;
    List<string[]> csvDatas = new List<string[]>();
    #endregion

    //! テキストデータの格納
    private string text_main;

    public GameObject tab_end = null;

    private void Start()
    {
        Reader_csvFile();
        StartCoroutine(nameof(MainWritter));
    }

    /// <summary>
    /// リストから変数に代入するためのメソッド
    /// </summary>
    /// <param name="num">リストの行数</param>
    private void Write(int num)
    {
        text_main = csvDatas[num][1];
    }

    /// <summary>
    /// csvファイルを読み取り、リスト化する
    /// </summary>
    private void Reader_csvFile()
    {
        csvFile = Resources.Load("Story_1") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        // 「,」で分割し読み込み、1行ずつリストに追加していく
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
            height++;
        }
    }

    /// <summary>
    /// クリックされたかのチェックをするメソッド
    /// </summary>
    /// <returns>クリックされるとtrue、そうでないときはfalse</returns>
    public bool IsClicked()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        return false;
    }
   
    /// <summary>
    /// ナレーション用のテキスト生成
    /// </summary>
    /// <param name="txt">セリフ</param>
    public void DrawText(string txt)
    {
        Text_name.text = "";
        StartCoroutine("CoDrawText", txt);
    }
    /// <summary>
    /// 通常会話用のテキスト生成
    /// </summary>
    /// <param name="name">キャラ名</param>
    /// <param name="txt">セリフ</param>
    public void DrawText(string name, string txt)
    {
        Text_name.text = name + "\n「";
        StartCoroutine("CoDrawText", txt + "」");
    }

    /// <summary>
    /// スキップするためのコルーチン
    /// </summary>
    IEnumerator Skip()
    {
        while (playing) yield return 0;
        while (!IsClicked()) yield return 0;
    }

    /// <summary>
    /// テキストがヌルヌル出てくるためのコルーチン
    /// </summary>
    /// <param name="txt">記述内容</param>
    /// <returns></returns>
    IEnumerator CoDrawText(string txt)
    {
        playing = true;
        float time = 0;
        while (true)
        {
            yield return 0;
            time += Time.deltaTime;

            if (IsClicked()) break; // クリックされると一気に表示

            int len = Mathf.FloorToInt(time / textSpeed);
            if (len > txt.Length) break;
            Text_talk.text = txt.Substring(0, len);
        }
        Text_talk.text = txt;
        yield return 0;
        playing = false;
    }

    /// <summary>
    /// 出力内容を記述していくためのコルーチン
    /// </summary>
    IEnumerator MainWritter()
    {
        for(int i = 1; i < height; i++)
        {
            Write(i);
            DrawText(text_main);
            yield return StartCoroutine(nameof(Skip));
            if (i >= height - 1)
            {
                tab_end.SetActive(true);
            }
        }
    }
}
