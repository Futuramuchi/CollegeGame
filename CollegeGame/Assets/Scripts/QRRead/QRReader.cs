using System.Collections;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using ZXing;

public class QRReader : MonoBehaviour
{
    private WebCamTexture _camTexture;

    public RawImage textureContainer;
    public Text ResultTextField;

    private bool _isZoomed = true;

    public void StopCamera()
    {
        _camTexture.Stop();
    }

    void Start()
    {
        _camTexture = new WebCamTexture();

        if (_camTexture != null)
        {
            _camTexture.Play();

            textureContainer.texture = _camTexture;
            textureContainer.material.mainTexture = _camTexture;

            FixVideoInput();
        }
    }

    private void OnDestroy()
    {
        textureContainer.material.mainTexture = null;
    }

    public void ChangeZoom()
    {
        var containerTransform = textureContainer.GetComponent<RectTransform>();

        var newScale = _isZoomed ? 0.6f : 1f;
        _isZoomed = !_isZoomed;
        containerTransform.localScale = new Vector2(newScale, newScale);
    }

    private void FixVideoInput()
    {
        textureContainer.SetNativeSize();
        var containerTransform = textureContainer.GetComponent<RectTransform>();

        var newWidth = Screen.height * _camTexture.width / _camTexture.height;
        containerTransform.sizeDelta = new Vector2(newWidth, Screen.height);

        containerTransform.localRotation = Quaternion.Euler(0, 0, -_camTexture.videoRotationAngle);
    }

    private void Update()
    {
        if (!_camTexture.isPlaying)
            return;

        IBarcodeReader barcodeReader = new BarcodeReader();
        var result = barcodeReader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);

        if (result != null)
        {
            StopCamera();

            if (DemoMode.IsEnabled)
            {
                DemoMode.Marks.Add(new Journal() { DeciplineName = "Физическое моделирование", Mark = 5});
                ResultTextField.text = "Оценка успешно поставлена!";

                return;
            }

            StartCoroutine(JournalRequest(result.Text));
        }
    }

    private IEnumerator JournalRequest(string json)
    {
        using (var webRequest = new UnityWebRequest("http://localhost:50179/api/Journal", "POST"))
        {
            var send = Encoding.UTF8.GetBytes(json);

            webRequest.uploadHandler = new UploadHandlerRaw(send);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-type", "application/json");

            yield return webRequest.SendWebRequest();

            while (!webRequest.isDone)
                yield return null;

            if (webRequest.result == UnityWebRequest.Result.Success && webRequest.responseCode == (int)HttpStatusCode.OK)
                ResultTextField.text = "Оценка успешно поставлена!";

            else
                ResultTextField.text = "Ошибка!";
        }
    }
}