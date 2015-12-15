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
    /// BookDetailsWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class BookDetailsWindow : Window
    {
        Book book;
        User user;

        string dueDateContent;

        /// <summary>
        /// 本の詳細
        /// </summary>
        /// <param name="_book">詳細を表示する図書</param>
        /// <param name="_user">ログインしていればユーザのデータ、ログインしていなければnull</param>
        public BookDetailsWindow(Book _book, User _user)
        {
            InitializeComponent();
            book = _book;
            user = _user;

            if (_book == null)
            {
                return;
            }
            
            if (_user == null)
            {
                InitOfGuestOnly();
            }

            var history = LendingHistoryRecord.GetDueDateOfBook(_book);
            if (history.Count == 1)
            {
                CanNotBorrow(history[0].DueDate);
            }
            else
            {
                this.dueDateContent = "You can borrow.";
            }

            this.DataContext = new {
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                Publisher = book.Publisher,
                LendInfo = dueDateContent,
                Caption = book.Caption,
            };
            if (book.ImageUrl != null)
            {
                this.imageBox.Source = new BitmapImage(new Uri(book.ImageUrl));
            }
        }

        /// <summary>
        /// ログインしていないユーザのときのみ行う処理
        /// </summary>
        private void InitOfGuestOnly()
        {
            this.buttonRental.IsEnabled = false;
        }

        /// <summary>
        /// 貸し出しのボタンが押された時のハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRental_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show(
                "Do you take this book?",
                "Rental",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes
                ) == MessageBoxResult.Yes)
            {
                if (LendingHistoryRecord.Create(book, user) == Result.Success)
                {
                    MessageBox.Show("Thank you!!!", "Complete");
                    CanNotBorrow("You're borrowing.");
                }
            }
        }

        /// <summary>
        /// 図書が借りれないときに呼び出す
        /// </summary>
        /// <param name="_content">DueInfoに表示する文字列</param>
        private void CanNotBorrow(string _content)
        {
            this.dueDateContent = _content;
            this.buttonRental.IsEnabled = false;
        }

        private void NormalizationButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
