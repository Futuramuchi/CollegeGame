using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    public UnityEvent OnFailed;

    public InputField LoginInputField;
    public InputField PasswordInputField;

    public static Student User;

    public void TryAutorize()
    {
        //Demo
        if (LoginInputField.text == "Demo" && PasswordInputField.text == "")
        {
            User = new Student()
            {
                CharacterId = -1,
                CouratorName = "Александров А.А.",
                FirstName = "Иван",
                GroupId = 24,
                GroupName = "324-OP",
                Id = 14,
                LastName = "Иванович",
                MiddleName = "Иванов"
            };

            DemoMode.Enable();

            Authorize();
            return;
        }

        StartCoroutine(AuthorizeRequest(LoginInputField.text, PasswordInputField.text));
    }

    private void Authorize()
    {
        if (User != null)
        {
            if (User.CharacterId == -1)
                SceneLoader.LoadScene("ChooseHero");
        
            else
            {
                AvatarsList.Get().SetAvatar((int)User.CharacterId);
                SceneLoader.LoadScene("Main");
            }
        }

        else
        {
            OnFailed?.Invoke();
        }
    }

    private IEnumerator AuthorizeRequest(string login, string password)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"http://localhost:50179/api/Student?login={login}&password={password}"))
        {
            webRequest.SetRequestHeader("Content-type", "application/json");

            yield return webRequest.SendWebRequest();

            while (!webRequest.isDone)
                yield return null;

            byte[] result = webRequest.downloadHandler.data;
            string json = Encoding.UTF8.GetString(result);

            Debug.Log(json);

            User = JsonUtility.FromJson<Student>(json);

            Authorize();
            yield return null;
        }
    }
}
