using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BackUtilsoftcom.Core
{
    public static class CloudflareApi
    {
        public static bool UploadFile(CloudflareConfig cfg)
        {
            return UploadFileAsync(cfg).GetAwaiter().GetResult();
        }
        public static async Task<bool> UploadFileAsync(CloudflareConfig cfg)
        {
            byte[] fileBytes = File.ReadAllBytes(cfg.FilePath);

            string url = $"{cfg.Endpoint}/{cfg.Bucket}/{cfg.ObjectKey}";
            string contentType = "application/octet-stream";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Put, url))
            {
                var content = new ByteArrayContent(fileBytes);
                content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                request.Content = content;

                // --- AWS Signature V4 ---
                string region = "auto";
                string service = "s3";
                DateTime now = DateTime.UtcNow;
                string amzDate = now.ToString("yyyyMMddTHHmmssZ");
                string dateStamp = now.ToString("yyyyMMdd");

                string payloadHash = ToHexString(SHA256Hash(fileBytes));
                string canonicalUri = $"/{cfg.Bucket}/{cfg.ObjectKey}";
                string canonicalQuery = "";

                string canonicalHeaders =
                    $"content-type:{contentType}\n" +
                    $"host:{new Uri(url).Host}\n" +
                    $"x-amz-content-sha256:{payloadHash}\n" +
                    $"x-amz-date:{amzDate}\n";

                string signedHeaders =
                    "content-type;host;x-amz-content-sha256;x-amz-date";

                string canonicalRequest =
                    "PUT\n" +
                    canonicalUri + "\n" +
                    canonicalQuery + "\n" +
                    canonicalHeaders + "\n" +
                    signedHeaders + "\n" +
                    payloadHash;

                string algorithm = "AWS4-HMAC-SHA256";
                string credentialScope = $"{dateStamp}/{region}/{service}/aws4_request";

                string stringToSign =
                    algorithm + "\n" +
                    amzDate + "\n" +
                    credentialScope + "\n" +
                    ToHexString(SHA256Hash(Encoding.UTF8.GetBytes(canonicalRequest)));

                byte[] signingKey = GetSignatureKey(cfg.SecretKey, dateStamp, region, service);
                string signature = ToHexString(HMACSHA256(signingKey, stringToSign));

                string authorization =
                    $"{algorithm} Credential={cfg.AccessKey}/{credentialScope}, " +
                    $"SignedHeaders={signedHeaders}, Signature={signature}";

                // Headers finais
                request.Headers.Add("x-amz-date", amzDate);
                request.Headers.Add("x-amz-content-sha256", payloadHash);
                request.Headers.TryAddWithoutValidation("Authorization", authorization);

                var response = await client.SendAsync(request);
                return response.IsSuccessStatusCode;
            }
        }

        // ============================================================
        //               MÉTODOS AUXILIARES .NET 4.7.2
        // ============================================================

        private static byte[] SHA256Hash(byte[] data)
        {
            using (var sha = new SHA256Managed())
            {
                return sha.ComputeHash(data);
            }
        }

        private static byte[] HMACSHA256(byte[] key, string data)
        {
            using (var hmac = new HMACSHA256(key))
            {
                return hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            }
        }

        private static byte[] GetSignatureKey(string secretKey, string dateStamp, string region, string service)
        {
            byte[] kDate = HMACSHA256(Encoding.UTF8.GetBytes("AWS4" + secretKey), dateStamp);
            byte[] kRegion = HMACSHA256(kDate, region);
            byte[] kService = HMACSHA256(kRegion, service);
            byte[] kSigning = HMACSHA256(kService, "aws4_request");
            return kSigning;
        }

        private static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }
    }
}
