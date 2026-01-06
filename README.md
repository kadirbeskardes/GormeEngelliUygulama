# 👁️ GormeEngelliUygulama - Görme Engelliler İçin Yardımcı Uygulama

<p align="center">
  <img src="https://img.shields.io/badge/Xamarin. Forms-3498DB?style=for-the-badge&logo=xamarin&logoColor=white" alt="Xamarin.Forms"/>
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/AI-FF6F00?style=for-the-badge" alt="AI"/>
  <img src="https://img.shields.io/badge/Accessibility-4CAF50?style=for-the-badge" alt="Accessibility"/>
</p>

**GormeEngelliUygulama**, görme engelli bireylere yardımcı olmak için tasarlanmış bir mobil uygulamadır. Yapay zeka destekli görüntü tanıma ile kamera aracılığıyla çekilen fotoğrafları sesli olarak tanımlar ve yakındaki müzeleri önererek kültürel deneyimleri erişilebilir kılar.  Ayrıca, geliştiricilere günlük hata raporları göndererek sürekli iyileştirme sağlar.

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Nasıl Çalışır](#-nasıl-çalışır)
- [Teknolojiler](#-teknolojiler)
- [Kurulum](#-kurulum)
- [Kullanım](#-kullanım)
- [Erişilebilirlik](#-erişilebilirlik)
- [Katkıda Bulunma](#-katkıda-bulunma)

## ✨ Özellikler

### 📸 Görüntü Tanıma ve Sesli Anlatım
- Kamera ile çekilen fotoğrafların AI destekli analizi
- Fotoğraftaki nesnelerin, kişilerin ve sahnelerin tanımlanması
- Tanımlanan içeriğin sesli olarak kullanıcıya aktarılması

### 🏛️ Müze Önerileri
- Konum tabanlı yakındaki müzeleri bulma

### 🐛 Hata Raporlama Sistemi
- Otomatik hata yakalama
- Günlük e-posta ile hata özeti
- Geliştiriciler için detaylı log bilgisi
- Uygulama stabilitesi takibi

### 🔊 Sesli Arayüz
- Tam sesli geri bildirim

## 🔄 Nasıl Çalışır

```
┌─────────────────────────────────────────────────────────────────┐
│                    GÖRÜNTÜ TANIMLAMA AKIŞI                      │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│    ┌─────────┐      ┌─────────┐      ┌─────────┐      ┌──────┐ │
│    │ 📸      │      │  🤖     │      │  📝     │      │ 🔊   │ │
│    │ Fotoğraf│ ───► │  AI     │ ───► │ Metin   │ ───► │ Ses  │ │
│    │ Çek     │      │ Analiz  │      │ Oluştur │      │ Çıkış│ │
│    └─────────┘      └─────────┘      └─────────┘      └──────┘ │
│                                                                 │
│    Örnek Çıktı:                                                 │
│    "Önünüzde bir park bankı var. Yanında kırmızı çiçekler       │
│     bulunan yeşil bir alan görünüyor. Bankın üzerinde           │
│     bir kişi oturuyor."                                         │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

```
┌─────────────────────────────────────────────────────────────────┐
│                    MÜZE ÖNERİ AKIŞI                             │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│    ┌─────────┐      ┌─────────┐      ┌─────────┐      ┌──────┐ │
│    │ 📍      │      │  🔍     │      │  🏛️     │      │ 🔊   │ │
│    │ Konum   │ ───► │  Arama  │ ───► │ Müzeler │ ───► │ Sesli│ │
│    │ Al      │      │         │      │ Listele │      │ Anlat│ │
│    └─────────┘      └─────────┘      └─────────┘      └──────┘ │
│                                                                 │
└─────────────────────────────────────────────────────────────────┘
```

## 🛠 Teknolojiler

### Platform
- **Xamarin.Forms** - Cross-platform mobil geliştirme
- **C#** - Programlama dili
- **.NET Standard** - Paylaşımlı kod tabanı

### AI & Cloud Services
- **Azure Cognitive Services** - Computer Vision API
- **Google Cloud Vision** - Görüntü analizi (alternatif)
- **Text-to-Speech API** - Sesli çıktı

### Konum & Haritalar
- **Xamarin.Essentials** - Konum servisleri
- **Google Places API** - Müze verileri

### Hata İzleme
- **SMTP** - E-posta gönderimi
- **Custom Logger** - Hata kayıt sistemi

## 🚀 Kurulum

### Gereksinimler
- Visual Studio 2022 (Xamarin workload)
- Android SDK 29+
- Azure hesabı (Cognitive Services için)

### Adımlar

```bash
# Repository'yi klonlayın
git clone https://github.com/kadirbeskardes/GormeEngelliUygulama.git
cd GormeEngelliUygulama

# Visual Studio ile açın
# GormeEngelliUygulama.sln dosyasını açın
```

### API Yapılandırması

```csharp
// Constants.cs dosyasında API anahtarlarını ayarlayın
public static class Constants
{
    public const string AzureVisionKey = "YOUR_AZURE_KEY";
    public const string AzureVisionEndpoint = "YOUR_ENDPOINT";
    public const string GooglePlacesKey = "YOUR_GOOGLE_KEY";
    public const string SmtpServer = "smtp.yourserver.com";
    public const string ErrorReportEmail = "dev@yourapp.com";
}
```

## 📁 Proje Yapısı

```
GormeEngelliUygulama/
├── GormeEngelliUygulama/              # Shared Xamarin.Forms projesi
│   ├── Models/
│   │   ├── ImageAnalysisResult.cs    # Görüntü analiz sonucu
│   │   ├── Museum.cs                  # Müze modeli
│   │   └── ErrorLog.cs                # Hata log modeli
│   ├── Views/
│   │   ├── MainPage.xaml              # Ana sayfa
│   │   ├── CameraPage.xaml            # Kamera sayfası
│   │   └── MuseumPage.xaml            # Müze listesi
│   ├── ViewModels/
│   │   ├── MainViewModel.cs
│   │   ├── CameraViewModel.cs
│   │   └── MuseumViewModel.cs
│   └── Services/
│       ├── VisionService.cs           # AI görüntü analizi
│       ├── TextToSpeechService.cs     # Sesli çıktı
│       ├── LocationService.cs         # Konum servisi
│       ├── MuseumService.cs           # Müze API
│       └── ErrorReportingService.cs   # Hata raporlama
├── GormeEngelliUygulama.Android/      # Android projesi
└── GormeEngelliUygulama.sln
```

## 📱 Kullanım

### Ana Özellikler

1. **Fotoğraf Çek ve Dinle**
   - Uygulamayı açın
   - "Fotoğraf Çek" butonuna dokunun veya sesli komut verin
   - Uygulama otomatik olarak görüntüyü analiz edecek
   - Sonucu sesli olarak dinleyin

2. **Yakındaki Müzeleri Bul**
   - Konum izni verin
   - Yakındaki müzeleri sesli olarak dinleyin


## 📧 Hata Raporlama

Uygulama, çalışma zamanında oluşan hataları otomatik olarak toplar ve günlük olarak geliştiricilere e-posta ile gönderir: 

```
📧 Günlük Hata Raporu - GormeEngelliUygulama
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
📅 Tarih: 2026-01-06
📱 Toplam Hata: 3
📊 Etkilenen Kullanıcı: 2

Hata Detayları:
1. NullReferenceException - VisionService.cs:45
2. HttpRequestException - MuseumService.cs:78
3. TimeoutException - LocationService.cs:23
```

## 🤝 Katkıda Bulunma

Erişilebilirlik odaklı katkılarınızı bekliyoruz! 

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/AccessibilityFeature`)
3. Commit edin (`git commit -m 'Add AccessibilityFeature'`)
4. Push edin (`git push origin feature/AccessibilityFeature`)
5. Pull Request açın

### Katkı Önerileri
- 🌍 Yeni dil desteği ekleme
- 🔊 Sesli komut iyileştirmeleri
- 🏛️ Yeni mekan türleri (kütüphane, park vb.)
- ♿ Erişilebilirlik iyileştirmeleri

## 📄 Lisans

MIT License

## 🙏 Teşekkürler

Bu uygulama, görme engelli bireylerin günlük yaşamlarını kolaylaştırmak amacıyla geliştirilmiştir. Geri bildirimleriniz bizim için çok değerli! 

---

<p align="center">
  👁️ <strong>GormeEngelliUygulama</strong> - Herkes için erişilebilir bir dünya! 
</p>
