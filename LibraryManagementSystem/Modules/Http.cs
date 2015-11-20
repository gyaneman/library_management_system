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
    public class RakutenResponseBook
    {
        public class RakutenResponseBookItem
        {
            public string title { get; set; }
            public string titleKana { get; set; }
            public string subTitle { get; set; }
            public string subTitleKana { get; set; }
            public string seriesName { get; set; }
            public string seriesNameKana { get; set; }
            public string contents { get; set; }
            public string contentsKana { get; set; }
            public string author { get; set; }
            public string authorKana { get; set; }
            public string publisherName { get; set; }
            public string size { get; set; }
            public string isbn { get; set; }
            public string itemCaption { get; set; }
            public string salesDate { get; set; }
            public int    itemPrice { get; set; }
            public int    listPrice { get; set; }
            public int    discountRate { get; set; }
            public int    discountPrice { get; set; }
            public string itemUrl { get; set; }
            public string affiliateUrl { get; set; }
            public string smallImageUrl { get; set; }
            public string mediumImageUrl { get; set; }
            public string largeImageUrl { get; set; }
            public string chirayomiUrl { get; set; }
            public string availability { get; set; }
            public int    postageFlag { get; set; }
            public int    limitedFlag { get; set; }
            public int    reviewCount { get; set; }
            public string reviewAverage { get; set; }
            public string booksGenreId { get; set; }
        }
        public int count { get; set; }
        public int page { get; set; }
        public int first { get; set; }
        public int last { get; set; }
        public int hits { get; set; }
        public int carrier { get; set; }
        public int pageCount { get; set; }
        public List<RakutenResponseBookItem> Items { get; set; }
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
