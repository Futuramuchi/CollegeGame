using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AvatarPreview : MonoBehaviour
{
    private int _counter = 0;
    private int _maxValue = 0;

    public GameObject PreviewParentGameObject;
    public List<GameObject> PreviewAvatarObjects;

    private void Start() 
    {
        _maxValue = AvatarsList.Get().Avatars.Count;

        AvatarsList.Get().Avatars.ForEach(avatar =>
        {
            var newObj = Instantiate(avatar.Model, PreviewParentGameObject.transform);
            PreviewAvatarObjects.Add(newObj);
        });

        ShowAvatarByIndex(0);
    }

    public void Next()
    {
        _counter = (_counter + 1) % _maxValue;
        ShowAvatarByIndex(_counter);
    }

    public void Prev()
    {
        _counter = Mathf.Abs((_counter  - 1)% _maxValue);
        ShowAvatarByIndex(_counter);
    }

    private void ShowAvatarByIndex(int id)
    {
        foreach (var avatar in PreviewAvatarObjects)
            avatar.SetActive(false);

        PreviewAvatarObjects[id].SetActive(true);
    }

    public void SetAvatar()
    {
        AvatarsList.Get().SetAvatar(_counter);
        Auth.User.CharacterId = _counter;

        Debug.Log(Auth.User.CharacterId);

        StartCoroutine(PostRequest(Auth.User));
    }

    private IEnumerator PostRequest(Student student)
    {
        string json = JsonUtility.ToJson(student);
        var webRequest = new UnityWebRequest("http://localhost:50179/api/Student", "POST");

        Debug.Log(json);
        var post = Encoding.UTF8.GetBytes(json);

        webRequest.uploadHandler = new UploadHandlerRaw(post);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-type", "application/json");

        yield return webRequest.SendWebRequest();
    }
}