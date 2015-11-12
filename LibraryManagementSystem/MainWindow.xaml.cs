using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        static SQLiteConnection _conn = null;
        string dbName = "c:\\Users\\kataoka\\Application\\dbdata\\sample.sqlite3";

        public MainWindow()
        {
            InitializeComponent();
            this.ConnectionOpen();
            this.ConnectionClose();
        }

        public void ConnectionOpen()
        {
            Console.WriteLine("DB Connecting...");
            _conn = new SQLiteConnection();
            _conn.ConnectionString = "Data Source=" + dbName + ";Version=3;";
            _conn.Open();
            Console.WriteLine("DB Connected.");
        }

        public void ConnectionClose()
        {
            Console.WriteLine("DB Closing...");
            _conn.Clone();
            Console.WriteLine("DB Closed.");
        }

        public void dataGrid_SelectionChanged(object sender, EventArgs e)
        {
            Console.WriteLine("dataGrid selection changed");
        }
    }
}
