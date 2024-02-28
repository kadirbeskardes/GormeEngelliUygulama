using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace GormeEngelliUygulama.Model
{
    public class TextToSpeech
    {
        static private string speechKey { get; set; }
        static private string speechRegion { get; set; }
        private SemaphoreSlim semaphoreSlim = null;

        public TextToSpeech(SemaphoreSlim semaphore)
        {
            speechKey = "***";//Azure TTS Servis API key
            speechRegion = "***";//Azure TTS Servis API Region
            this.semaphoreSlim = semaphore;
        }

        static void KonusmaSonuc(SpeechSynthesisResult islemSonuc, string text)
        {
            switch (islemSonuc.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(islemSonuc);
                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Logger.Hata(cancellation.Reason.ToString());
                    }
                    break;
                default:
                    break;
            }
        }

        public async Task Konus(string text)
        {
            await semaphoreSlim.WaitAsync();
            try
            {
                var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);
                speechConfig.SpeechSynthesisVoiceName = "tr-TR-EmelNeural";
                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
                {
                    var islemSonuc = await speechSynthesizer.SpeakTextAsync(text);
                    KonusmaSonuc(islemSonuc, text);
                }
            }
            catch (Exception e)
            {
                Logger.Hata(e.Message);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }
    }
}
