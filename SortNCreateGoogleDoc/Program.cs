using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SortNCreateGoogleDoc
{
    class Program
    {
        static string Seperator = ", ";
        static string SettingsDirectory = Environment.CurrentDirectory + @"\settings";
        static string SortOrderFile = SettingsDirectory + @"\sortorder.txt";
        static string FilterFile = SettingsDirectory + @"\filter.txt";
        static void Main(string[] args)
        {
            Console.WriteLine("Input names:");
            string[] names = Console.ReadLine().Split(Seperator);
            Console.WriteLine(SettingsDirectory);
            if (!Directory.Exists(SettingsDirectory)) Directory.CreateDirectory(SettingsDirectory);
            if (!File.Exists(SortOrderFile)) File.CreateText(SortOrderFile);
            if (!File.Exists(FilterFile)) File.CreateText(FilterFile);
            names = FilterFromArray(names, File.ReadAllLines(FilterFile));
            SortArrayByArray(names, File.ReadAllLines(SortOrderFile));
            Console.WriteLine("result:");
            Console.WriteLine(ToStringArray(names));
        }

        static string ToStringArray<T>(T[] arr)
        {
            if (arr.Length == 0) return "";
            StringBuilder result = new StringBuilder();
            for(int i = 0; i < arr.Length - 1; i++)
            {
                result.Append(arr[i].ToString() + Seperator);
            }
            result.Append(arr[arr.Length - 1].ToString());
            return result.ToString();
        }

        static string[] FilterFromArray(string[] arr, string[] values)
        {
            Stack<string> stack = new Stack<string>();
            for(int i = 0; i < arr.Length; i++)
            {
                string cur = arr[i];
                for(int x = 0; x < values.Length; x++)
                {
                    string value = values[x];
                    if (cur.StartsWith(value))
                    {
                        cur = cur.Substring(value.Length);
                        x = -1;
                    }
                }
                if (cur != "") stack.Push(cur);
            }
            string[] result = new string[stack.Count];
            for(int i = result.Length - 1; i > -1; i--)
            {
                result[i] = stack.Pop();
            }
            return result;
        }

        static void SortArrayByArray(string[] arr, string[] by)
        {
            for(int i = 1; i < arr.Length; i++)
            {
                string value = arr[i];
                int order = IndexOf(by, value);
                int cur = i - 1;
                string curvalue = arr[cur];
                while (order < IndexOf(by, curvalue))
                {
                    arr[cur] = value;
                    arr[cur + 1] = curvalue;
                    cur--;
                    if (cur > -1) curvalue = arr[cur];
                    else break;
                }
            }
        }

        static int IndexOf(string[] arr, string value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (value.StartsWith(arr[i])) return i;
            }
            return arr.Length;
        }
    }
}
 