using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonConverter : MonoBehaviour
{
    public Journal newJornal;
    public Journal otconvertedJournal;


    public void Convert()
    {
        string json = JsonUtility.ToJson(newJornal);
        Debug.Log(json);

        otconvertedJournal = JsonUtility.FromJson<Journal>(json);
    }
}
