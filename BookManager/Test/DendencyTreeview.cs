using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Test.Classes;
using System.Windows;
using System.Windows.Controls;

namespace Test
{
    public class DendencyTreeview : TreeView
    {
        public int RentalBook;
        public DendencyTreeview()
        {

        }
        public  string DeTreeview
        {
            get { return (string) GetValue(deTreeview); }
            set { SetValue(deTreeview, value); }
        }


        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static  DependencyProperty deTreeview =
            DependencyProperty.Register("DeTreeview", typeof(string), typeof(DendencyTreeview), new PropertyMetadata(MakeTreeview));

        private static void MakeTreeview(DependencyObject source, DependencyPropertyChangedEventArgs e)
        {
            DendencyTreeview DT = source as DendencyTreeview;
           List<BookInformation> LB;
            DT.Items.Clear();

            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.dataPath))
            {
                conn.CreateTable<BookInformation>();
                LB = conn.Table<BookInformation>().ToList();

            }

            // LB = DT.DeTreeview as List<BookInformation>;


            if (LB != null)
            {
                List<BookInformation> ll = new List<BookInformation>();
                TreeViewItem treeItem = null;
                ll = LB.FindAll(FindSort1);
                treeItem = new TreeViewItem();
                treeItem.Header = "미분류" + " (" + ll.Count() + ")";

                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "미분류")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();



                ll = LB.FindAll(FindSort2);
                treeItem.Header = "인문" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "인문")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort3);
                treeItem.Header = "소설" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "소설")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort4);
                treeItem.Header = "시/에세이" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "시/에세이")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort5);
                treeItem.Header = "역사/문화" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "역사/문화")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort6);
                treeItem.Header = "예술/대중문화" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "예술/대중문화")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort7);
                treeItem.Header = "정치/사회" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "정치/사회")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort8);
                treeItem.Header = "경제/경영" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "경제/경영")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort9);
                treeItem.Header = "수학/과학" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "수학/과학")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort10);
                treeItem.Header = "자기계발" + " (" + ll.Count() + ")";
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Sort == "자기계발")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();

                ll = LB.FindAll(FindSort11);
                treeItem.Header = "대여 중" + " (" + ll.Count() + ")";
                DT.RentalBook = ll.Count();
                MainWindow.RentalBooks = ll.Count();
                for (int i = 0; i < ll.Count(); i++)
                {
                    if (ll[i].Rental == "대여 중")
                    {
                        treeItem.Items.Add(new TreeViewItem() { Header = ll[i].Title });
                    }
                }
                DT.Items.Add(treeItem);
                treeItem = new TreeViewItem();
            }

        }

        private static bool FindSort1(BookInformation bk)
        {

            if (bk.Sort == "미분류")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort2(BookInformation bk)
        {

            if (bk.Sort == "인문")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private static bool FindSort3(BookInformation bk)
        {

            if (bk.Sort == "소설")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort4(BookInformation bk)
        {

            if (bk.Sort == "시/에세이")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort5(BookInformation bk)
        {

            if (bk.Sort == "역사/문화")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort6(BookInformation bk)
        {

            if (bk.Sort == "예술/대중문화")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort7(BookInformation bk)
        {

            if (bk.Sort == "정치/사회")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort8(BookInformation bk)
        {

            if (bk.Sort == "경제/경영")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort9(BookInformation bk)
        {

            if (bk.Sort == "수학/과학")
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        private static bool FindSort10(BookInformation bk)
        {

            if (bk.Sort == "자기계발")
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private static bool FindSort11(BookInformation bk)
        {

            if (bk.Rental == "대여 중")
            {
                return true;
            }
            else
            {
                return false;
            }


        }
    }
}
