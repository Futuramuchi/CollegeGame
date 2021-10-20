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
            new Journal() { DeciplineName = "Дискретная математика", Mark = 4},
            new Journal() { DeciplineName = "Основы проектирования БД", Mark = 4},
            new Journal() { DeciplineName = "Графический дизайн", Mark = 5},
            new Journal() { DeciplineName = "Физкультура", Mark = 5}
        };
    }
}
