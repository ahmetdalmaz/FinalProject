using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        //publicler pascal case yazılır
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string ProductsListed = "Ürünler Listelendi";
        public static string MaintenanceTime = "Sistem Bakımda";
        public static string ProductCountOfCategoryError = "Bu kategoride yeterli ürün mevcut";
        internal static string ProductNameAlreadyExists;

        public static string AuthorizationDenied = "";
        internal static string UserRegistered="Kayıt Olundu";
        internal static string UserNotFound = "Kullanıcı bulunmadaı";
        internal static string PasswordError = "şifre hatalı";
        internal static string SuccessfulLogin;
        internal static string UserAlreadyExists;
        internal static string AccessTokenCreated = "token oluşturuldu";
    }
}
