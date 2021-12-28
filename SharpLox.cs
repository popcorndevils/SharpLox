using System;
using System.IO;
using System.Linq;

public class SharpLox
{
    public static int ERROR_STATUS = 0;

    static int Main(string[] args)
    {
        if(args.Count() > 1)
        {
            SharpLox.Error(0, "Usage: SharpLox [script]");
        }
        else if(args.Count() == 1)
        {
            SharpLox.RunFile(args[0]);
        }
        else
        {
            SharpLox.RunPrompt();
        }

        return SharpLox.ERROR_STATUS;
    }

    static void RunFile(string path)
    {
        string code = File.ReadAllText(path);
        SharpLox.Run(code);
    }

    static void RunPrompt()
    {
        SharpLox.ERROR_STATUS = 0;
        Console.Write(">> ");
        string? code = Console.ReadLine();
        if(code != null && code != "exit()")
        {
            SharpLox.Run(code);
            SharpLox.RunPrompt();
        }
    }

    static void Run(string code)
    {
        foreach(string token in Scanner.Tokenize(code))
        {
            Console.WriteLine(token);
        }
    }

    static void Error(int line, string message)
    {
        SharpLox.ERROR_STATUS = 1;
        SharpLox.Report(line, "", message);
    }

    static void Report(int line, string where, string message)
    {
        var _msg = $"** line {line}: {where} {message} **";
        var _line = new string('*', _msg.Length);

        Console.WriteLine(_line);
        Console.WriteLine(_msg);
        Console.WriteLine(_line);
    }
}