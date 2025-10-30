# UstaPlatform - Şehrin Uzmanlık Platformu

## Proje Özeti

UstaPlatform, Yozgat şehrindeki kayıp uzmanları (Tesisatçı, Elektrikçi, vb.) vatandaş talepleriyle eşleştiren, dinamik fiyatlama ve akıllı rota planlama yapabilen, genişletilebilir ve açık uçlu bir yazılım platformudur.

## Kurulum ve Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Visual Studio 2022 veya VS Code

### Kurulum Adımları

1. **Projeyi klonlayın:**
   ```bash
   git clone [repository-url]
   cd UstaPlatform
   ```

2. **Çözümü derleyin:**
   ```bash
   dotnet build
   ```

3. **Testleri çalıştırın:**
   ```bash
   dotnet test
   ```

4. **Uygulamayı çalıştırın:**
   ```bash
   dotnet run --project UstaPlatform.App
   ```

## Tasarım Kararları

### 1. SOLID Prensipleri Uygulaması

#### Açık/Kapalı Prensibi (OCP)
- **IPricingRule** arayüzü sayesinde yeni fiyatlandırma kuralları ana kodu değiştirmeden eklenebilir
- Plugin mimarisi ile runtime'da yeni kurallar yüklenebilir
- Örnek: `SadakatIndirimiKurali` ayrı DLL olarak eklenebilir

#### Tek Sorumluluk Prensibi (SRP)
- Her sınıf tek bir sorumluluğa sahiptir:
  - `Usta`: Uzman bilgilerini tutar
  - `PricingEngine`: Fiyat hesaplama işlemlerini yönetir
  - `Çizelge`: İş emri takvimini yönetir
  - `Rota`: Durak noktalarını ve mesafe hesaplamalarını yönetir

#### Bağımlılıkların Tersine Çevrilmesi (DIP)
- Üst katmanlar somut sınıflara değil arayüzlere bağımlıdır:
  - `IUstaRepository`, `ITalepRepository`, `IİşEmriRepository`
  - `IPricingRule` arayüzü

### 2. İleri C# Özellikleri

#### Init-Only Properties
```csharp
public class Usta
{
    public int Id { get; init; }
    public string Ad { get; init; } = string.Empty;
    // Nesne oluşturulduktan sonra değiştirilemez
}
```

#### Nesne ve Koleksiyon Başlatıcıları
```csharp
var usta = new Usta
{
    Ad = "Ahmet",
    Soyad = "Yılmaz",
    UzmanlikAlani = "Tesisatçı"
};

var rota = new Rota
{
    UstaId = 1,
    Tarih = DateTime.Now
};
rota.Add(0, 0);   // Koleksiyon başlatıcısı
rota.Add(10, 5);
```

#### Dizinleyici (Indexer)
```csharp
var çizelge = new Çizelge();
var bugününİşleri = çizelge[ustaId, DateOnly.FromDateTime(DateTime.Now)];
var tümUstalarınİşleri = çizelge[DateOnly.FromDateTime(DateTime.Now)];
```

#### Özel IEnumerable<T> Koleksiyonu
```csharp
public class Rota : IEnumerable<(int X, int Y)>
{
    public void Add(int X, int Y) { /* Koleksiyon başlatıcısı desteği */ }
    
    public IEnumerator<(int X, int Y)> GetEnumerator()
    {
        return _durakNoktalari.GetEnumerator();
    }
}
```

#### Static Yardımcı Sınıflar
- `Guard`: Doğrulama işlemleri
- `ParaFormatlayici`: Para formatlaması
- `KonumYardimcisi`: Konum hesaplamaları

### 3. Plug-in (Eklenti) Mimarisi

#### Nasıl Çalışır?
1. **IPricingRule Arayüzü:** Tüm fiyatlandırma kuralları bu arayüzü uygular
2. **PricingEngine:** Uygulama başladığında `Plugins` klasöründeki DLL'leri tarar
3. **Dinamik Yükleme:** Reflection kullanarak `IPricingRule` uygulayan sınıfları bulur ve yükler
4. **Kompozisyon:** Fiyat hesaplaması kuralların ardışık uygulanması ile yapılır

#### Yeni Kural Ekleme
```csharp
// Yeni bir kural sınıfı oluşturun
public class YeniKural : IPricingRule
{
    public string KuralAdi => "Yeni Kural";
    public string Aciklama => "Açıklama";
    
    public decimal FiyatHesapla(decimal temelFiyat, Talep talep, İşEmri işEmri)
    {
        // Fiyat hesaplama mantığı
        return temelFiyat * 1.1m;
    }
    
    public bool UygulanabilirMi(Talep talep, İşEmri işEmri)
    {
        // Kural uygulanabilirlik kontrolü
        return true;
    }
}
```

#### Plugin Olarak Derleme
```bash
# Yeni kuralı ayrı proje olarak derleyin
dotnet build YeniKural.csproj --output Plugins/
```

## Demo Senaryoları

Uygulama çalıştırıldığında aşağıdaki senaryolar otomatik olarak çalışır:

1. **Normal Talep:** Temel fiyatlandırma
2. **Acil Talep:** %50 ek ücret
3. **Hafta Sonu Talep:** %20 ek ücret
4. **Çizelge ve Rota:** Indexer ve IEnumerable kullanımı
5. **Plugin Sistemi:** Dinamik kural yükleme

## Test Kapsamı

Proje xUnit test framework'ü ile test edilmiştir:

- **PricingEngine Tests:** Fiyat hesaplama kuralları
- **Çizelge Tests:** Indexer fonksiyonalitesi
- **Plugin Tests:** Dinamik kural yükleme

Testleri çalıştırmak için:
```bash
dotnet test
```