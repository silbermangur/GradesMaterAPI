using GradesMaterAPI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace GradesMaterAPI.Services
{
    public class CsvExport<T> : IExport<T>
    {
        // fields
        private ICollection<T> arr;
        private string path = "";

        // Properties
        public ICollection<T> Arr
        {
            get { return arr; }
            set { arr = value; }
        }

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        // Ctor
        public CsvExport()
        {
            arr = new List<T>();
        }

        public void Export(string path, List<T> list)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };

            using (var writer = new StreamWriter(path))
            using (var csv = new CsvWriter(writer, config))
            {
                csv.WriteRecords(list);
            }

            Console.WriteLine("CSV file has been written successfully.");
        }
    }
}

