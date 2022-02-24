using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using SQLite;
namespace EventScheduler.Model
{
    public class eventModel
    {
        public static ObservableCollection<eventModel> eventList = new ObservableCollection<eventModel>();

        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]

        public string EventName { get; set; }
        public string EventDate { get; set; }
        
    }
}
