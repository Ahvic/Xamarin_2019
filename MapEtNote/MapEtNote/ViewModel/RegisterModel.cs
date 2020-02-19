using MapAndNotes.Dtos;
using MapEtNotes.View;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapEtNote.ViewModel
{
    class RegisterModel : ViewModelBase
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private string _password;

        public string Email
        {
            get { return _email; }
            set { SetProperty<string>(ref _email, value); }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty<string>(ref _firstName, value); }
        }

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty<string>(ref _lastName, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

        public ICommand SubmitCommand { get; set; }

        public RegisterModel() 
        {
            SubmitCommand = new Command(Enregistrer);
        }

        private async void Enregistrer() 
        {
            //On n'accepte pas d'avoir un champ vide
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_firstName) || string.IsNullOrEmpty(_lastName) || string.IsNullOrEmpty(_password))
            {
                await Application.Current.MainPage.DisplayAlert("Alerte", "Aucun champ ne peut être vide", "OK");
            }
            else 
            {
                RegisterRequest rr = new RegisterRequest() {
                    Email = _email,
                    FirstName = _firstName,
                    LastName = _lastName,
                    Password = _password
                };

                if (await RestAPI.RestAPI.RegisterRequest(rr))
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new LoginView());
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Enregistrement réussi !", "OK");
                }
                else 
                {
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Il y a une erreur.", "OK");
                }
            }
        }
    }
}
