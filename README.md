# 🏭 Offline Industrial Catalog & Cross-Reference System

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![WPF](https://img.shields.io/badge/WPF-512BD4?style=for-the-badge&logo=windows&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Dapper](https://img.shields.io/badge/Dapper-FE0902?style=for-the-badge)

A high-performance, offline-first desktop application built with **WPF, C#**, and **Dapper**. This system allows industrial users (like factory workers or warehouse managers) to search for product cross-references (OEM/Competitor numbers to Internal Brand numbers) and view detailed technical specifications instantly without requiring an internet connection.

---

## ⚠️ Data Privacy Disclaimer
*This repository contains the fully functional source code and architecture of the application. However, to comply with strict data privacy policies and protect client confidentiality, **all real commercial data, brand names, and product images have been replaced with mock (dummy) data.** The provided SQL script allows you to test the system locally.*

---

## 📸 Screenshots

*(Ekranda uygulamanın ana arama sayfasını gösteren bir görsel ekle)*
![Search Screen](link_to_your_search_screenshot.png)

*(Ekranda ürün detaylarını ve fotoğrafını gösteren bir görsel ekle)*
![Product Detail Screen](link_to_your_detail_screenshot.png)

---

## 🚀 Key Features

* **Lightning-Fast Queries:** Utilizes **Dapper** (Micro-ORM) for zero-latency SQL Server database reads, even with thousands of cross-reference records.
* **100% Offline Capability:** All product images are cached and read from a local directory relative to the executable (`/Fotograflar`). No AWS/S3 or internet latency.
* **Robust Cross-Reference Engine:** Search by competitor numbers to instantly find the equivalent internal product and its exact specifications.
* **Production-Ready Architecture:** Clean separation of concerns with Repository patterns and scalable Data Models.

---

## 🛠️ Tech Stack

* **Frontend:** WPF (Windows Presentation Foundation), XAML
* **Backend:** C# (.NET 8)
* **Database:** Microsoft SQL Server
* **ORM:** Dapper
* **Architecture:** Repository Pattern

---

## ⚙️ Setup & Installation

Follow these steps to run the application locally with the dummy dataset:

1. **Clone the repository:**
   ```bash
   git clone [https://github.com/yourusername/your-repo-name.git](https://github.com/yourusername/your-repo-name.git)
