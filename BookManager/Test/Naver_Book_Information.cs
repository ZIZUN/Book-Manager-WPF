using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Item
    {
        public string title { get; set; }
        public string link { get; set; }
        public string image { get; set; }
        public string author { get; set; }
        public string price { get; set; }
        public string discount { get; set; }
        public string publisher { get; set; }
        public string pubdate { get; set; }
        public string isbn { get; set; }
        public string description { get; set; }
    }

    public class Example
    {
        public string lastBuildDate { get; set; }
        public int total { get; set; }
        public int start { get; set; }
        public int display { get; set; }
        public IList<Item> items { get; set; }
    }
}
