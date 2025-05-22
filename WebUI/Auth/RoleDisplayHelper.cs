using System.Collections.Generic;

namespace WebUI.Auth
{
    public static class RoleDisplayHelper
    {
        private static readonly Dictionary<string, string> _roleDisplayNames = new Dictionary<string, string>
        {
            // Araç ile ilgili rol görüntüleme isimleri
            { "Vehicle_Add", "Araç Ekleme" },
            { "Vehicle_Edit", "Araç Düzenleme" },
            { "Vehicle_Remove", "Araç Silme" },
            { "Vehicle_View", "Araçları Görüntüleme" },
            
            // Araç markası ile ilgili rol görüntüleme isimleri
            { "VehicleBrand_Add", "Araç Markası Ekleme" },
            { "VehicleBrand_Edit", "Araç Markası Düzenleme" },
            { "VehicleBrand_Remove", "Araç Markası Silme" },
            { "VehicleBrand_View", "Araç Markalarını Görüntüleme" },
            
            // Araç modeli ile ilgili rol görüntüleme isimleri
            { "VehicleModel_Add", "Araç Modeli Ekleme" },
            { "VehicleModel_Edit", "Araç Modeli Düzenleme" },
            { "VehicleModel_Remove", "Araç Modeli Silme" },
            { "VehicleModel_View", "Araç Modellerini Görüntüleme" },
            
            // Araç tipi ile ilgili rol görüntüleme isimleri
            { "VehicleType_Add", "Araç Tipi Ekleme" },
            { "VehicleType_Edit", "Araç Tipi Düzenleme" },
            { "VehicleType_Remove", "Araç Tipi Silme" },
            { "VehicleType_View", "Araç Tiplerini Görüntüleme" },
            
            // Araç rengi ile ilgili rol görüntüleme isimleri
            { "VehicleColor_Add", "Araç Rengi Ekleme" },
            { "VehicleColor_Edit", "Araç Rengi Düzenleme" },
            { "VehicleColor_Remove", "Araç Rengi Silme" },
            { "VehicleColor_View", "Araç Renklerini Görüntüleme" },
            
            // Araç yılı ile ilgili rol görüntüleme isimleri
            { "VehicleYear_Add", "Araç Yılı Ekleme" },
            { "VehicleYear_Edit", "Araç Yılı Düzenleme" },
            { "VehicleYear_Remove", "Araç Yılı Silme" },
            { "VehicleYear_View", "Araç Yıllarını Görüntüleme" },
            
            // Anahtar konumu ile ilgili rol görüntüleme isimleri
            { "KeyLocation_Add", "Anahtar Konumu Ekleme" },
            { "KeyLocation_Edit", "Anahtar Konumu Düzenleme" },
            { "KeyLocation_Remove", "Anahtar Konumu Silme" },
            { "KeyLocation_View", "Anahtar Konumlarını Görüntüleme" },
            
            // Bağlantı nedeni ile ilgili rol görüntüleme isimleri
            { "LinkingReason_Add", "Bağlantı Nedeni Ekleme" },
            { "LinkingReason_Edit", "Bağlantı Nedeni Düzenleme" },
            { "LinkingReason_Remove", "Bağlantı Nedeni Silme" },
            { "LinkingReason_View", "Bağlantı Nedenlerini Görüntüleme" },
            
            // Bağlantı bölgesi ile ilgili rol görüntüleme isimleri
            { "LinkingRegion_Add", "Bağlantı Bölgesi Ekleme" },
            { "LinkingRegion_Edit", "Bağlantı Bölgesi Düzenleme" },
            { "LinkingRegion_Remove", "Bağlantı Bölgesi Silme" },
            { "LinkingRegion_View", "Bağlantı Bölgelerini Görüntüleme" },
            
            // Şube ile ilgili rol görüntüleme isimleri
            { "Branch_Add", "Şube Ekleme" },
            { "Branch_Edit", "Şube Düzenleme" },
            { "Branch_Remove", "Şube Silme" },
            { "Branch_View", "Şubeleri Görüntüleme" },
            
            // Park konumu ile ilgili rol görüntüleme isimleri
            { "ParkLocation_Add", "Park Konumu Ekleme" },
            { "ParkLocation_Edit", "Park Konumu Düzenleme" },
            { "ParkLocation_Remove", "Park Konumu Silme" },
            { "ParkLocation_View", "Park Konumlarını Görüntüleme" },
            
            // Araç resmi ile ilgili rol görüntüleme isimleri
            { "VehicleImage_Add", "Araç Resmi Ekleme" },
            { "VehicleImage_Edit", "Araç Resmi Düzenleme" },
            { "VehicleImage_Remove", "Araç Resmi Silme" },
            { "VehicleImage_View", "Araç Resimlerini Görüntüleme" },
            
            // Belge ile ilgili rol görüntüleme isimleri
            { "Document_Add", "Belge Ekleme" },
            { "Document_Edit", "Belge Düzenleme" },
            { "Document_Remove", "Belge Silme" },
            { "Document_View", "Belgeleri Görüntüleme" },
            
            // Çekme kaydı ile ilgili rol görüntüleme isimleri
            { "EnforcementRecord_Add", "Çekme Kaydı Ekleme" },
            { "EnforcementRecord_Edit", "Çekme Kaydı Düzenleme" },
            { "EnforcementRecord_Remove", "Çekme Kaydı Silme" },
            { "EnforcementRecord_View", "Çekme Kayıtlarını Görüntüleme" },
            
            // Çekme ofisi ile ilgili rol görüntüleme isimleri
            { "EnforcementOffice_Add", "Çekme Ofisi Ekleme" },
            { "EnforcementOffice_Edit", "Çekme Ofisi Düzenleme" },
            { "EnforcementOffice_Remove", "Çekme Ofisi Silme" },
            { "EnforcementOffice_View", "Çekme Ofislerini Görüntüleme" },
            
            // Kullanıcı ile ilgili rol görüntüleme isimleri
            { "User_Add", "Kullanıcı Ekleme" },
            { "User_Edit", "Kullanıcı Düzenleme" },
            { "User_Remove", "Kullanıcı Silme" },
            { "User_View", "Kullanıcıları Görüntüleme" },
            
            // Rol ile ilgili rol görüntüleme isimleri
            { "Role_Add", "Rol Ekleme" },
            { "Role_Edit", "Rol Düzenleme" },
            { "Role_Remove", "Rol Silme" },
            { "Role_View", "Rolleri Görüntüleme" },

            // İşlem ile ilgili rol görüntüleme isimleri
            { "Transaction_Add", "Ödeme Ekleme" },
            { "Transaction_Edit", "Ödeme Düzenleme" },
            { "Transaction_Remove", "Ödeme Silme" },
            { "Transaction_View", "Ödemeleri Görüntüleme" },

            // İşlem kategorisi ile ilgili rol görüntüleme isimleri
            { "TransactionCategory_Add", "Ödeme Kategorisi Ekleme" },
            { "TransactionCategory_Edit", "Ödeme Kategorisi Düzenleme" },
            { "TransactionCategory_Remove", "Ödeme Kategorisi Silme" },
            { "TransactionCategory_View", "Ödeme Kategorilerini Görüntüleme" },
            
            // Admin rolü
            { "Admin", "Sistem Yöneticisi" }
        };

        /// <summary>
        /// Rol adını kullanıcı dostu görüntüleme ismine dönüştürür
        /// </summary>
        /// <param name="roleName">Rol adı</param>
        /// <returns>Kullanıcı dostu görüntüleme ismi</returns>
        public static string GetDisplayName(string roleName)
        {
            return _roleDisplayNames.TryGetValue(roleName, out var displayName) 
                ? displayName 
                : roleName; // Eşleşme yoksa orijinal rol adını dön
        }
    }
}