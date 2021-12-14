using System;
using System.IO;

public class SharpLox
{
    static void Main(string[] args)
    {
        if(args.Count() > 1)
        {
            Console.WriteLine("Usage: SharpLox [script]");
        }
        else if(args.Count() == 1)
        {
            SharpLox.RunFile(args[0]);
        }
        else
        {
            SharpLox.RunPrompt();
        }
    }

    static void RunFile(string path)
    {
        string code = File.ReadAllText(path);
        SharpLox.Run(code);
    }

    static void RunPrompt()
    {
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
        Console.WriteLine(code);
    }
}