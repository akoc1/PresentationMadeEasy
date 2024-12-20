# Presentation Made Easy

Presentation Made Easy, kullanıcıların sunum yaparken kolayca bölümler arasında gezinmesine olanak tanıyan bir C# ve WinUI tabanlı bir uygulamadır. Bu proje, özellikle eğitim veya iş sunumları için bölümleri sırayla görüntülemeyi ve kontrol etmeyi kolaylaştırmayı amaçlamaktadır. PowerPoint'in notlar özelliği var fakat yine de böyle bir uygulama yapmak istedim.

## Kullanım

- **Bölüm Ekleyin**: Sağ üst köşedeki "+" simgesine tıklayarak yeni bir bölüm ekleyin. Bölüm adını ve açıklamasını girin.
- **Düzenleme ve Silme**: Bölümleri düzenlemek için kalem simgesine, silmek için çöp kutusu simgesine tıklayın.
- **Sunuma Başlayın**: "Sunum Yardımcısını Başlat" butonuna tıklayarak sunum moduna geçiş yapın. Fare kontrollerini kullanarak bölümler arasında geçiş yapın.

## Özellikler

- **Bölüm Ekleyebilme**: Kullanıcılar istedikleri kadar bölüm ekleyebilir.
- **Bölümler Arası Geçiş**: Eklenen bölümler arasında sırayla veya doğrudan geçiş yapabilme.
- **Özelleştirilmiş Metinler**: Bölümler için açıklamalar ve işlevsel bilgiler eklenebilir.
- **Fare Etkileşimi**: [GlobalMouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook) kütüphanesi ile fare girişleri üzerinden sunum kontrolü sağlanır.
- **Basit ve Kullanıcı Dostu Arayüz**: Minimalist ve modern bir tasarım, kullanıcı deneyimini iyileştirir.

## Teknolojiler

Bu proje aşağıdaki teknolojiler ve kütüphaneler kullanılarak geliştirilmiştir:

- **.Net 8**, **C#**
- **WinUI 3**
- **[GlobalMouseKeyHook](https://github.com/gmamaladze/globalmousekeyhook)** - Fare girişlerini dinlemek için kullanılmıştır.

## Ekran Görüntüleri

### Ana Ekran

![pme3](https://github.com/user-attachments/assets/0cf6a1df-7f7b-4636-bd7c-706957d7733d)

### Sunum Yardımcısı Ekranı

![pme2](https://github.com/user-attachments/assets/a3744443-999d-4c87-ac97-c64228d7f75c)

## Kurulum

Bu projeyi kendi bilgisayarınızda çalıştırmak için hazır build'i alınmış versiyonu **[indirip](https://github.com/akoc1/PresentationMadeEasy/releases)** (.Net Runtime yüklü olmalı) kullanabilirsiniz veyahut kaynak kodu derleyip çalıştırmak istiyorsanız aşağıdaki adımları takip edebilirsiniz.

1. **Projeyi Klonlayın**: 
   ```bash
   git clone https://github.com/akoc1/PresentationMadeEasy.git
   
2. **Gerekli Bağımlılıkları Yükleyin**:
   WinUI 3 ve .NET 8 veya daha üstü bir sürümü sisteminizde yüklü olmalıdır.

   GlobalMouseKeyHook kütüphanesini yüklemek için NuGet Paket Yöneticisi'ni kullanın:
   ```bash
   Install-Package Gma.System.MouseKeyHook

3. **Projeyi Çalıştırın**:
   Visual Studio ile açın ve Build işlemini gerçekleştirin.
   Ardından projeyi çalıştırarak uygulamayı deneyimleyebilirsiniz.

## Katkıda Bulunma

1. Bu projeyi forklayarak veya pull request göndererek katkıda bulunabilirsiniz.
2. Yeni özellikler ekleyin, hataları düzeltin veya iyileştirmeler yapın.

## Lisans
Bu proje MIT Lisansı ile lisanslanmıştır.
