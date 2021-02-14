﻿using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EkranFavoriti : ContentPage
    {
        public List<Classes.Publikacije> listaSvihPublikacija;
        public EkranFavoriti()
        {
            InitializeComponent();
            BindingContext = this;
            listaSvihPublikacija = Classes.Clanovi.listaFavorita;
            ListaPublikacije.ItemsSource = listaSvihPublikacija;

            MessagingCenter.Subscribe<App>((App)Application.Current, "osvjeziFavorite", (sender) => { OsvjeziFavorite(); });
        }

        private void OsvjeziFavorite()
        {
            ListaPublikacije.ItemsSource = null;
            listaSvihPublikacija = Classes.Clanovi.listaFavorita;
            ListaPublikacije.ItemsSource = listaSvihPublikacija;
        }

        public async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Classes.Publikacije tappedItem = e.Item as Classes.Publikacije;
            await Navigation.PushAsync(new BookInfo(tappedItem));
        }

        public async void PomocKliknuta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.PomocPopUpPage("DOBRODOŠLI U POMOĆ ZA PREGLED FAVORITA!", "Prikazan Vam je uvid u sve vaše omiljene knjige koje ste dodali u favorite. Odabirom željene knjige dobijate detaljne informacije i istu možete maknuti iz rezervacija ili nastaviti ka posudbi ili rezervaciji ukoliko ima dostupnih kopija."));
        }

        protected override bool OnBackButtonPressed()
        {
            System.Environment.Exit(0);
            return false;
        }

    }
}