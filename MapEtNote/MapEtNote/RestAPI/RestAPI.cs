using MapAndNotes.Dtos;
using MonkeyCache.SQLite;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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

                LoginResult result = JsonConvert.DeserializeObject<LoginResult>(SerializeResult);

                //On stocke le refreshToken
                Barrel.Current.Add(key: "accesToken", data: result.AccessToken, expireIn: TimeSpan.FromDays(result.ExpiresIn));
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
    }
}
