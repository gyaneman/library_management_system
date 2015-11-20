using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Modules
{
    class Http
    {
        public static Models.Book GetBookDataFromIsbn()
        {
            var book = new Models.Book();
            var client = new HttpClient();
            return book;
        }
    }
}
