using BackupUtilSoftcom;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

internal static class CloudflareConfigDecoder
{
    public static CloudflareConfig LoadConfig()
    {
        string base64 = GetBase64FromWebhook();

        if (string.IsNullOrEmpty(base64))
            throw new Exception("Webhook não retornou base64.");

        return LoadFromBase64(base64);
    }

    public static CloudflareConfig LoadFromBase64(string base64)
    {
        byte[] bytes = Convert.FromBase64String(base64);
        string json = Encoding.UTF8.GetString(bytes);

        var dto = JsonConvert.DeserializeObject<CloudflareConfig>(json);

        if (dto == null)
            throw new Exception("JSON inválido ao decodificar Base64.");

        // Mapeia DTO → Config
        return new CloudflareConfig
        {
            Endpoint = dto.Endpoint,
            AccessKey = dto.AccessKey,
            SecretKey = dto.SecretKey,
            Bucket = dto.Bucket,
            ObjectKey = dto.ObjectKey,
            FilePath = dto.FilePath
        };
    }

    private static string GetBase64FromWebhook()
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Basic",
                "ZjNjOWE3MWE4ZTJiNGRiZWI4NGIyZGZlNDFjOTdhNTU6OWU3YTFiNGMyNmY1NGYxYmIxMmM4ZDk5M2YwYWY2Mzg=");

            var response = client.PostAsync(
                "https://n8n-webhook.services.softcomtecnologia.com/webhook/upsoftware",
                new StringContent(""))
                .Result;

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro HTTP ao chamar webhook: " + response.StatusCode);

            string json = response.Content.ReadAsStringAsync().Result;

            JObject obj = JObject.Parse(json);
            return obj["base64"]?.ToString();
        }
    }
}
