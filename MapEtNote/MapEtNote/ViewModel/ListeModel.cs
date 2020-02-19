using MapAndNotes.Dtos;
using Storm.Mvvm;
using MapEtNote.RestAPI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using MapEtNote.View;

namespace MapEtNote.Models
{
    class ListeModel : ViewModelBase
    {
        private ObservableCollection<PlaceItemSummary> _places;

        public ObservableCollection<PlaceItemSummary> Places
        {
            get { return _places; }
            set { SetProperty<ObservableCollection<PlaceItemSummary>>(ref _places, value); }
        }

        public ICommand AjouterCommand { get; set; }

        public ICommand RefreshCommand { get; set; }

        public ListeModel()
        {

            AjouterCommand = new Command(Ajouter);
            RefreshCommand = new Command(GetPlacesAsync);

            Places = new ObservableCollection<PlaceItemSummary>();
            GetPlacesAsync();
        }

        public void Ajouter()
        {
            App.Current.MainPage.Navigation.PushModalAsync(new AjouterPlaceView());
        }

        public async void GetPlacesAsync() 
        {
            List<PlaceItemSummary> temp = await RestAPI.RestAPI.GetPlaceRequestAsync();

            //Ajoute element a _places
            foreach (var item in temp) 
            {
                Places.Add(item);
            }
        }
    }
}
