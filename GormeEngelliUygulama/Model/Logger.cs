using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Text;
using Xamarin.Essentials;

namespace GormeEngelliUygulama.Model
{
    public static class Logger
    {
        private const string dosya_isim = "HataLog";
        private const int maxFileSizeInBytes = 1000;
        public static void Hata(string hataMesaji)
        {
            try
            {
                var anaDizin = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var zaman = DateTime.Now;
                var dosyaAdi = Path.Combine(anaDizin, $"{dosya_isim}_{zaman:yyyyMMdd}.txt");
                using (StreamWriter sw = new StreamWriter(dosyaAdi, true))
                {
                    sw.WriteLine($"{zaman} - {hataMesaji}");
                }
                Preferences.Set("dosyaAdi", dosyaAdi);
                if (File.Exists(dosyaAdi) && new FileInfo(dosyaAdi).Length > maxFileSizeInBytes)
                {
                    MailGonder(dosyaAdi);
                    File.WriteAllText(dosyaAdi, string.Empty);
                }
            }
            catch (Exception ex)
            {

            }
        } 


        public static void MailGonder(string ek_txt)
        {
            try
            {
                const string konu = "Log Dosyası Uyarısı";
                const string icerik = "Hata log dosyanızı ekte bulabilirsiniz.";

                var smtp = new SmtpClient
                {
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("***", "***")//,Gönderici mail- Gmail app password
                };

                using (var mailMessage = new MailMessage("***", "****")//Gönderici mail- Alıcı mail
                {
                    Subject = konu,
                    Body = icerik
                })
                {
                    mailMessage.Attachments.Add(new Attachment(ek_txt));
                    smtp.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
