using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace YandexCloudOCR;

public class IamTokenGettingResponse
{
    public string iamToken { get; set; }
    public string expiresAt { get; set; }
}

public class OcrRequest
{
    public string mimeType { get; set; }
    public List<string> languageCodes { get; set; }
    public string model { get; set; }
    public string content { get; set; }
}

public static class OcrClient
{
    static OcrClient()
    {
        Token = GetIamToken();
    }

    public static string GetIamToken()
    {
        using (var httpClient = new HttpClient())
        {
            // httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "y0__xDzyaz6BxjB3RMgjKieqhKxbaxYG5-unQzkYXEyQBr7vPY8Sg");
            var serializeObject = JsonConvert.SerializeObject(new { yandexPassportOauthToken = "y0__xDzyaz6BxjB3RMgjKieqhKxbaxYG5-unQzkYXEyQBr7vPY8Sg" });
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, "https://iam.api.cloud.yandex.net/iam/v1/tokens")
            {
                Content = new StringContent(serializeObject)
            };
            var responseMessage = httpClient.Send(httpRequestMessage);
            var content = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<IamTokenGettingResponse>(content).iamToken;
        }
    }

    public const string XFolderIdHeader = "x-folder-id";
    public const string XFolderIdHeaderValue = "b1g61ei7k8e30blcfbgv";
    public const string xDataLoggingEnabledHeader = "x-data-logging-enabled";
    public const bool xDataLoggingEnabledHeaderValue = true;
    public const string Uri = "https://ocr.api.cloud.yandex.net/ocr/v1/recognizeText";

    public static readonly string Token;

    public static TableResponse SendRequest(string base64Image)
    {
        var req = new OcrRequest()
        {
            mimeType = "JPEG",
            languageCodes = ["*"],
            model = "table",
            content = base64Image
        };
        var json = JsonConvert.SerializeObject(req);
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, Uri)
            {
                Headers =
                {
                    { XFolderIdHeader, XFolderIdHeaderValue },
                    { xDataLoggingEnabledHeader, xDataLoggingEnabledHeaderValue.ToString() },
                },
                Content = content
            };

            var result = client.Send(requestMessage);
            var sResult = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<TableResponse>(sResult);
        }
    }
}

