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
        public static Entities.BookEntity GetBookDataFromIsbn()
        {
            var book = new Entities.BookEntity();
            var client = new HttpClient();
            return book;
        }
    }
}
