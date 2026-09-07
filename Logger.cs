using System;
using System.IO;

static class Logger
{
    private const string LogFile = "error.log";

    public static void Log(string message)
    {
        try
        {
            string text = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(LogFile, text);
        }
        catch
        {
            // Swallow logging exceptions to avoid recursive failures
        }
    }

    public static void Log(Exception ex)
    {
        Log(ex.ToString());
    }
}
