using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Modules.RakutenJsonModels
{
    public class RakutenBook
    {
        public class Item
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
            public int itemPrice { get; set; }
            public int listPrice { get; set; }
            public int discountRate { get; set; }
            public int discountPrice { get; set; }
            public string itemUrl { get; set; }
            public string affiliateUrl { get; set; }
            public string smallImageUrl { get; set; }
            public string mediumImageUrl { get; set; }
            public string largeImageUrl { get; set; }
            public string chirayomiUrl { get; set; }
            public string availability { get; set; }
            public int postageFlag { get; set; }
            public int limitedFlag { get; set; }
            public int reviewCount { get; set; }
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
        public List<Item> Items { get; set; }
    }
}
