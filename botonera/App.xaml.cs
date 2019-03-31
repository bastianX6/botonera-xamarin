using System;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using botonera.View;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace botonera
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=f70377f4-7c1e-4bee-98af-5c6db9d27bbe;"
                + "android=faf6a221-117d-4402-9385-652657b62895", typeof(Analytics), typeof(Crashes));
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
