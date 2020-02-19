using MapAndNotes.Dtos;
using MapEtNote.View;
using MonkeyCache.SQLite;
using Storm.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MapEtNote.ViewModel
{
    class AjoutPlaceModel : ViewModelBase
    {
        private string _title;
        private string _description;
        private int _imageID;

        public string Title
        {
            get { return _title; }
            set { SetProperty<string>(ref _title, value); }
        }
        public string Description
        {
            get { return _description; }
            set { SetProperty<string>(ref _description, value); }
        }

        public ICommand PosterCommand { get; set; }

        public AjoutPlaceModel() 
        {
            _imageID = 1;
            PosterCommand = new Command(Poster);
            Barrel.ApplicationId = "coucou";
        }

        private async void Poster() 
        {
            //On vérifie que l'entrée est correcte
            if (string.IsNullOrEmpty(_title))
            {
                await Application.Current.MainPage.DisplayAlert("Alerte", "Les infos rentrées ne sont pas valides", "OK");
            }
            else 
            {
                CreatePlaceRequest cpr = new CreatePlaceRequest() {
                    Title = _title,
                    Description = _description,
                    ImageId = _imageID,
                    Latitude = 17.828292,
                    Longitude = 88.901
                };

                if (await RestAPI.RestAPI.PostRequest(cpr))
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new ListePlaceView());
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Envoi réussi !", "OK");
                }
                else 
                {
                    await Application.Current.MainPage.DisplayAlert("Alerte", "Il y a une erreur quelque part, amusez vous à la trouver.", "OK");
                }
            }
        }
    }
}
