using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    /// <summary>
    /// BookCreationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BookCreationWindow : Window
    {
        public BookCreationWindow()
        {
            InitializeComponent();
        }

        private void buttonBookCreation_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxTitle.Text == string.Empty)
            {
                this.labelWarningTitle.Content = "Please enter title.";
                this.labelWarningTitle.Visibility = Visibility.Visible;
                return;
            }
            var newBook = new Entities.BookEntity();
            newBook.Title = this.textBoxTitle.Text;
            newBook.Author = this.textBoxAuthor.Text;
            newBook.Publisher = this.textBoxPublisher.Text;
            Book.Save(newBook);
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSearchFromIsbn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Search From ISBN");
        }
    }
}
