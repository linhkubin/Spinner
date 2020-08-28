using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpinItem : MonoBehaviour
{
    public Text txt_Number;

    public void SetItem(int _type)
    {
        txt_Number.text = _type.ToString();
    }

    public string GetItem()
    {
        return txt_Number.text;
    }
}
