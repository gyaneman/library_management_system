using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
//using System.Net.Http;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace LibraryManagementSystem.Modules
{
    
    class RakutenResponseBook
    {
        public int count { get; set; }
        public int page { get; set; }
        public int first { get; set; }
        public int last { get; set; }
        public int hits { get; set; }
        public int carrier { get; set; }
        public int pageCount { get; set; }

    }
    class Http
    {
        static string applicationId = @"1099179332900118213";
        static string rakutenApiUrl = @"https://app.rakuten.co.jp/services/api/BooksBook/Search/20130522?applicationId=1099179332900118213&isbn=9784274068560";
        public static Models.Book GetBookDataFromIsbn()
        {
            var book = new Models.Book();
            // var client = new HttpClient();


            var req = WebRequest.CreateHttp(rakutenApiUrl);
            req.Method = "GET";
            req.ContentType = "application/json";
            var res = req.GetResponse();
            var serializer = new JsonSerializer();
            RakutenResponseBook result = serializer.Deserialize<RakutenResponseBook>(new JsonTextReader(new StreamReader(res.GetResponseStream())));
            Console.WriteLine(result.ToString());
            Console.WriteLine(result.count);
            return book;
        }
    }
}
