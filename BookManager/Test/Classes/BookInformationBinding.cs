using System;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Classes
{
    class BookInformationBinding
    {
        public ObservableCollection<BookInformation> BooksCollection { get; set; }
        public BookInformationBinding()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.dataPath))
            {
                conn.CreateTable<BookInformation>();
                BooksCollection = new ObservableCollection<BookInformation>(conn.Table<BookInformation>().ToList());
            }

        }
    }
}
