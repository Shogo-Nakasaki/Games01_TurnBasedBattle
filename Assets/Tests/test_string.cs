using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_string : MonoBehaviour
{
    public Text txtBox = null;
    private string baseTxt = "{0}は{1}をした";
    private string txt1 = "プレイヤー";
    private string txt2 = "こうげき";

    private void Start()
    {
        txtBox.text = string.Format(baseTxt,txt1,txt2);
    }

}
