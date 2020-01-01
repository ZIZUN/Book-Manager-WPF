using SQLite;
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
using Test.Classes;

namespace Test
{
    /// <summary>
    /// LockSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LockSetting : Window
    {
        public LockSetting()
        {
            InitializeComponent();
            SolidColorBrush myBrush = (SolidColorBrush)this.TryFindResource("Top_Color");

            if (MainWindow.AddWindowTrigger == 1)
            {

                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFEAB00C");

                myBrush = (SolidColorBrush)this.TryFindResource("Top2_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFFFF0C5");

                myBrush = (SolidColorBrush)this.TryFindResource("Overall_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFFFD863");
            }
            else
            {

                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FF6969FF");

                myBrush = (SolidColorBrush)this.TryFindResource("Top2_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFEAEAFF");

                myBrush = (SolidColorBrush)this.TryFindResource("Overall_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFA5B9F0");
            }
        }

        private void Lock_set_Click(object sender, RoutedEventArgs e)
        {
            SetInformation sb = new SetInformation();

            sb.PassWord = Lock_password.ToString();
            sb.IsSet = "Set";

            using (SQLiteConnection connection = new SQLiteConnection(App.LockdataPath))
            {
                string sql = "DELETE FROM SetInformation";
                connection.Execute(sql);

                connection.CreateTable<SetInformation>();
                connection.Insert(sb);
            }
            Window.GetWindow(this).Close();
        }

        private void Lock_unlock_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.LockdataPath))
            {
                string sql = "UPDATE SetInformation SET IsSet='Null' WHERE IsSet='Set'";
                connection.Execute(sql);
            }
            Window.GetWindow(this).Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
