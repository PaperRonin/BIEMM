using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;

namespace BIEMM
{
    public static class Logger
    {
        public static void ErrorLog(string msg, string stkTrace, string source)
        {
            var appPath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

            using var sw = new StreamWriter(new FileStream(appPath + "\\Error.log", FileMode.Append, FileAccess.Write));
            sw.WriteLine($"Source: {source}");

            sw.WriteLine($"Date / Time: {DateTime.Now}");

            sw.WriteLine($"Message: {msg}");

            sw.WriteLine($"StackTrace: {stkTrace}");

            sw.WriteLine(new string('=', 100));
        }

        public static void InfoLog(string info)
        {

            var appPath = Directory.GetParent(Assembly.GetEntryAssembly().Location).FullName;

            using var sw = new StreamWriter(new FileStream(appPath + "\\Info.log", FileMode.Append, FileAccess.Write));

            sw.WriteLine(info);
        }

    }


}
