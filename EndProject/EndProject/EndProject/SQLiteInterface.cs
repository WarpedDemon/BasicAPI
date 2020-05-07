using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace EndProject
{
    public interface SQLiteInterface
    {
        SQLiteAsyncConnection GetConnection();
    }
}
