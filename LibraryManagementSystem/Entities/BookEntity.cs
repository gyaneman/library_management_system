using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Entities
{
    class BookEntity: BaseEntity
    {
        private string isbn;
        private string title;
        private string author;
        private string publisher;

        public string Title
        {
            set
            {
                this.title = value;
                UpdateEditedAt();
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
                UpdateEditedAt();
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
                UpdateEditedAt();
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
                UpdateEditedAt();
            }
            get
            {
                return this.publisher;
            }
        }

        public BookEntity():base()
        {
        }

        public BookEntity(string id): base(id)
        {
            Console.WriteLine(id);
        }

        public void Print()
        {
            Console.WriteLine("Title:" + title);
        }
    }
}
