# Görme Engelli Uygulama

Görme Engelli Uygulama, görme engelli bireylerin günlük yaşamlarını kolaylaştırmak amacıyla geliştirilmiş bir mobil uygulamadır. Bu uygulama, çeşitli API'ler kullanarak kullanıcılarına yardımcı olmayı hedefler.

## Kurulum

Görme Engelli Uygulama'yı kurmak ve çalıştırmak için aşağıdaki adımları izleyin:

1. Bu depoyu klonlayın:
    ```bash
    git clone https://github.com/kadirbeskardes/GormeEngelliUygulama.git
    ```
2. Projeyi Visual Studio'da açın.
3. Gerekli bağımlılıkları yükleyin
4. Proje de kullanılan servislerin (OpenAIClient, Google Cloud Places API, Azure TTS Service API, Azure AI Vision API) erişim için kullanılan anahtarlarını kod üzerinde gerekli yerlere yazın.
5. Log e-postalarını gönderebilmek için Logger sınıfı içerisinde gönderici mail, alıcı mail ve Gmail app password kısımlarını doldurunuz.
6. Projeyi derleyin ve çalıştırın.

