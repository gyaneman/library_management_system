using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace LibraryManagementSystem.Models
{
    enum Result
    {
        Success,
        Failed
    }

    class Model
    {
        static string dbPath = "c:\\Users\\kataoka\\Application\\dbdata\\library_management_system.sqlite3";
        protected static string dbConStr = "Data Source=" + dbPath + ";Version=3;";
    }
}
