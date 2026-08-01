# Ticketora

Ticketora, etkinlikleri listelemek, filtrelemek ve kullanıcıların dijital bilet oluşturmasını sağlamak için geliştirilmiş bir ASP.NET Core MVC uygulamasıdır. Projede kullanıcı kayıt/giriş işlemleri, rol bazlı admin paneli, etkinlik ve kategori yönetimi, katılımcı takibi ve bilet satın alma akışı yer alır.

## Özellikler

- Etkinlik listeleme, detay görüntüleme ve filtreleme
- Kategori, şehir/lokasyon ve tarih bazlı etkinlik arama
- Kullanıcı kayıt, giriş ve çıkış işlemleri
- ASP.NET Core Identity ile kimlik doğrulama
- Admin rolü için etkinlik, kategori ve katılımcı yönetimi
- Dijital bilet oluşturma ve kullanıcıya ait bilet detaylarını görüntüleme
- Benzersiz bilet numarası üretimi
- Aktif/yaklaşan etkinlik istatistikleri

## Kullanılan Teknolojiler

- **.NET 9**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server**
- **ASP.NET Core Identity**
- **MediatR**
- **CQRS Design Pattern**
- **N-Tier Architecture**
- **Bootstrap**
- **jQuery**

## Proje Yapısı

```text
Ticketora
|-- Core
|   |-- Ticketora.Domain
|   `-- Ticketora.Application
|-- Infrastructure
|   |-- Ticketora.Infrastructure
|   `-- Ticketora.Persistence
|-- Presentation
|   `-- Ticketora.WebUI
`-- Ticketora.sln
```

- **Domain:** Event, Category, Participant ve Ticket gibi temel entity sınıflarını içerir.
- **Application:** CQRS ve MediatR tabanlı command, query ve handler katmanlarını içerir.
- **Persistence:** Entity Framework Core DbContext, Identity kullanıcı modeli ve migration dosyalarını içerir.
- **WebUI:** MVC controller, Razor view, view model ve statik dosyaları içerir.

## Ekran Görüntüleri

## Ana Sayfa

<img width="1848" height="885" alt="home" src="https://github.com/user-attachments/assets/f2ebf49c-504c-4a7f-bfc0-b0bf7c0c150c" />

### Ana Sayfa Etkinlik Kategorilere Göre Gösterim
<img width="1849" height="881" alt="home2" src="https://github.com/user-attachments/assets/9b816e77-664f-459d-b591-58bb90d9adad" />

## Etkinlikler

### [Etkinlik Listesi]
<img width="1756" height="879" alt="eventlist" src="https://github.com/user-attachments/assets/ddffed04-9633-4a08-a6ab-2b30dca0e4ed" />

### [Etkinlik Filtreleme]
<img width="1846" height="872" alt="eventlist filtreleme" src="https://github.com/user-attachments/assets/b93ccb8c-d2be-4ba8-abe2-133427a0c1a9" />

### [Geçmiş Etkinlikler]
<img width="1300" height="798" alt="event geçmiş tarihtekiler" src="https://github.com/user-attachments/assets/4b048bfc-0543-42cd-8927-711c20be84dd" />

### [Etkinlik Kayıt]
<img width="1374" height="879" alt="event sec" src="https://github.com/user-attachments/assets/744bd212-4525-42b8-a11b-ddc4df39b0cc" />


## Bilet Görünümü

### [Dijital Bilet]
<img width="1650" height="806" alt="ticket" src="https://github.com/user-attachments/assets/f20a50ae-c42c-4060-bd2c-83b0b13f8acc" />

## Kullanıcı İşlemleri

### [Giriş Sayfası]
<img width="1359" height="880" alt="kayıt ol" src="https://github.com/user-attachments/assets/05597e4c-a909-4017-9e95-4a61b14aea18" />

### [Kayıt Sayfası]
<img width="1389" height="858" alt="login" src="https://github.com/user-attachments/assets/755a4ec8-0045-4a55-9141-af2a1d3ce559" />


## Admin Paneli

### [Admin Dashboard]
<img width="1419" height="879" alt="admin" src="https://github.com/user-attachments/assets/1ae3b094-78b0-499b-b957-afb612829772" />

### [Admin Etkinlik Yönetimi]
<img width="1345" height="853" alt="admin event" src="https://github.com/user-attachments/assets/06361cb6-b902-45b2-b5f3-9b3f029841be" />

### [Admin Kategori Yönetimi]
<img width="1453" height="884" alt="admin category" src="https://github.com/user-attachments/assets/41888080-ba02-4304-894a-011258f1cdca" />


## Footer

<img width="1377" height="238" alt="footer" src="https://github.com/user-attachments/assets/01107526-1c19-479c-b228-32d8c51c3236" />


