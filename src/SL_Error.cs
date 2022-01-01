using System;

public static class SL_Error
{
    public static int ERROR_STATUS;
    
    public static void Error(int line, string message)
    {
        SL_Error.ERROR_STATUS = 1;
        SL_Error.Report(line, "", message);
    }

    public static void Report(int line, string where, string message)
    {
        var _msg = $"** line {line}: {where} {message} **";
        var _line = new string('*', _msg.Length);

        Console.WriteLine(_line);
        Console.WriteLine(_msg);
        Console.WriteLine(_line);
    }
}