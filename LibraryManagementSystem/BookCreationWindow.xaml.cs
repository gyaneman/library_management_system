﻿using System;
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
using LibraryManagementSystem.Modules;

namespace LibraryManagementSystem
{
    public delegate void CreateBookDelegate(Book book);
    /// <summary>
    /// BookCreationWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BookCreationWindow : Window
    {
        public CreateBookDelegate createBookDelegate;

        string imageUrl;

        public BookCreationWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 新規図書登録用のボタンが押された時のハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBookCreation_Click(object sender, RoutedEventArgs e)
        {
            if (this.textBoxTitle.Text == string.Empty)
            {
                return;
            }
            var newBook = new Book();
            newBook.Isbn = this.textBoxIsbn.Text;
            newBook.Title = this.textBoxTitle.Text;
            newBook.Author = this.textBoxAuthor.Text;
            newBook.Publisher = this.textBoxPublisher.Text;
            newBook.Caption = this.textBoxCaption.Text;
            newBook.ImageUrl = this.imageUrl;
            if (Book.Save(newBook) == Result.Success)
            {
                createBookDelegate(newBook);
            }
            this.Close();
        }

        /// <summary>
        /// キャンセルボタンを押された時のハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ISBNで検索を行うボタンが押された時のハンドラ
        /// 楽天APIで検索する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSearchFromIsbn_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Search From ISBN");

            if (this.textBoxIsbn.Text == "")
            {
                return;
            }
            
            List<Book> books = Book.GetBookFromIsbn(this.textBoxIsbn.Text);
            if (books.Count == 1)
            {
                Book book = books[0];
                this.textBoxTitle.Text = book.Title;
                this.textBoxPublisher.Text = book.Publisher;
                this.textBoxAuthor.Text = book.Author;
                this.textBoxIsbn.Text = book.Isbn;
                this.textBoxCaption.Text = book.Caption;
                this.imageUrl = book.ImageUrl;
                this.imageBox.Source = new BitmapImage(new Uri(this.imageUrl));
            }
            foreach(Book book in books)
            {
                Console.WriteLine("================");
                book.Show();
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("UserListWindow: Error");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
