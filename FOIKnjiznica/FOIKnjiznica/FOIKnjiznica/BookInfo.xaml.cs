﻿using FOIKnjiznica.Classes;
using Newtonsoft.Json;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using FOIKnjiznica.PopUpPages;
using System.Net.Http.Headers;

namespace FOIKnjiznica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookInfo : ContentPage
    {
        public static List<Classes.Publikacije> listaSvihPublikacija;
        Publikacije publikacijeD;

        bool jeFavorit = false;
        bool prvaProvjera = true;
        public string slikaFavorita { get; private set; }
        public BookInfo(Publikacije publikacijeU)
        {
            publikacijeD = publikacijeU;

            InitializeComponent();
            this.Disappearing += PosaljiPorukuOsvjezavanja;

            ProvjeriJeLiFavorit();

            Naziv.Text = publikacijeD.naziv;
            Image.Source = publikacijeD.slika_url;
            Autor.Text = "Autor: " + publikacijeD.Autor;
            Isbn.Text = "ISBN: " + publikacijeD.isbn;
            Udk.Text = "UDK: " + publikacijeD.udk;
            Signatura.Text = "Signatura: " + publikacijeD.signatura;
            Jezik.Text = "Jezik: " + publikacijeD.jezik;
            Stranice.Text = "Stranice: " + publikacijeD.stranice.ToString();
            Godina.Text = "Godina izdanja: " + publikacijeD.godina_izdanja.ToString();
            Izdanje.Text = "Izdanje: " + publikacijeD.izdanje;
            Izdavac.Text = "Izdavac: " + publikacijeD.Izdavac;

            DohvatiPublikaciju(publikacijeD.id);
            MessagingCenter.Subscribe<App>((App)Application.Current, "RezervacijaPublikacije", (sender) => { OsvjeziListuPublikacija(); });
        }

        private void OsvjeziListuPublikacija()
        {
            DohvatiPublikaciju(publikacijeD.id);
        }
        private async void DohvatiPublikaciju(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(WebServisInfo.PutanjaWebServisa + "Publikacije/"+id);
            var publikacije = JsonConvert.DeserializeObject<List<Classes.Publikacije>>(response);
            listaSvihPublikacija = publikacije;
            ListaPublikacije.ItemsSource = listaSvihPublikacija;
        }

        public async void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Classes.Publikacije tappedItem = e.Item as Classes.Publikacije;
            await PopupNavigation.PushAsync(new RezerviranjePopupPage(tappedItem));
        }

        private async void ButtonSadrzaj(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new Sadrzaj(publikacijeD));
        }

        private async void ProvjeriJeLiFavorit()
        {
            jeFavorit = Classes.Clanovi.listaFavorita.Any(x => x.id == publikacijeD.id);

            if (jeFavorit)
            {
                slikaFavorita = "jeFavorit.png";
                ZvijezdaFavorita.Source = slikaFavorita;

                if (prvaProvjera==false)
                {
                    CrossToastPopUp.Current.ShowCustomToast($"Uspješno ste dodali {publikacijeD.naziv} u favorite", "#ae2323","White");

                    var httpClient = new HttpClient();
                    var Json = JsonConvert.SerializeObject(new Je_Favorit() { PublikacijeId = publikacijeD.id, ClanoviId = Classes.Clanovi.id, pomocno = "null" });
                    var content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "Favoriti", content);
                }

                prvaProvjera = false ;

            }
            else
            {

                slikaFavorita = "nijeFavorit.png";
                ZvijezdaFavorita.Source = slikaFavorita;

                if (prvaProvjera==false)
                {
                    CrossToastPopUp.Current.ShowCustomToast($"Uspješno ste izbrisali {publikacijeD.naziv} iz favorita", "#ae2323", "White");

                    var httpClient = new HttpClient();
                    var Json = JsonConvert.SerializeObject(new Je_Favorit() { PublikacijeId = publikacijeD.id, ClanoviId = Classes.Clanovi.id, pomocno = "null" });
                    var content = new StringContent(Json, Encoding.UTF8, "application/json");
                    var odgovor = await httpClient.PostAsync(WebServisInfo.PutanjaWebServisa + "Favoriti", content);
                }

                prvaProvjera = false;               
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            jeFavorit = Classes.Clanovi.listaFavorita.Any(x => x.id == publikacijeD.id);

            if (jeFavorit)
            {
                Classes.Clanovi.listaFavorita.RemoveAll(x => x.id == publikacijeD.id);
                ProvjeriJeLiFavorit();
            }
            else
            {
                Classes.Clanovi.listaFavorita.Add(publikacijeD);
                ProvjeriJeLiFavorit();
            }
        }

        private void PosaljiPorukuOsvjezavanja(object sender, EventArgs e)
        {
            MessagingCenter.Send<App>((App)Application.Current, "osvjeziFavorite");
        }

        public async void PomocKliknuta(object sender, EventArgs e)
        {
            await PopupNavigation.PushAsync(new PopUpPages.PomocPopUpPage("DOBRODOŠLI U POMOĆ ZA PREGLED DETALJNIH INFORMACIJA KNJIGE!", "Prikazane su Vam osnovne informacije o svakoj knjigi. Imate mogućnost pregledati sadržaj knjige ili dodati knjigu u listu favorita gdje ćete joj lako pristupiti. Odabirom jedne od kopija knjige ponuđenih u dnu možete rezervirati ili posuditi knjigu ukoliko je ona slobodna ili dobiti uvod u rezervaciju ili posudbu ako je zauzeta."));
        }
    }
}