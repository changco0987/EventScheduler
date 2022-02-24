using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using EventScheduler.Model;

namespace EventScheduler
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing() 
        {
            try
            {
                base.OnAppearing();
                myCollectionView.ItemsSource = await App.MyDatabase.ReadSchedule();
            }
            catch { }
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            myCollectionView.ItemsSource = await App.MyDatabase.Search(e.NewTextValue);
        }

        private async void ToolbarItemCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new createNewEvent());
        }
        private async void SwipeItemRemove_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as eventModel;
            var result = await DisplayAlert("Delete", $"Delete { emp.EventName} from the database","Yes","No");
            if (result) 
            {
                await App.MyDatabase.DeleteSchedule(emp);
                myCollectionView.ItemsSource = await App.MyDatabase.ReadSchedule();
            }
        } 

        private async void SwipeItemEdit_Invoked(object sender, EventArgs e)
        {
            var item = sender as SwipeItem;
            var emp = item.CommandParameter as eventModel;
            await Navigation.PushAsync(new createNewEvent(emp));
        }
    }
}
