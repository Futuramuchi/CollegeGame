using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DemoMode
{
    public static bool IsEnabled = false;
    public static List<Journal> Marks = null;

    public static void Enable()
    {
        IsEnabled = true;

        Marks = new List<Journal>()
        {
            new Journal() { DeciplineName = "���������� ����������", Mark = 4},
            new Journal() { DeciplineName = "������ �������������� ��", Mark = 4},
            new Journal() { DeciplineName = "����������� ������", Mark = 5},
            new Journal() { DeciplineName = "�����������", Mark = 5}
        };
    }
}
