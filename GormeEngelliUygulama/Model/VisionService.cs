using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GormeEngelliUygulama.Model
{
    public class VisionService
    {
        private const string ApiKey = "***";//Azure AI Vision API key
        private const string ApiEndpoint = "https://***.cognitiveservices.azure.com/vision/v3.2";//Azure AI Vision API End-Point
        public async Task<string> ResimAciklamaGetir(byte[] imageData)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);

                using (var content = new ByteArrayContent(imageData))
                {
                    content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                    var response = await client.PostAsync($"{ApiEndpoint}/analyze?visualFeatures=Description&language=en&model-version=latest", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsStringAsync();
                        return result;
                    }
                    else
                    {
                        Logger.Hata(response.StatusCode.ToString());
                        return null;
                    }
                }
            }
        }
    }
}
