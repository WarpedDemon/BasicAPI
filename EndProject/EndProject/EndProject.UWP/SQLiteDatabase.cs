using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(EndProject.UWP.SQLiteDatabase))]

namespace EndProject.UWP
{
    class SQLiteDatabase : SQLiteInterface
    {
        public SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection("database.db2");
        }
    }
}
