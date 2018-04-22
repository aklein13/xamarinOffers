using System;

using App3.Views;
using Xamarin.Forms;

namespace App3
{
	public partial class App : Application
	{

		public App ()
		{
			InitializeComponent();


            MainPage = new MainPage();
        }

		protected override void OnStart ()
		{
            Console.WriteLine("Start");
		}

		protected override void OnSleep ()
		{
			Console.WriteLine("Sleep");
		}

		protected override void OnResume ()
		{
            Console.WriteLine("Resume");
        }
	}
}
