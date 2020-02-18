using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using System.Net.Http;
using System.Net.Http.Headers;
using Xamarin.Forms;
using Storm.Mvvm;
using Newtonsoft.Json;
using MapAndNotes.Dtos;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using MapEtNote;
using MonkeyCache.SQLite;
using MapEtNote.RestAPI;

namespace MapEtNotes.Models
{
    public class LoginModel : ViewModelBase
    {
        private string _username;
        private string _password;
        public string Username
        {
            get { return _username; }
            set { SetProperty<string>(ref _username, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

        public ICommand SubmitCommand { get; set; }

        public LoginModel()
        {
            SubmitCommand = new Command(OnSubmit);
            Barrel.ApplicationId = "coucou";
        }

        public async void OnSubmit()
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Alerte", "Username ou mot de passe vide", "OK");
            }
            else
            {
                LoginRequest user = new LoginRequest() {
                    Email = Username,
                    Password = Password
                };

                if (await RestAPI.LoginRequest(user))
                {
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Login correct", "OK");

                    await Application.Current.MainPage.DisplayAlert("Alerte", "Bonjour " + Barrel.Current.Get<string>(key: "refreshToken"), "OK");

                    //Ouverture fenêtre
                }
                else 
                {
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Login incorrect", "OK");
                }
            }
        }
    }
}
