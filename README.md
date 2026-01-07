# 👁️ GörmeEngelliUygulama

<div align="center">

![Xamarin.Forms](https://img.shields.io/badge/Xamarin.Forms-5.0.0.2622-3498DB?style=for-the-badge&logo=xamarin&logoColor=white)
![C#](https://img.shields.io/badge/C%23-.NET%20Standard%202.0-239120?style=for-the-badge&logo=csharp&logoColor=white)
![Azure](https://img.shields.io/badge/Azure%20AI-Cognitive%20Services-0078D4?style=for-the-badge&logo=microsoft-azure&logoColor=white)
![OpenAI](https://img.shields.io/badge/OpenAI-GPT--3.5--Turbo-412991?style=for-the-badge&logo=openai&logoColor=white)
![Android](https://img.shields.io/badge/Android-SDK%2021+-3DDC84?style=for-the-badge&logo=android&logoColor=white)

**Görme Engelli Bireyler İçin Yapay Zeka Destekli Mobil Yardımcı Uygulama**

*Kamera ile çevrenizdeki dünyayı sesli olarak keşfedin*

</div>

---

## 📋 İçindekiler

- [Proje Hakkında](#-proje-hakkında)
- [Temel Özellikler](#-temel-özellikler)
- [Mimari Yapı](#-mimari-yapı)
- [Proje Yapısı](#-proje-yapısı)
- [Kullanılan Teknolojiler](#-kullanılan-teknolojiler)
- [NuGet Paketleri](#-nuget-paketleri)
- [API Entegrasyonları](#-api-entegrasyonları)
- [Kod Detayları](#-kod-detayları)
- [Android İzinleri](#-android-izinleri)
- [Zamanlayıcı Sistemi](#-zamanlayıcı-sistemi)
- [Hata Yönetimi ve Loglama](#-hata-yönetimi-ve-loglama)
- [Kurulum](#-kurulum)
- [Gereksinimler](#-gereksinimler)

---

## 🎯 Proje Hakkında

**GörmeEngelliUygulama**, görme engelli bireylerin günlük yaşamlarını kolaylaştırmak amacıyla geliştirilmiş bir Xamarin.Forms tabanlı Android uygulamasıdır. 

Uygulama, cihazın kamerasını kullanarak otomatik olarak fotoğraf çeker, bu fotoğrafları yapay zeka ile analiz eder ve çevredeki nesneleri, kişileri ve sahneleri **Türkçe sesli olarak** kullanıcıya aktarır. Ayrıca kullanıcının konumuna göre yakındaki müzeleri tespit ederek sesli bilgilendirme yapar.

### 🔄 Çalışma Akışı

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                         GÖRÜNTÜ TANIMLAMA AKIŞI                              │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│   ┌──────────┐    ┌──────────────┐    ┌──────────┐    ┌──────────────────┐ │
│   │ 📸       │    │ ☁️ Azure     │    │ 🤖 OpenAI│    │ 🔊 Azure TTS     │ │
│   │ Kamera   │───►│ Computer     │───►│ GPT-3.5  │───►│ (tr-TR-EmelNeural│ │
│   │ (25 sn)  │    │ Vision API   │    │ Türbo    │    │ Seslendirme)     │ │
│   └──────────┘    └──────────────┘    └──────────┘    └──────────────────┘ │
│                                                                             │
│   1. Her 25 saniyede   2. Görüntü analizi   3. İngilizce metin   4. Türkçe  │
│      otomatik fotoğraf    ve İngilizce         Türkçeye             sesli   │
│      çekimi               açıklama             çevrilir             çıktı   │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────────────┐
│                           MÜZE ÖNERİ AKIŞI                                   │
├─────────────────────────────────────────────────────────────────────────────┤
│                                                                             │
│   ┌──────────┐    ┌──────────────┐    ┌──────────────┐    ┌──────────────┐ │
│   │ 📍       │    │ 🗺️ Google   │    │ 📋 Mekan     │    │ 🔊 Sesli     │ │
│   │ GPS      │───►│ Places API   │───►│ Listesi      │───►│ Bilgilendirme│ │
│   │ Konum    │    │ (10km yarıçap│    │ Oluşturma    │    │              │ │
│   └──────────┘    └──────────────┘    └──────────────┘    └──────────────┘ │
│                                                                             │
│   Her 60 saniyede konum kontrolü yapılır ve yakındaki müzeler sesli olarak  │
│   kullanıcıya bildirilir.                                                   │
│                                                                             │
└─────────────────────────────────────────────────────────────────────────────┘
```

---

## ✨ Temel Özellikler

### 📸 Otomatik Görüntü Tanıma
- Her **25 saniyede** otomatik fotoğraf çekimi
- Azure Computer Vision API ile görüntü analizi
- Fotoğraftaki nesnelerin, kişilerin ve sahnelerin tanımlanması
- Açıklama metinlerinin güvenilirlik skoru ile birlikte alınması

### 🌐 Türkçe Dil Desteği
- OpenAI GPT-3.5 Turbo ile İngilizce açıklamaların Türkçeye çevrilmesi
- Yorum katılmadan doğrudan çeviri yapılması
- Doğal ve anlaşılır Türkçe çıktılar

### 🔊 Sesli Geri Bildirim
- Azure Text-to-Speech servisi entegrasyonu
- **tr-TR-EmelNeural** Türkçe kadın sesi kullanımı
- SemaphoreSlim ile senkronize sesli çıktı yönetimi
- Çakışmayan sesli bildirimler

### 🏛️ Müze Önerileri
- Xamarin.Essentials ile GPS konum alımı
- Google Places API ile yakındaki müzelerin aranması
- **10 km** yarıçap içindeki müzelerin listelenmesi
- Dinamik mesaj oluşturma (müze sayısına göre)

### 📧 Otomatik Hata Raporlama
- Günlük hata log dosyası oluşturma
- Her gece saat 00:00'da otomatik e-posta gönderimi
- Log dosyası boyut kontrolü (1000 byte üzeri otomatik gönderim)
- Outlook SMTP entegrasyonu

---

## 🏗️ Mimari Yapı

Uygulama **MVC (Model-View-Controller)** benzeri bir mimari yapı kullanmaktadır:

```
GormeEngelliUygulama/
│
├── 📁 Model/                    # Veri modelleri ve servisler
│   ├── GooglePlacesResponse.cs  # Google Places API yanıt modeli
│   ├── RootObject.cs            # Azure Vision API yanıt modeli
│   ├── VisionService.cs         # Azure Computer Vision servisi
│   ├── TextToSpeech.cs          # Azure TTS servisi
│   └── Logger.cs                # Hata loglama sistemi
│
├── 📁 View/                     # Kullanıcı arayüzü
│   └── MainPage.xaml            # Ana sayfa XAML tasarımı
│
├── 📁 Controller/               # İş mantığı ve kontrol
│   └── MainPage.xaml.cs         # Ana sayfa code-behind
│
├── App.xaml                     # Uygulama kaynakları
└── App.xaml.cs                  # Uygulama başlangıç noktası
```

---

## 📂 Proje Yapısı

### 📱 Ana Proje (GormeEngelliUygulama)

| Dosya | Açıklama |
|-------|----------|
| `App.xaml` | Uygulama düzeyinde XAML kaynakları |
| `App.xaml.cs` | Uygulama yaşam döngüsü yönetimi (OnStart, OnSleep, OnResume) |
| `AssemblyInfo.cs` | Assembly meta bilgileri |

### 🎨 View Katmanı

| Dosya | Açıklama |
|-------|----------|
| `MainPage.xaml` | Xamarin Community Toolkit CameraView bileşeni içeren ana sayfa |

**MainPage.xaml Yapısı:**
- `ContentPage` ana container
- `Grid` layout sistemi
- `xct:CameraView` kamera bileşeni
  - `CaptureMode="Photo"` - Fotoğraf çekme modu
  - `MediaCaptured` event handler - Çekim sonrası işlem

### 🎮 Controller Katmanı

| Dosya | Açıklama |
|-------|----------|
| `MainPage.xaml.cs` | Tüm iş mantığı, zamanlayıcılar ve API çağrıları |

**Temel Metodlar:**
- `OnkonumKontrolTimerElapsed()` - Konum izni kontrolü ve müze arama
- `OnresimCekmeElapsed()` - Otomatik fotoğraf çekimi tetikleyici
- `OnhataMailElapsed()` - Günlük hata e-postası kontrolü
- `MediaCaptured()` - Fotoğraf işleme ve AI analizi
- `ChatGPT()` - OpenAI ile Türkçe çeviri
- `KonumKont()` - Google Places API ile müze arama
- `Konus()` - TTS ile sesli çıktı

### 📦 Model Katmanı

#### VisionService.cs
Azure Computer Vision API entegrasyonu sağlar:
- HTTP POST ile görüntü gönderimi
- `application/octet-stream` content type
- `visualFeatures=Description` parametresi
- API v3.2 kullanımı

#### TextToSpeech.cs
Azure Cognitive Services Speech SDK entegrasyonu:
- `SpeechConfig` yapılandırması
- `SpeechSynthesizer` ile ses sentezi
- Thread-safe yapı için SemaphoreSlim kullanımı
- Hata durumu yönetimi (Canceled, Error)

#### Logger.cs
Hata loglama ve raporlama sistemi:
- Günlük log dosyası oluşturma (`HataLog_YYYYMMDD.txt`)
- `ApplicationData` klasörüne kayıt
- Dosya boyutu kontrolü (max 1000 byte)
- SMTP ile e-posta gönderimi (Outlook)

#### GooglePlacesResponse.cs
Google Places API yanıt modeli:
- `Results` - Mekan listesi
- `Status` - API yanıt durumu
- `PlaceResult` - Mekan detayları (Name, Vicinity, Geometry)
- `Location` - Koordinat bilgisi (Lat, Lng)

#### RootObject.cs
Azure Vision API yanıt modeli:
- `Description` - Görüntü açıklaması
- `Captions` - Açıklama metinleri ve güvenilirlik skorları
- `Tags` - Görüntü etiketleri
- `Metadata` - Görüntü boyut ve format bilgisi

### 🤖 Android Projesi (GormeEngelliUygulama.Android)

| Dosya | Açıklama |
|-------|----------|
| `MainActivity.cs` | Android giriş noktası, platform başlatma |
| `AndroidManifest.xml` | İzinler ve uygulama yapılandırması |
| `Resource.designer.cs` | Otomatik oluşturulan kaynak referansları |

---

## 🛠️ Kullanılan Teknolojiler

| Teknoloji | Versiyon | Kullanım Amacı |
|-----------|----------|----------------|
| **Xamarin.Forms** | 5.0.0.2622 | Cross-platform mobil uygulama framework'ü |
| **.NET Standard** | 2.0 | Paylaşımlı kod kütüphanesi |
| **Xamarin.Essentials** | 1.8.1 | Konum, izinler ve platform özellikleri |
| **Xamarin.CommunityToolkit** | 2.0.6 | CameraView bileşeni |
| **Azure.AI.OpenAI** | 1.0.0-beta.8 | GPT-3.5 Turbo entegrasyonu |
| **Microsoft.CognitiveServices.Speech** | 1.34.1 | Text-to-Speech servisi |
| **Newtonsoft.Json** | 13.0.3 | JSON serileştirme/deserileştirme |

---

## 📦 NuGet Paketleri

```xml
<PackageReference Include="Azure.AI.OpenAI" Version="1.0.0-beta.8" />
<PackageReference Include="Betalgo.OpenAI.GPT3" Version="6.8.4" />
<PackageReference Include="Google.Apis.Texttospeech.v1" Version="1.66.0.3295" />
<PackageReference Include="Google.Cloud.TextToSpeech.V1" Version="3.3.0" />
<PackageReference Include="Microsoft.CognitiveServices.Speech" Version="1.34.1" />
<PackageReference Include="Microsoft.Extensions.Hosting" Version="8.0.0" />
<PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
<PackageReference Include="Xam.Plugin.Media" Version="6.0.2" />
<PackageReference Include="Xamarin.CommunityToolkit" Version="2.0.6" />
<PackageReference Include="Xamarin.Forms" Version="5.0.0.2622" />
<PackageReference Include="Xamarin.Essentials" Version="1.8.1" />
```

---

## 🔌 API Entegrasyonları

### 1️⃣ Azure Computer Vision API
- **Endpoint:** `https://***.cognitiveservices.azure.com/vision/v3.2`
- **Özellik:** `visualFeatures=Description`
- **Dil:** İngilizce (en)
- **Amaç:** Görüntü analizi ve açıklama oluşturma

### 2️⃣ OpenAI GPT-3.5 Turbo
- **Model:** `gpt-3.5-turbo`
- **Amaç:** İngilizce açıklamaları Türkçeye çevirme
- **Prompt:** `"{metin}\nYorum katmadan Türkçe yap."`

### 3️⃣ Azure Speech Services (TTS)
- **Ses:** `tr-TR-EmelNeural`
- **Amaç:** Türkçe sesli çıktı üretimi
- **SDK:** Microsoft.CognitiveServices.Speech

### 4️⃣ Google Places API
- **Endpoint:** `maps.googleapis.com/maps/api/place/nearbysearch/json`
- **Yarıçap:** 10.000 metre (10 km)
- **Tür:** `museum` (müze)
- **Amaç:** Yakındaki müzeleri bulma

---

## ⚙️ Kod Detayları

### Zamanlayıcı Başlatma
```csharp
// Konum kontrolü: Her 60 saniyede bir
konumKontrolTimer = new System.Timers.Timer(60000);

// Fotoğraf çekimi: Her 25 saniyede bir  
resimCekmeTimer = new System.Timers.Timer(25000);

// Hata e-postası kontrolü: Her 1 saatte bir
hataMailTimer = new System.Timers.Timer(3600000);
```

### SemaphoreSlim ile Senkronizasyon
```csharp
static private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
// TTS çağrılarının sıralı yapılmasını sağlar
```

### Konum İzni Yönetimi
```csharp
var izinDurum = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
if (izinDurum != PermissionStatus.Granted)
{
    var requestStatus = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
}
```

---

## 📱 Android İzinleri

`AndroidManifest.xml` dosyasında tanımlı izinler:

| İzin | Açıklama |
|------|----------|
| `ACCESS_NETWORK_STATE` | Ağ durumu kontrolü |
| `CAMERA` | Kamera erişimi |
| `ACCESS_FINE_LOCATION` | Hassas konum erişimi |
| `ACCESS_COARSE_LOCATION` | Yaklaşık konum erişimi |
| `ACCESS_BACKGROUND_LOCATION` | Arka plan konum erişimi |
| `WAKE_LOCK` | Ekran uyanık tutma |
| `MODIFY_AUDIO_SETTINGS` | Ses ayarları değiştirme |
| `READ_EXTERNAL_STORAGE` | Harici depolama okuma |
| `WRITE_EXTERNAL_STORAGE` | Harici depolama yazma (SDK 28'e kadar) |
| `FLASHLIGHT` | Flaş kontrolü |

**Minimum SDK:** 21 (Android 5.0 Lollipop)  
**Hedef SDK:** 33 (Android 13)

---

## ⏱️ Zamanlayıcı Sistemi

| Zamanlayıcı | Süre | İşlev |
|-------------|------|-------|
| `konumKontrolTimer` | 60.000 ms (1 dk) | GPS konum kontrolü ve müze arama |
| `resimCekmeTimer` | 25.000 ms (25 sn) | Otomatik fotoğraf çekimi |
| `hataMailTimer` | 3.600.000 ms (1 saat) | Gece 00:00'da hata e-postası kontrolü |

---

## 🐛 Hata Yönetimi ve Loglama

### Log Dosyası Yapısı
- **Konum:** `Environment.SpecialFolder.ApplicationData`
- **İsimlendirme:** `HataLog_YYYYMMDD.txt`
- **Format:** `{tarih/saat} - {hata mesajı}`
- **Maksimum Boyut:** 1000 byte

### E-posta Gönderimi
- **SMTP Sunucu:** `smtp-mail.outlook.com`
- **Port:** 587
- **SSL:** Etkin
- **Tetikleyici:** Gece saat 00:00 veya dosya boyutu aşımı

### Preferences ile Durum Takibi
- `dosyaAdi` - Son log dosyası yolu
- `sonGun` - Son e-posta gönderilen gün

---

## 🚀 Kurulum

### Gereksinimler

- Visual Studio 2022 (Xamarin workload yüklü)
- Android SDK 21+ 
- .NET Standard 2.0 desteği

### Adımlar

1. **Projeyi Klonlayın**
   ```bash
   git clone https://github.com/kullanici/GormeEngelliUygulama.git
   ```

2. **Visual Studio'da Açın**
   - `GormeEngelliUygulama.sln` dosyasını açın

3. **API Anahtarlarını Yapılandırın**
   - `MainPage.xaml.cs` → OpenAI API key
   - `VisionService.cs` → Azure Vision API key ve endpoint
   - `TextToSpeech.cs` → Azure Speech API key ve region
   - `MainPage.xaml.cs` → Google Places API key
   - `Logger.cs` → E-posta kimlik bilgileri

4. **NuGet Paketlerini Geri Yükleyin**
   - Solution üzerine sağ tıklayın → "Restore NuGet Packages"

5. **Derleyin ve Çalıştırın**
   - Android emülatörü veya fiziksel cihaz seçin
   - F5 veya Debug → Start Debugging

---

## 📊 Teknik Özellikler Özeti

| Özellik | Değer |
|---------|-------|
| **Platform** | Android |
| **Framework** | Xamarin.Forms 5.0 |
| **Hedef Framework** | .NET Standard 2.0 |
| **Minimum Android** | SDK 21 (Lollipop) |
| **Hedef Android** | SDK 33 (Android 13) |
| **Dil** | C# |
| **Mimari** | MVC benzeri yapı |
| **AI Servisleri** | Azure Vision, OpenAI GPT-3.5, Azure TTS |
| **Konum Servisi** | Google Places API |

---

<div align="center">

**Görme engelli bireyler için erişilebilirlik odaklı geliştirilmiştir** ♿

*Bu proje, yapay zeka teknolojilerini erişilebilirlik alanında kullanarak toplumsal fayda sağlamayı amaçlamaktadır.*

</div>
