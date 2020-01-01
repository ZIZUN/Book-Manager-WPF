using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string databaseName = "BookDatabase.db";
        public static string folderPath = Directory.GetCurrentDirectory();
        public static string dataPath = System.IO.Path.Combine(folderPath, databaseName);

        public static string LockdatabaseName = "LockDatabase.db";
        public static string LockdataPath = System.IO.Path.Combine(folderPath, LockdatabaseName);
    }
}