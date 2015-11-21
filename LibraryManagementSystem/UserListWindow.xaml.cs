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
using System.Collections.ObjectModel;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem
{
    /// <summary>
    /// UserListWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class UserListWindow : Window
    {
        ObservableCollection<User> usersToBeDisplayed;
        public UserListWindow()
        {
            InitializeComponent();
            var users = User.GetAllUsers();
            usersToBeDisplayed = new ObservableCollection<User>(users);
            this.dataGrid.ItemsSource = usersToBeDisplayed;
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            User user = this.dataGrid.SelectedItem as User;
            if (user != null)
            {
                Console.WriteLine(user.Name + "   " + user.CreatedAt);
            }
        }
    }
}
