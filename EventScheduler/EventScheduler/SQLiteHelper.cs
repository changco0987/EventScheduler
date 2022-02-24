using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using EventScheduler.Model;

namespace EventScheduler
{
    public class SQLiteHelper
    {
        private readonly SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<eventModel>();
        }
        public Task<int> CreateSchedule(eventModel EventModel)
        {
            return db.InsertAsync(EventModel);
        }
        public Task<List<eventModel>> ReadSchedule()
        {
            return db.Table<eventModel>().ToListAsync();
        }
        public Task<int> UpdateSchedule(eventModel EventModel)
        {
            return db.UpdateAsync(EventModel);
        }
        public Task<int> DeleteSchedule(eventModel EventModel)
        {
            return db.DeleteAsync(EventModel);
        }
        public Task<List<eventModel>> Search(string search) 
        {
            return db.Table<eventModel>().Where(p => p.EventName.StartsWith(search)).ToListAsync();
        }
    }
}
