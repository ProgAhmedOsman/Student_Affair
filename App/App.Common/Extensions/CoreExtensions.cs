using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace App.Common.Extensions
{
    public static class CoreExtensions
    {
        public static void Merge<T>(this ObservableCollection<T> source, ObservableCollection<T> collection)
        {
            Merge<T>(source, collection, false);
        }

        public static void Merge<T>(this ObservableCollection<T> source, ObservableCollection<T> collection, bool ignoreDuplicates)
        {
            if (collection != null)
            {
                foreach (T item in collection)
                {
                    bool addItem = true;

                    if (ignoreDuplicates)
                        addItem = !source.Contains(item);

                    if (addItem)
                        source.Add(item);
                }
            }
        }



        static Dictionary<string, bool> BrowsableProperties = new Dictionary<string, bool>();
        static Dictionary<string, PropertyInfo[]> BrowsablePropertyInfos = new Dictionary<string, PropertyInfo[]>();


        public static string CleanFileName(this string fileName)
        {
            return System.IO.Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
        }
        public static string UniqueCleanFileName(this string fileName)
        {
            fileName = fileName.CleanFileName();
            var clearFileName = Regex.Replace(fileName.Replace(Path.GetExtension(fileName), ""), @"[^0-9a-zA-Z\u0600-\u06FF]+", "_");
            fileName = clearFileName + (clearFileName.EndsWith("_") ? "" : "_") + (new Random().Next(10000, 99999)).ToString() + Path.GetExtension(fileName);
            return fileName;
        }
        public static string CleanSearchTerm(this string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return null;
            searchTerm = searchTerm.Trim().Replace("أ", "ا").Replace("إ", "ا").Replace("آ", "ا").Replace("ى", "ي").Replace("چ", "ج").Replace("ڤ", "ف").Replace("پ", "ب").Replace("٠", "0").Replace("١", "1").Replace("٢", "2").Replace("٣", "3").Replace("٤", "4").Replace("٥", "5").Replace("٦", "6").Replace("٧", "7").Replace("٨", "8").Replace("٩", "9");
            return searchTerm;
        }
        public static DateTime? GetCalculatedBirthDate(this long? nationalId)
        {
            DateTime? result = null;
            int? yyyy = null;
            int? MM = null;
            int? dd = null;
            var strNationalId = nationalId != null ? nationalId.ToString() : "";
            if (strNationalId.Length < 7)
            {
                result = null;
            }
            else
            {
                yyyy = 1800 + ((int.Parse(strNationalId.Substring(0, 1)) - 1) * 100) + int.Parse(strNationalId.Substring(1, 2));
                MM = int.Parse(strNationalId.Substring(3, 2));
                dd = int.Parse(strNationalId.Substring(5, 2));
                try
                {
                    result = new DateTime(yyyy ?? 0, MM ?? 0, dd ?? 0);
                }
                catch
                {

                    result = null;

                }
            }
            return result;
        }
        public static DateTime NextDayOfWeek(this DateTime dt, DayOfWeek dayOfWeek)
        {
            int offsetDays = dayOfWeek - dt.DayOfWeek;
            return dt.AddDays(offsetDays > 0 ? offsetDays : offsetDays + 7);
        }
        public static DateTime PreviousDayOfWeek(this DateTime dt, DayOfWeek dayOfWeek)
        {
            int offsetDays = -(dt.DayOfWeek - dayOfWeek);
            return dt.AddDays(offsetDays);
        }
    }
}
