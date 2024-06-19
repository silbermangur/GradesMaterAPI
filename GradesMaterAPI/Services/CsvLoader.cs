using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace GradesMaterAPI.Services
{
    public class CsvLoader : ICsvLoader
    {
        private string path="";
        public CsvLoader(){}

        public string Path {
            get 
            { 
                return path; 
            }  set 
            {
                path = value; 
            }
        } 

        public void test(string p)
        {
             path = p;
             Console.WriteLine(Path);
        }
    }
}
