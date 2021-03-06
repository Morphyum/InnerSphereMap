﻿using System;
using System.IO;

namespace InnerSphereMap {
    public class Logger {
        public static void LogError(Exception ex) {
            var filePath = $"{InnerSphereMap.ModDirectory}/Log.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true)) {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }

        public static void LogLine(String line) {
            var filePath = $"{InnerSphereMap.ModDirectory}/Log.txt";
            using (StreamWriter writer = new StreamWriter(filePath, true)) {
                writer.WriteLine(line + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}