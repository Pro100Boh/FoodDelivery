﻿using FoodDeliveryMobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodDeliveryMobileApp
{
    public partial class App : Application
    {
        public App(string serverApiAdress)
        {
            Properties["serverApiAdress"] = serverApiAdress;

            InitializeComponent();
            MainPage = new TabbedProductsPage();
            //new NavigationPage(new PizzaPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
