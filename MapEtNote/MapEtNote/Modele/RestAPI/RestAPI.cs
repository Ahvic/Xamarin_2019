using MapAndNotes.Dtos;
using MapAndNotes.Models;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MapEtNote.RestAPI
{
    public static class RestAPI
    {
        public static async Task<bool> LoginRequest(LoginRequest data)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Constantes.LoginUrl);
            request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                //On convertie la réponse de l'api en objet utilisable
                JObject reponseComplete = JObject.Parse(await response.Content.ReadAsStringAsync());
                string SerializeResult = reponseComplete.Property("data").ToString().Substring(7);

                Console.WriteLine("\n \n" + SerializeResult + "\n \n");

                LoginResult result = JsonConvert.DeserializeObject<LoginResult>(SerializeResult);

                //On stocke le refreshToken
                Barrel.Current.Add(key: "accessToken", data: result.AccessToken, expireIn: TimeSpan.FromDays(result.ExpiresIn));
                Barrel.Current.Add(key: "refreshToken", data: result.RefreshToken, expireIn: TimeSpan.FromDays(result.ExpiresIn));

                //await Application.Current.MainPage.DisplayAlert("Alerte", "Connecté ! " + result.ToString(), "OK");

                return true;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("réponse login perdu");
                return false;
            }
        }

        public static async Task<List<PlaceItemSummary>> GetPlaceRequestAsync()
        {
            List<PlaceItemSummary> resultat = new List<PlaceItemSummary>();

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Constantes.PlacesUrl);
            HttpResponseMessage response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode) 
            {
                //On convertie le résultat en json
                JObject reponseComplete = JObject.Parse(await response.Content.ReadAsStringAsync());
                string SerializeResult = reponseComplete.Property("data").ToString().Substring(7);

                resultat = JsonConvert.DeserializeObject<List<PlaceItemSummary>>(SerializeResult);
            }

            return resultat;
        }

        public static async Task<bool> PostRequest(CreatePlaceRequest data)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    data.Latitude = location.Latitude;
                    data.Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            string token = Barrel.Current.Get<string>(key: "accessToken");
            string body = JsonConvert.SerializeObject(data);

            ApiClient api = new ApiClient();

            HttpResponseMessage response = await api.Execute(HttpMethod.Post, Constantes.PlacesUrl, data, token);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                JObject reponseComplete = JObject.Parse(await response.Content.ReadAsStringAsync());

                Console.WriteLine("\n \n" + "Envoi raté: " + reponseComplete + "\n \n");
                return false;
            }
        }

        public static async Task<bool> RegisterRequest(RegisterRequest data) 
        {
            ApiClient api = new ApiClient();

            HttpResponseMessage response = await api.Execute(HttpMethod.Post, Constantes.RegisterUrl, data, null);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                JObject reponseComplete = JObject.Parse(await response.Content.ReadAsStringAsync());

                Console.WriteLine("\n \n" + "Envoi raté: " + reponseComplete + "\n \n");
                return false;
            }
        }
    }
}
