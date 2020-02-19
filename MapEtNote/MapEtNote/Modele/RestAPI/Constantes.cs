using System;
using System.Collections.Generic;
using System.Text;

namespace MapEtNote.RestAPI
{
    public class Constantes
    {
        private static readonly string baseURL = "https://td-api.julienmialon.com";

        //API USER
        private static readonly string register = baseURL + "/auth/register";
        private static readonly string login = baseURL + "/auth/login";
        private static readonly string refresh = baseURL + "/auth/refresh";
        private static readonly string userInfos = baseURL + "/me";
        private static readonly string editPassword = userInfos + "/password";

        //API PLACE
        private static readonly string image = baseURL + "/images";
        private static readonly string places = baseURL + "/places";

        public static string BaseUrl
        {
            get => baseURL;
        }

        public static string RegisterUrl
        {
            get => register;
        }

        public static string LoginUrl
        {
            get => login;
        }

        public static string RefreshTokenUrl
        {
            get => refresh;
        }

        public static string UserInfosUrl
        {
            get => userInfos;
        }

        public static string EditPasswordUrl
        {
            get => editPassword;
        }

        public static string ImageUrl
        {
            get => image;
        }

        public static string PlacesUrl
        {
            get => places;
        }
    }
}
