using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sat.Recruitment.Data.DB
{
    public class DataBase
    {
        private readonly static DataBase _instance = new DataBase();
        private DataBase()
        {

        }
        public static DataBase GetInstance
        {
            get
            {
                return _instance;
            }
        }
        public StreamReader ReadUsersFromFile()
        {
            var path = Directory.GetCurrentDirectory() + "\\Users.txt";

            FileStream fileStream = new FileStream(path, FileMode.Open);

            StreamReader reader = new StreamReader(fileStream);
            return reader;
        }
    }
}
