using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StudentJournal : MonoBehaviour
{
    public Text UserInfoField;
    [SerializeField] public List<Journal> Marks;
    public GameObject MarkPrefab;
    public GameObject MarksParentObject;
    
    private void Start()
    {
        UserInfoField.text = $"Èìÿ: {Auth.User.FirstName}" +
            $"\nÔàìèëèÿ: {Auth.User.MiddleName}" +
            $"\nÃðóïïà: {Auth.User.GroupName}";

        if (DemoMode.IsEnabled)
        {
            Marks = DemoMode.Marks;
            UpdateJournal();
            return;
        }

        StartCoroutine(JournalRequest());
    }

    private void UpdateJournal()
    {
        Marks.ForEach(mark =>
        {
            var newMark = Instantiate(MarkPrefab, MarksParentObject.transform);
            newMark.GetComponent<Mark>().SetMark(mark.DeciplineName, mark.Mark.ToString());
        });
    }

    private IEnumerator JournalRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"http://localhost:50179/api/Journal"))
        {
            webRequest.SetRequestHeader("Content-type", "application/json");

            yield return webRequest.SendWebRequest();

            while (!webRequest.isDone)
                yield return null;

            byte[] result = webRequest.downloadHandler.data;
            string json = Encoding.UTF8.GetString(result);

            JsonHelper.FixJson(ref json);
            Debug.Log(json);

            var journal = JsonHelper.FromJson<Journal>(json);

            foreach(var mark in journal)
            {
                var newMark = new Journal()
                {
                    Date = mark.Date,
                    DeciplineName = mark.DeciplineName,
                    DesciplineId = mark.DesciplineId,
                    Id = mark.Id,
                    Mark = mark.Mark,
                    StudentId = mark.StudentId,
                    StudentName = mark.StudentName,
                    TeacherId = mark.TeacherId,
                    TeacherName = mark.TeacherName
                };

                Marks.Add(newMark);
            }

            UpdateJournal();
            yield return null;
        }
    }
}
