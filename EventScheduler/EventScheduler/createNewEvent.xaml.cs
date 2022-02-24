using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using EventScheduler.Model;

namespace EventScheduler
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class createNewEvent : ContentPage
    {
        public createNewEvent()
        {
            InitializeComponent();
        }

        Model.eventModel _EventModel;
        public createNewEvent(Model.eventModel EventModel)
        {
            InitializeComponent();
            Title = "Edit Event";
            _EventModel = EventModel;
            eventName.Text = EventModel.EventName;

            DateTime convertedDate = DateTime.Parse(EventModel.EventDate);//This will convert the string into Date format
            eventDate.Date = convertedDate;

            eventName.Focus();
        }


        async void AddNewSchedule() 
        {

            await App.MyDatabase.CreateSchedule(new Model.eventModel
            {
                EventName = eventName.Text,
                EventDate = eventDate.Date.ToString("MM/dd/yyyy")
            });
            await Navigation.PopAsync();
        }

        private async void ToolbarItemSubmit_Clicked(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(eventName.Text) || string.IsNullOrWhiteSpace(eventDate.Date.ToString("MM/dd/yyyy")))
            {
                await DisplayAlert("Invalid", "Blank or Whitespace value is Invalid!", "OK");
            }
            else if (_EventModel!=null) 
            {
                UpdateSchedule();
            }
            else
            {
                AddNewSchedule();
            }

        }

        async void UpdateSchedule() 
        {
            _EventModel.EventName = eventName.Text;
            _EventModel.EventDate = eventDate.Date.ToString("MM/dd/yyyy");
            await App.MyDatabase.UpdateSchedule(_EventModel);
            await Navigation.PopAsync();
        }
    }
}