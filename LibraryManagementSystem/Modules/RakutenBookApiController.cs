using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace LibraryManagementSystem.Modules
{
    
    class RakutenBookApiController
    {
        static string applicationId = @"1099179332900118213";
        static string rakutenApiUrl =
            @"https://app.rakuten.co.jp/services/api/BooksBook/Search/20130522?applicationId="
            + applicationId
            + @"&formatVersion=2"
            + @"&isbn=";

        /// <summary>
        /// Typeは基本的に、LibraryManagementSystem.Modules.RakutenJsonModels.RakutenBook である
        /// </summary>
        /// <returns>Typeのインスタンス</returns>
        public static Type GetBookDataFromIsbn<Type>(string _isbn)
        {
            try
            {
                var req = WebRequest.CreateHttp(rakutenApiUrl + _isbn);
                req.Method = "GET";
                req.ContentType = "application/json";
                var res = req.GetResponse();
                var serializer = new JsonSerializer();
                return serializer.Deserialize<Type>(new JsonTextReader(new StreamReader(res.GetResponseStream())));
            }
            catch (Exception)
            {
                return default(Type);
            }
        }
    }
}
