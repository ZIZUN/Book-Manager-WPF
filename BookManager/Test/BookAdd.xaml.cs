using Test.Classes;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// BookAdd.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BookAdd : Window
    {
        public BookAdd()
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
        
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            BookInformation bb = new BookInformation();
            bb.Title = TB_ADD_Title.Text;
            bb.Writer = TB_ADD_Writer.Text;
            bb.Price = TB_ADD_Price.Text;
            bb.ISBN = TB_ADD_ISBN.Text;
            bb.Position = TB_ADD_Position.Text;
            bb.Pages = TB_ADD_Pages.Text;
            bb.Publisher = TB_ADD_Publisher.Text;
            bb.Rental = "대여 가능";
            ComboBoxItem t = CB_Sort.SelectedItem as ComboBoxItem;
            bb.Sort = t.Content.ToString();
            

            using (SQLiteConnection connection = new SQLiteConnection(App.dataPath))
            {
                connection.CreateTable<BookInformation>();
                connection.Insert(bb);
            }
            MainWindow.Trigger_AnotherWindow_Closed++;
            this.Close();

        }

        private void TB_ADD_Research_Click(object sender, RoutedEventArgs e)
        {
            this.Height = 618;
            string query = TB_ADD_Search.Text; // 검색할 문자열
            string url = "https://openapi.naver.com/v1/search/book.json?query=" + query + "&display=77"; // 결과가 JSON 포맷
            string text = "";                                                                                       // string url = "https://openapi.naver.com/v1/search/blog.xml?query=" + query;  // 결과가 XML 포맷
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-Naver-Client-Id", "76Sy_EuBC_ia3aSqm8h9"); // 클라이언트 아이디
            request.Headers.Add("X-Naver-Client-Secret", "wiE4MJXrlI");       // 클라이언트 시크릿
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string status = response.StatusCode.ToString();
            if (status == "OK")
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                text = reader.ReadToEnd();
                // Console.WriteLine(text);
            }
            else
            {
                // Console.WriteLine("Error 발생=" + status);
            }

            Example result = JsonConvert.DeserializeObject<Example>(text);

            try
            {
                string Original = result.items[0].title;
                string tmp1 = Original.Replace("<b>", "");
                string tmp2 = tmp1.Replace("</b>", "");

                TB_ADD_Title.Text = tmp2;
                Original = result.items[0].author;
                tmp1 = Original.Replace("<b>", "");
                tmp2 = tmp1.Replace("</b>", "");
                TB_ADD_Writer.Text = tmp2;
                TB_ADD_Price.Text = result.items[0].price;
                TB_ADD_ISBN.Text = result.items[0].isbn;
                TB_ADD_Publisher.Text = result.items[0].publisher;
                ImageSource ii = new BitmapImage(new Uri(result.items[0].image));
                img.Source = ii;
            }
            catch (ArgumentOutOfRangeException ff)
            {
                MessageBox.Show("검색실패!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            
            Window.GetWindow(this).Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
