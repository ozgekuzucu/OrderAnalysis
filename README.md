# 📊 OrderAnalysis API

OrderAnalysis API, e-ticaret sipariş verilerini analiz ederek finansal performans ve satış risklerini hesaplayan bir backend servisidir. Bu proje bir backend challenge kapsamında geliştirilmiştir.

Bu API sayesinde:

- ✅ Gerçek net kâr hesaplanır  
- ✅ Zararda olan ürünler tespit edilir  
- ✅ Ürün bazlı risk skoru oluşturulur  
- ✅ Anormal fiyat hareketleri yakalanır  
- ✅ Platform bazlı performans karşılaştırılır  
- ✅ Günlük ciro ve kâr trendi analiz edilir  

Proje Clean Architecture prensipleri uygulanarak geliştirilmiştir.

---

## 🚀 Teknolojiler

- .NET 8
- MongoDB
- AutoMapper
- Swagger

---

## 🏗️ Proje Yapısı

```
OrderAnalysis/
├── OrderAnalysis.API/            # Controller'lar, Program.cs
├── OrderAnalysis.Application/    # Servisler, DTO'lar, Interface'ler
├── OrderAnalysis.Domain/         # Entity'ler
└── OrderAnalysis.Infrastructure/ # MongoDB, Repository'ler
```

---


## 🛡️ Validation

`POST /api/orders` endpoint’i için .NET `DataAnnotations` kullanılarak model bazlı doğrulama (validation) uygulanmıştır.

Validation işlemleri:

- `CreateOrderDto`
- `CreateOrderItemDto`

sınıfları üzerinde tanımlanmıştır.

### 📌 Order Seviyesi Kurallar

- `Platform` zorunludur.
- `Tarih` zorunludur.
- `Items` zorunludur.
- `Items` listesi en az 1 eleman içermelidir.

### 📌 Ürün (Item) Seviyesi Kurallar

- `Urun` zorunludur.
- `AlisFiyat` 0'dan büyük olmalıdır.
- `SatisFiyat` 0'dan büyük olmalıdır.
- `KomisyonOrani` 0–100 arasında olmalıdır.
- `KargoBedeli` 0 veya 0'dan büyük olmalıdır.
- `Adet` en az 1 olmalıdır.

### ❗ Hatalı İstek Durumu

Geçersiz veri gönderildiğinde API:

- `400 Bad Request` döner
- ModelState üzerinden detaylı hata mesajları üretir

Bu yapı sayesinde hatalı sipariş kaydı engellenir ve veri bütünlüğü korunur.

---

## 🌐 Global Hata Yakalama (Global Exception Handling)

Uygulamada oluşan tüm beklenmeyen hataları merkezi olarak yakalamak için bir **global exception middleware** uygulanmıştır.

---

## 📡 Endpoint'ler ve Formüller

| Endpoint | Açıklama |
|----------|----------|
| `POST /api/orders` | Sipariş Ekleme |
| `GET /api/report/summary` | Genel özet (toplam sipariş, ciro, net kar) |
| `GET /api/report/platform` | Platform bazlı ciro, kar ve marj analizi |
| `GET /api/report/loss` | Zararda olan ürünler |
| `GET /api/report/anomaly` | Anormal fiyat tespiti |
| `GET /api/report/trend` | Günlük ciro ve kar trendi |
| `GET /api/report/risk` | Ürün bazlı risk analizi |

### 1. Sipariş Ekleme
```
POST /api/orders
```
```json
{
  "platform": "Trendyol",
  "tarih": "2026-02-27",
  "items": [
    {
      "urun": "A",
      "alisFiyat": 50,
      "satisFiyat": 80,
      "komisyonOrani": 15,
      "kargoBedeli": 5,
      "adet": 2
    }
  ]
}
```
```
Komisyon = Satış Fiyatı * Komisyon Oranı / 100
Net Kar  = (Satış Fiyatı - Alış Fiyatı - Kargo - Komisyon) * Adet
```

---

### 2. Genel Özet
```
GET /api/report/summary
```
```
Toplam Ciro    = Σ (Satış Fiyatı * Adet)
Toplam Net Kar = Σ Net Kar
```

---

### 3. Platform Bazlı Rapor
```
GET /api/report/platform
```
```
Platform Ciro    = Σ (Satış Fiyatı * Adet) — platforma göre gruplu
Platform Net Kar = Σ Net Kar — platforma göre gruplu
Kar Marjı        = Net Kar / Ciro * 100
```

---

### 4. Zararda Olan Ürünler
```
GET /api/report/loss
```
```
Net Kar < 0 olan ürünler listelenir
Toplam Zarar = |Net Kar|
```

---

### 5. Anormal Fiyat Tespiti
```
GET /api/report/anomaly
```
```
Ortalama Satış Fiyatı = Σ Satış Fiyatı / Kayıt Sayısı
Sapma Yüzde           = (Mevcut Fiyat - Ortalama) / Ortalama * 100
```

---

### 6. Günlük Trend
```
GET /api/report/trend
```
```
Günlük Ciro    = Σ (Satış Fiyatı * Adet) — güne göre gruplu
Günlük Net Kar = Σ Net Kar — güne göre gruplu
Değişim Yüzde  = (Bugün - Dün) / Dün * 100
```

---

### 7. Risk Analizi
```
GET /api/report/risk
```
```
Kar Marjı  = Net Kar / Ciro * 100
Risk Skoru = 100 - Kar Marjı

Risk Seviyesi:
  >= 90 → Yuksek
  >= 70 → Orta
  <  70 → Dusuk
```
