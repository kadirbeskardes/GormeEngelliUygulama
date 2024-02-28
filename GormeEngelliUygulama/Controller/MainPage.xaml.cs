using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Azure.AI.OpenAI;
using Newtonsoft.Json;
using GormeEngelliUygulama.Model;


namespace GormeEngelliUygulama.Controller
{
    public partial class MainPage : ContentPage
    {
        private System.Timers.Timer konumKontrolTimer;
        private System.Timers.Timer resimCekmeTimer;
        private System.Timers.Timer hataMailTimer;

        static private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private static Model.TextToSpeech ttsService = new Model.TextToSpeech(semaphoreSlim);
        public static OpenAIClient client = new OpenAIClient("***");//OpenAI API key
        private static bool mailGonderildi = false;
        public MainPage()
        {
            try
            {
                InitializeComponent();
                konumKontrolTimer = new System.Timers.Timer(60000);
                konumKontrolTimer.Elapsed += OnkonumKontrolTimerElapsed;
                konumKontrolTimer.AutoReset = true;
                konumKontrolTimer.Enabled = true;

                resimCekmeTimer = new System.Timers.Timer(25000);
                resimCekmeTimer.Elapsed += OnresimCekmeElapsed;
                resimCekmeTimer.AutoReset = true;
                resimCekmeTimer.Enabled = true;

                hataMailTimer = new System.Timers.Timer(3600000);
                hataMailTimer.Elapsed += OnhataMailElapsed;
                hataMailTimer.AutoReset = true;
                hataMailTimer.Enabled = true;

            }
            catch (Exception ex)
            {
                Logger.Hata(ex.Message);
            }

        }

        private async void OnkonumKontrolTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                var izinDurum = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (izinDurum != PermissionStatus.Granted)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var requestStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                    });
                    if (izinDurum == PermissionStatus.Granted)
                    {
                        KonumKont(this);
                    }
                }
                if (izinDurum == PermissionStatus.Granted)
                {
                    KonumKont(this);
                }
            }
            catch (Exception ex)
            {
                Logger.Hata(ex.Message);
            }
        }
        private void OnhataMailElapsed(object sender, ElapsedEventArgs e)
        {
            if (DateTime.Now.Day != Preferences.Get("sonGun", 0))
            {
                mailGonderildi = false;
            }
            var dosyaAdi = Preferences.Get("dosyaAdi", "no");
            if (!mailGonderildi && DateTime.Now.Hour == 0 && dosyaAdi != "no" && File.Exists(dosyaAdi))
            {
                Preferences.Set("sonGun", DateTime.Now.Day);
                Logger.MailGonder(dosyaAdi);
                mailGonderildi = true;
            }
        }
        private void OnresimCekmeElapsed(object sender, ElapsedEventArgs e)
        {
            CaptureImage(this, EventArgs.Empty);
        }

        private void CaptureImage(object sender, EventArgs e)
        {
            xctCameraView.Shutter();
        }



        private async void MediaCaptured(object sender, MediaCapturedEventArgs e)
        {
            try
            {
                #region Fotoğrafı Azure'ye gönderip açıklamasını alıyoruz
                byte[] resimByte = e.ImageData;
                var visionService = new VisionService();
                var ciktiJson = await visionService.ResimAciklamaGetir(resimByte);
                var ciktiNesne = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(ciktiJson);
                var ciktiMetin = ciktiNesne.description.captions.FirstOrDefault().text;
                #endregion

                string cikti = await ChatGPT(ciktiMetin);//GPT'ye gönderip Türkçe çeviri alıyoruz.

                Konus(cikti);
            }
            catch (Exception ex)
            {
                Logger.Hata(ex.Message);
            }
        }
        #region Çıktıyı ChatGPT'ye gönderip Türkçeleştir.
        private async Task<string> ChatGPT(string metin)
        {
            string metin1 = "\nYorum katmadan Türkçe yap.";

            var istek = await client.GetChatCompletionsAsync("gpt-3.5-turbo", new ChatCompletionsOptions()
            {
                Messages = { new ChatMessage(ChatRole.System, metin + metin1) }
            });
            string cikti = istek.Value.Choices.FirstOrDefault().Message.Content;
            return cikti;
        }
        #endregion

        #region Bulunan konumun çevresindeki müzeleri getiren metot 
        private async void KonumKont(object sender)
        {
            try
            {
                var konum = await Geolocation.GetLocationAsync();
                using (HttpClient client = new HttpClient())
                {
                    var apiKey = "***";//Google Cloud Places API KEY
                    var apiUrl = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" +
                        $"{konum.Latitude.ToString().Replace(',', '.')}," +
                        $"{konum.Longitude.ToString().Replace(',', '.')}&radius=10000&type=museum&key={apiKey}";

                    var _yanit = await client.GetStringAsync(apiUrl);
                    var mekanlar = JsonConvert.DeserializeObject<GooglePlacesResponse>(_yanit);

                    string ciktiMetin = string.Empty;
                    if (mekanlar.Results.Count == 0)
                    {
                        ciktiMetin = $"Yakınlarınızda ziyaret etmek isteyebileceğiniz bir yer yok.";
                    }
                    else if (mekanlar.Results.Count == 1)
                    {
                        ciktiMetin = $"Yakınlarınızda {mekanlar.Results.FirstOrDefault().Name} var. Ziyaret etmek isteyebilirsiniz.";
                    }
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("Yakınlarınızda ");
                        foreach (var place in mekanlar.Results)
                        {
                            sb.Append(place.Name + "  ,  ");
                        }
                        sb.Append(" var. Bu mekanları ziyaret etmek isteyebilirsiniz.");
                        ciktiMetin = sb.ToString();
                    }
                    Konus(ciktiMetin);
                }
            }
            catch (Exception ex)
            {
                Logger.Hata(ex.Message);
            }
        }
        #endregion

        private async void Konus(string text)
        {
            await ttsService.Konus(text);
        }
    }
}
