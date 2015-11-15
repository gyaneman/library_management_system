using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Entities
{
    class BookEntity: BaseEntity
    {
        private string isbn = null;
        private string title = null;
        private string author = null;
        private string publisher = null;
        private string series = null;

        public string Title
        {
            set
            {
                this.title = value;
                Edited();
            }
            get
            {
                return this.title;
            }
        }

        public string Isbn
        {
            set
            {
                this.isbn = value;
                Edited();
            }
            get
            {
                return this.isbn;
            }
        }

        public string Author
        {
            set
            {
                this.author = value;
                Edited();
            }
            get
            {
                return this.author;
            }
        }

        public string Publisher
        {
            set
            {
                this.publisher = value;
                Edited();
            }
            get
            {
                return this.publisher;
            }
        }

        public string Series
        {
            set
            {
                this.series = value;
                Edited();
            }
            get
            {
                return this.series;
            }
        }

        public BookEntity():base()
        {
        }

        public BookEntity(
            string _id,
            string _isbn,
            string _title, 
            string _author, 
            string _publisher,
            string _series, 
            string _created_at, 
            string _edited_at)
            : base(_id, _created_at, _edited_at)
        {
            Console.WriteLine(_id);
            this.isbn = _isbn;
            this.title = _title;
            this.author = _author;
            this.publisher = _publisher;
            this.series = _series;
        }

        public void Print()
        {
            Console.WriteLine("Title:" + title);
        }
    }
}
