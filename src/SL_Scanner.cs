using System.Collections.Generic;

public class SL_Scanner
{
    private readonly string Source;
    private readonly char[] Chars;
    private List<Token> Tokens = new List<Token>();

    private int Start = 0;
    private int Current = 0;
    private int Line = 1;

    public SL_Scanner(string source)
    {
        this.Source = source;
        this.Chars = source.ToCharArray();
        this.ScanTokens();
    }

    private void ScanTokens()
    {
        while(!this.IsAtEnd())
        {
            this.Start = this.Current;
            this.ScanToken();
        }
        this.Tokens.Add(new Token(TokenType.EOF, "", null, this.Line));
    }

    private bool IsAtEnd()
    {
        return this.Current >= this.Source.Length;
    }

    private char Advance()
    {
        return this.Chars[this.Current++];
    }

    private void AddToken(TokenType type)
    {
        this.AddToken(type, null);
    }

    private void AddToken(TokenType type, object? literal)
    {
        string text = this.Source.Substring(this.Start, this.Current);
        this.Tokens.Add(new Token(type, text, literal, this.Line));
    }

    public void ScanToken()
    {
        char c = this.Advance();
        switch(c)
        {
            case '(':
                this.AddToken(TokenType.LEFT_PAREN);
                break;
            case ')':
                this.AddToken(TokenType.RIGHT_PAREN);
                break;
            case '{':
                this.AddToken(TokenType.LEFT_BRACE);
                break;
            case '}':
                this.AddToken(TokenType.RIGHT_BRACE);
                break;
            case ',':
                this.AddToken(TokenType.COMMA);
                break;
            case '.':
                this.AddToken(TokenType.DOT);
                break;
            case '-':
                this.AddToken(TokenType.MINUS);
                break;
            case '+':
                this.AddToken(TokenType.PLUS);
                break;
            case ';':
                this.AddToken(TokenType.SEMICOLON);
                break;
            case '*':
                this.AddToken(TokenType.STAR);
                break;
            default:
                SL_Error.Error(this.Line, "Unexpected Character");
                break;
        }
    }

    public static string[] Tokenize(string source)
    {
        return source.Split(' ');
    }
}