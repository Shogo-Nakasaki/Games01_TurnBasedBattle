using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test_string : MonoBehaviour
{
    public Text txtBox = null;
    private string baseTxt = "{0}��{1}������";
    private string txt1 = "�v���C���[";
    private string txt2 = "��������";

    private void Start()
    {
        txtBox.text = string.Format(baseTxt,txt1,txt2);
    }

}
