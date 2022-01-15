using System;
using System.IO;
using System.Linq;

public class SharpLox
{
    static int Main(string[] args)
    {
        if(args.Count() > 1)
        {
            SL_Error.Error(0, "Usage: SharpLox [script]");
        }
        else if(args.Count() == 1)
        {
            SharpLox.RunFile(args[0]);
        }
        else
        {
            SharpLox.RunPrompt();
        }

        return SL_Error.ERROR_STATUS;
    }

    static void RunFile(string path)
    {
        string code = File.ReadAllText(path);
        SharpLox.Run(code);
    }

    static void RunPrompt()
    {
        SL_Error.ERROR_STATUS = 0;
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
        var _scan = new Scanner(code);
        if(SL_Error.ERROR_STATUS == 0)
        {
            foreach(Token token in _scan.Tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}