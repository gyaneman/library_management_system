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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_user"></param>
        public LendingHistoryWindow(User _user)
        {
            InitializeComponent();
            user = _user;
            var history = LendingHistoryRecord.GetUnreturnedBookFromUser(_user);
            lendingHistoryToBeDisplayed = 
                new ObservableCollection<LendingHistoryRecord>(history);
            this.dataGrid.ItemsSource = lendingHistoryToBeDisplayed;
        }

        /// <summary>
        /// datagrid上でダブルクリックされた時のハンドラ
        /// ダブルクリックされたアイテム（本）を返却
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var lendingRecord = this.dataGrid.SelectedItem as LendingHistoryRecord;
            lendingRecord.Return();
            if (lendingRecord == null)
            {
                return;
            }

            if (MessageBox.Show(
                "Are you shure you want to return this book?",
                "Return book", 
                MessageBoxButton.YesNo,
                MessageBoxImage.Question,
                MessageBoxResult.Yes
                ) == MessageBoxResult.Yes)
            {
                if (lendingRecord.Return() == Result.Success)
                {
                    lendingHistoryToBeDisplayed.Remove(lendingRecord);
                    MessageBox.Show("Return procedure was completed. Thank you!!", "Completed", MessageBoxButton.OK);
                }
            }
        }
    }
}
