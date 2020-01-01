using Test.Classes;
using Test;
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
    /// BookSetting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BookSetting : Window
    {
        string title1;
        public BookSetting()
        {
            InitializeComponent();
           
        }
        public BookSetting(string tt)
        {

            InitializeComponent();
            title1 = tt;
            MessageBox.Show(title1);

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

        private void Set_Click(object sender, RoutedEventArgs e)
        {
            BookInformation bb = new BookInformation();
            bb.Title = TB_SETTING_Title.Text;
            bb.Writer = TB_SETTING_Writer.Text;
            bb.Price = TB_SETTING_Price.Text;
            bb.ISBN = TB_SETTING_ISBN.Text;
            bb.Position = TB_SETTING_Position.Text;
            bb.Pages = TB_SETTING_Pages.Text;
            bb.Publisher = TB_SETTING_Publisher.Text;
            bb.Rental = TB_SETTING_Rental.Text;


            using (SQLiteConnection connection = new SQLiteConnection(App.dataPath))
            {
                string sql = "UPDATE BookInformation SET Title=" + "'" + bb.Title + "'" + ", " + "Writer="
                                + "'" + bb.Writer + "'" + ", " + "Price=" + "'" + bb.Price + "'" + ", "
                                + "ISBN=" + "'" + bb.ISBN + "'" + ", " + "Position=" + "'" + bb.Position + "'" + ", "
                               + "Pages=" + "'" + bb.Pages + "'" + ", " + "Publisher=" + "'" + bb.Publisher + "'" + ", " + "Rental=" + "'" + bb.Rental + "'" +
                               " WHERE Title='" + title1 + "'";
                connection.Execute(sql);
            }
            MainWindow.Trigger_AnotherWindow_Closed++;
            this.Close();

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
