using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class CurlRequestExample : MonoBehaviour
{
    void Start()
    {
        Debug.Log("開始");
        StartCoroutine(SendChatCompletionRequest());
    }

    IEnumerator SendChatCompletionRequest()
    {
        string url = "http://localhost:1234/v1/chat/completions";
        string jsonPayload = @"
        {
            ""model"": ""berghof-erp-7b"",
            ""messages"": [
                {""role"": ""user"", ""content"": ""あなたは陽菜と言い、22歳の女子大生です。アナウンサーを目指しています。えっちな自己紹介して""}
            ],
            ""temperature"": 0.7
        }";

        // JSONをバイト配列に変換
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonPayload);

        // POSTリクエスト作成
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        Debug.Log("POST");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("リクエスト失敗: " + request.error);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            Debug.Log("END");
        }
    }
}
