using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Classes
{
    class BookInformation
    {
        // [PrimaryKey,AutoIncrement]
        public string Title { get; set; }
        public string Writer { get; set; }
        public string Publisher { get; set; }
        public string Price { get; set; }
        public string ISBN { get; set; }
        public string Position { get; set; }
        public string Pages { get; set; }

        public string Sort { get; set; }

        public string Rental { get; set; }


    }
}
