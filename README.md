# Tubes1_JaMin - Implementasi Bot Robocode Tank Royale


## Deskripsi

Repositori ini berisi implementasi bot tank otonom untuk platform Robocode Tank Royale menggunakan strategi Algoritma Greedy. Proyek ini dikembangkan sebagai Tugas Besar 1 untuk mata kuliah Strategi Algoritma (IF2211) di Institut Teknologi Bandung.

**Link Spesifikasi:** [bit.ly/Spek-Tubes-1-Stima-25](http://bit.ly/Spek-Tubes-1-Stima-25)

## Strategi Bot

Kami telah mengimplementasikan empat strategi bot berbeda menggunakan berbagai heuristik greedy:

1. **MajuMundurLock (Main Bot)** - Greedy by Movement and Shooting
   - Mempertahankan gerakan maju-mundur konstan untuk menghindari tembakan
   - Menerapkan penguncian target untuk penembakan efisien
   - Menghemat energi ketika di bawah ambang kritis

2. **NabrakBelok (Alternative Bot)** - Greedy by Finding the Wall for Safety Place
   - Memanfaatkan dinding sebagai titik strategis untuk keamanan
   - Bergerak dengan pola maju dan berbelok setelah menabrak dinding
   - Menyerang berdasarkan perhitungan jarak optimal

3. **SeptikTank (Alternative Bot)** - Greedy by Target Vulnerability
   - Menganalisis level energi musuh untuk menentukan strategi serangan
   - Menabrak musuh dengan energi rendah
   - Mempertahankan jarak tembak optimal dari musuh yang lebih kuat

4. **ZigiZaga (Alternative Bot)** - Greedy by ZigZag Movement
   - Menggunakan pola zigzag yang tidak terprediksi untuk menghindari tembakan musuh
   - Menyesuaikan sudut dan jarak gerakan secara dinamis
   - Mendeteksi tembakan musuh melalui pemantauan energi

Analisis dan implementasi detail dari strategi-strategi ini dapat ditemukan dalam laporan proyek kami.

## Cara Menjalankan

### Prasyarat
- .NET SDK  9.0 atau yang lebih baru
- Robocode Royale GUI

### Instalasi

1 **Clone repositori ini (atau download release)**:
```bash
git clone https://github.com/rafifrs/Tubes1_JaMin.git
```
2 **Unduh file jar Robocode Tank Royale**
   https://github.com/Ariel-HS/tubes1-if2211-starter-pack/releases/tag/v1.0
   
3 **Jalankan Robocode Tank Royale**
   ```bash
   java -jar robocode-tankroyale-gui-0.30.0.jar
   ```
4. Ikuti intruksi cara menjalankan permainan melalui tautan sebagai berikut
   [Get Started With Robocode](https://docs.google.com/document/d/12upAKLU9E7tS6-xMUpJZ8gA1L76YngZNCc70AaFgyMY/edit?usp=)
   
## Authors
1. [@Dzaky Aurelia Fawwaz(13523065)](https://github.com/WwzFwz)
2. [@Rafif Farras (13523095)](https://github.com/rafifrs)
3. [@Ahmad Wicaksono (13523121)](https://github.com/sonix03)
   
## Referensi
- [Robocode Tank Royale](https://github.com/robocode-dev/tank-royale) - Engine asli **sebelum** di modifikasi asisten.
- [Robocode Tank Royale Docs](https://robocode-dev.github.io/tank-royale/) - Dokumentasi lengkap mengenai tank royale.
- [C# .Net](https://dotnet.microsoft.com/en-us/) - Website resmi untuk .Net dari Microsoft.
- [Learn to code C#](https://dotnet.microsoft.com/en-us/learntocode) - Tutorial bahasa C# dari situs resmi Microsoft yang cukup friendly.
- [C# language documentation](https://learn.microsoft.com/en-us/dotnet/csharp/) - Dokumentasi bahasa C# dari Microsoft.
- [What is an API?](https://aws.amazon.com/what-is/api/) - Penjelasan apa itu API bagi yang memerlukan.
