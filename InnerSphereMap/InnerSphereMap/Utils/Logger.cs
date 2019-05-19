using System;
using System.IO;

namespace InnerSphereMap {
    public class Logger {

        private static StreamWriter LogStream;
        private static string LogFile;

        public Logger(string modDir, string logName) {
            if (LogFile == null) {
                LogFile = Path.Combine(modDir, $"{logName}.log");
            }
            if (File.Exists(LogFile)) {
                File.Delete(LogFile);
            }

            LogStream = File.AppendText(LogFile);
        }

        public void Exception(Exception e) {
            Info($"Exception caught: {e.Message}");
            Info($"  Stacktrace: {e.StackTrace}");
        }

        public void Debug(string message) { if (InnerSphereMap.SETTINGS.Debug) { Info(message); } }
        public void Trace(string message) { if (InnerSphereMap.SETTINGS.Trace) { Info(message); } }

        public void Info(string message) {
            string now = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);
            LogStream.WriteLine($"{now} - {message}");
            LogStream.Flush();
        }

    }
}
