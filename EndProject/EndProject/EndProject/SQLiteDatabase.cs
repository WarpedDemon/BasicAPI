using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EndProject
{
    class SQLiteDatabase : SQLiteInterface
    {
        public SQLiteAsyncConnection GetConnection()
        {
            return new SQLiteAsyncConnection("database.db3");
        }
    }
}
