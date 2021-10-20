using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mark : MonoBehaviour
{
    public Text DesciplineNameField;
    public Text MarkField;

    public void SetMark(string desciplineName, string mark)
    {
        DesciplineNameField.text = desciplineName;
        MarkField.text = mark;
    }
}
