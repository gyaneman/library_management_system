using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace LibraryManagementSystem
{
    /// <summary>
    /// LendingHistoryWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LendingHistoryWindow : Window
    {
        ObservableCollection<LendingHistoryRecord> lendingHistoryToBeDisplayed;
        private User user;

        public LendingHistoryWindow(User _user)
        {
            InitializeComponent();
            user = _user;
            var history = LendingHistoryRecord.GetUnreturnedBookFromUser(_user);
            lendingHistoryToBeDisplayed = 
                new ObservableCollection<LendingHistoryRecord>(history);
            this.dataGrid.ItemsSource = lendingHistoryToBeDisplayed;
        }
    }
}
