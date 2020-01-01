using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Test.Classes;

namespace Test
{
    public partial class MainWindow : Window
    {
        List<BookInformation> list = new List<BookInformation>();
        
        List<BookInformation> ll = new List<BookInformation>();



        public static int AddWindowTrigger;
        public static int RentalBooks;
        public static int Trigger_AnotherWindow_Closed=0;
        Chart chart;
        public MainWindow()
        {


            chart = new Chart();
            DataContext = new ChartViewModel(chart);


            
            using (SQLiteConnection connection = new SQLiteConnection(App.LockdataPath))
            {
                connection.CreateTable<SetInformation>();
                List<SetInformation> setlist = connection.Table<SetInformation>().ToList();
                if (setlist[0].IsSet.Equals("Set") && !setlist[0].IsSet.Equals("Null")) // 잠금설정시
                {
                    Lock newWin = new Lock();
                    newWin.ShowDialog();
                }
                setlist = connection.Table<SetInformation>().ToList();
                if (setlist[0].IsSet.Equals("NoSet")) //잠금 풀림
                {
                    string sql = "UPDATE SetInformation SET IsSet='Set' WHERE IsSet='NoSet'";
                    connection.Execute(sql);
                    InitializeComponent();
                    ReadDatabase();
                    AddWindowTrigger = 2;
                }
            }
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.dataPath))
            {
                conn.CreateTable<BookInformation>();
                list = conn.Table<BookInformation>().ToList();
            }
            

        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Menu.Toggle();
            chart = new Chart(list.Count(), RentalBooks);
            DataContext = new ChartViewModel(chart);
        }





        public void ReadDatabase()
        {

            Trigger_kiwan.Content = Trigger_kiwan.Content + "a";
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.dataPath))
            {
                conn.CreateTable<BookInformation>();
                list = conn.Table<BookInformation>().ToList();
                LvUsers.ItemsSource = list;
            }
            AllBook.Content = list.Count();
            AllRentalBook.Content = RentalBooks;

            

            
        }


        private void MenuItem_Click(object sender, RoutedEventArgs e)//삭제 버튼
        {
            var test = LvUsers2.SelectedItem as BookInformation;
            list.Remove(test);
            using (SQLiteConnection connection = new SQLiteConnection(App.dataPath))
            {
                string sql = "DELETE FROM BookInformation WHERE Title=" + "'" + test.Title + "'";
                connection.Execute(sql);
            }
            LvUsers.Items.Refresh();

               ReadDatabase();
               Lvuser2Update();
        }




        private void MenuItem_Click_1(object sender, RoutedEventArgs e) // 책추가
        {
            int tmp = Trigger_AnotherWindow_Closed;
            BookAdd newWin = new BookAdd();
            newWin.ShowDialog();

            if (tmp != Trigger_AnotherWindow_Closed)
            {
                ReadDatabase();
                Lvuser2Update();
            }

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e) // 전체 불러오기
        {
            ReadDatabase();

            //MessageBox.Show(cbBox.SelectedItem.ToString());

        }
        private void MenuItem_Click_3(object sender, RoutedEventArgs e) // 수정하기
        {
            int tmp = Trigger_AnotherWindow_Closed;
            var tt = LvUsers2.SelectedItem as BookInformation;
            BookSetting newWin = new BookSetting(tt.Title);
            newWin.TB_SETTING_Title.Text = tt.Title;
            newWin.TB_SETTING_Writer.Text = tt.Writer;
            newWin.TB_SETTING_Price.Text = tt.Price;
            newWin.TB_SETTING_Publisher.Text = tt.Publisher;
            newWin.TB_SETTING_ISBN.Text = tt.ISBN;
            newWin.TB_SETTING_Position.Text = tt.Position;
            newWin.TB_SETTING_Pages.Text = tt.Pages;
            newWin.ShowDialog();
            if (tmp != Trigger_AnotherWindow_Closed)
            {
                ReadDatabase();
                Lvuser2Update();
            }


        }
        private void MenuItem_Click_4(object sender, RoutedEventArgs e) // 검색하기
        {

            var tt = LvUsers2.SelectedItem as BookInformation;


            string query = tt.Title; // 검색할 문자열
            string url = "https://openapi.naver.com/v1/search/book.json?query=" + query + "&display=77"; // 결과가 JSON 포맷
            string text = "";  // string url = "https://openapi.naver.com/v1/search/blog.xml?query=" + query;  // 결과가 XML 포맷
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

                LBITEM_Title.Content = tmp2;
                Original = result.items[0].author;
                tmp1 = Original.Replace("<b>", "");
                tmp2 = tmp1.Replace("</b>", "");
                LBITEM_Author.Content = tmp2;
                LBITEM_Price.Content = result.items[0].price;
                LBITEM_ISBN.Content = result.items[0].isbn;
                LBITEM_Publisher.Content = result.items[0].publisher;
                LBITEM_Link.Content = result.items[0].link;
                LBITEM_DiscountPrice.Content = result.items[0].discount;
                LBITEM_Pubdate.Content = result.items[0].pubdate;

                Original = result.items[0].description;
                tmp1 = Original.Replace("<b>", "");
                tmp2 = tmp1.Replace("</b>", "");

                LBITEM_Description.Content = tmp2;

                ImageSource ii = new BitmapImage(new Uri(result.items[0].image));
                img.Source = ii;
            }
            catch (ArgumentOutOfRangeException ff)
            {
                MessageBox.Show("검색실패!");
            }



        }

        void Lvuser2Update()
        {
            TextBox searchTextBox = TB_Search2;
            ComboBoxItem typeItem = (ComboBoxItem)cbBox2.SelectedItem;
            if (typeItem.Content.ToString() == "제목")
            {
                var filteredList = list.Where(c => c.Title.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "저자")
            {
                var filteredList = list.Where(c => c.Writer.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "가격")
            {
                var filteredList = list.Where(c => c.Price.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "ISBN")
            {
                var filteredList = list.Where(c => c.ISBN.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "위치")
            {
                var filteredList = list.Where(c => c.Position.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "페이지")
            {
                var filteredList = list.Where(c => c.Pages.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "출판사")
            {
                var filteredList = list.Where(c => c.Publisher.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
            else if (typeItem.Content.ToString() == "대여 여부")
            {
                var filteredList = list.Where(c => c.Rental.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();
                LvUsers2.ItemsSource = filteredList;
            }
        }

        private void BNT_Search_Click(object sender, RoutedEventArgs e)
        {
            Lvuser2Update();
        }

        private void LvUsers_Click(object sender, RoutedEventArgs e)
        {
            var column = e.OriginalSource as GridViewColumnHeader;
            string check = column.Content.ToString();
            if (check == "제목")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            }
            else if (check == "저자")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Writer", ListSortDirection.Ascending));
            }
            else if (check == "가격")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Price", ListSortDirection.Ascending));
            }
            else if (check == "ISBN")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("ISBN", ListSortDirection.Ascending));
            }
            else if (check == "페이지수")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Pages", ListSortDirection.Ascending));
            }
            else if (check == "출판사")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Publisher", ListSortDirection.Ascending));
            }
            else if (check == "위치")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Position", ListSortDirection.Ascending));
            }
            else if (check == "대여 여부")
            {
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(LvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("Rental", ListSortDirection.Ascending));
            }
        }

        private void LvUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void MenuItem_Rental(object sender, RoutedEventArgs e)
        {
            var tmp = sender as MenuItem;
            MessageBox.Show(tmp.Header.ToString());
            if (tmp.Header.ToString() == "대출")
            {
                var tt = LvUsers2.SelectedItem as BookInformation;
                using (SQLiteConnection connection = new SQLiteConnection(App.dataPath))
                {
                    string sql = "UPDATE BookInformation SET " + "Rental=" + "'" + "대여 중" + "'" +
                                   " WHERE Title='" + tt.Title + "'";
                    connection.Execute(sql);
                }
            }
            else if (tmp.Header.ToString() == "반납")
            {
                var tt = LvUsers2.SelectedItem as BookInformation;
                using (SQLiteConnection connection = new SQLiteConnection(App.dataPath))
                {
                    string sql = "UPDATE BookInformation SET " + "Rental=" + "'" + "대여 가능" + "'" +
                                   " WHERE Title='" + tt.Title + "'";
                    connection.Execute(sql);
                }
            }
            ReadDatabase();
        }


        private void MenuButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)//창옮기기
        {
            DragMove();
        }

        private void MenuButton_MouseDown_1(object sender, MouseButtonEventArgs e)//도서추가(옆메뉴)
        {
            BookAdd newWin = new BookAdd();
            newWin.ShowDialog();
        }


        private void MenuButton_MouseDown_3(object sender, MouseButtonEventArgs e)//제작정보
        {
            MessageBox.Show("Completion date : 19-06-05\n\nPeriod : about 20 days\n\nProgrammer : Ki-Whan, Sung-min\n\nE-mail : dml4258@naver.com", "Information                              ");
        }

        private void MenuButton_MouseDown_2(object sender, MouseButtonEventArgs e)//레이아웃 변경
        {
            SolidColorBrush myBrush = (SolidColorBrush)this.TryFindResource("Top_Color");
            if(myBrush.Color.Equals((Color)ColorConverter.ConvertFromString("#FF6969FF")))// 노랑 배경일시
            {

                AddWindowTrigger = 1;
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFEAB00C");

                myBrush = (SolidColorBrush)this.TryFindResource("Top2_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFFFF0C5");

                myBrush = (SolidColorBrush)this.TryFindResource("Overall_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFFFD863");

            }
            else
            {
                AddWindowTrigger = 2;
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FF6969FF");

                myBrush = (SolidColorBrush)this.TryFindResource("Top2_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFEAEAFF");

                myBrush = (SolidColorBrush)this.TryFindResource("Overall_Color");
                myBrush.Color = (Color)ColorConverter.ConvertFromString("#FFA5B9F0");
            }
            

        }

        private void MenuButton_MouseDown_4(object sender, MouseButtonEventArgs e)//잠금설정
        {
            LockSetting newWin = new LockSetting();
            newWin.ShowDialog();
        }
    }


    internal class ChartViewModel
    {

        public List<Chart> Chart { get; private set; }

        public ChartViewModel(Chart chart)
        {
            Chart = new List<Chart>();
            Chart.Add(chart);
        }
    }
    internal class Chart
    {
        public string Title { get; private set; }
        public int Porcentagem { get; private set; }
        public double allbook;
        public double rentalbook;
        public Chart()
        {
            Title = "대여가능도서";
            allbook = 100;
            rentalbook = 100;
            Porcentagem = CalcularPorcentagem();
            
        }
        public Chart(int all,int rental)
        {
            Title = "대여가능도서";
            allbook = all;
            rentalbook = rental;
            Porcentagem = CalcularPorcentagem();
        }

        public void Reset()
        {
            Porcentagem = CalcularPorcentagem();
        }
        private int CalcularPorcentagem()
        {
            double test = 100 - ((rentalbook / allbook )*100);
            return (int)test;
        }
    }
}
