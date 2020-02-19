using System;
using System.Windows.Input;
using Xamarin.Forms;
using Storm.Mvvm;
using Newtonsoft.Json;
using MapAndNotes.Dtos;
using MonkeyCache.SQLite;
using MapEtNote.RestAPI;
using MapEtNote;
using MapEtNote.View;

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

        public ICommand SubmitRegister { get; set; }

        public LoginModel()
        {
            SubmitCommand = new Command(OnSubmit);
            SubmitRegister = new Command(LaunchRegisterAsync);
            Barrel.ApplicationId = "coucou";
        }

        private async void LaunchRegisterAsync() 
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new RegisterView());
        }

        private async void OnSubmit()
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
                    //await Application.Current.MainPage.DisplayAlert("Alerte", "Login correct", "OK");
                    //await Application.Current.MainPage.DisplayAlert("Alerte", "Bonjour " + Barrel.Current.Get<string>(key: "refreshToken"), "OK");

                    await App.Current.MainPage.Navigation.PushModalAsync(new ListePlaceView());
                }
                else 
                {
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Login incorrect", "OK");
                }
            }
        }
    }
}
