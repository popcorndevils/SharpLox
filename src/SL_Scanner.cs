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
            // set start to current position so we can grab the whole substring when we have a multicharacter token.
            this.Start = this.Current;
            this.ScanToken();
        }
        this.Tokens.Add(new Token(TokenType.EOF, "", null, this.Line));
    }

    private bool IsAtEnd()
    {
        return this.Current >= this.Source.Length;
    }

    /// <summary>
    /// Return the character at the current position and increment the current variable by 1.
    /// </summary>
    /// <returns>char object at current position.</returns>
    private char Advance()
    {
        // this logic is susceptible to error if the calling function does not check if the file is at the end first.
        return this.Chars[this.Current++];
    }

    /// <summary>
    /// Add new token to internal list.
    /// </summary>
    /// <param name="type">Type of token being added.</param>
    /// <param name="literal">Optional object representing the value being stored.</param>
    private void AddToken(TokenType type, object? literal = null)
    {
        string text = this.Source.Substring(this.Start, this.Current);
        this.Tokens.Add(new Token(type, text, literal, this.Line));
    }

    /// <summary>
    /// Read the next character from the source code and ingest as token when possible.
    /// </summary>
    public void ScanToken()
    {
        char c = this.Advance();
        switch(c)
        {
            // SINGLE CHAR LEXEMES
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
            // TWO CHARACTER LEXEMES
            case '!':
                this.AddToken(this.Match('=') ? TokenType.BANG_EQUAL : TokenType.BANG);
                break;
            case '=':
                this.AddToken(this.Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
                break;
            case '<':
                this.AddToken(this.Match('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER);
                break;
            case '>':
                this.AddToken(this.Match('=') ? TokenType.LESS_EQUAL : TokenType.LESS);
                break;
            // COMPLEX LEXEMES
            case '/':
                if(this.Match('/'))
                {
                    // if there are two '/'s, this is a comment.  Just consume the rest of the characters on the line.
                    while(this.Peek() != '\n' && !this.IsAtEnd())
                    {
                        this.Current++;
                    }
                }   
                else 
                {
                    this.AddToken(TokenType.SLASH);
                }
                break;
            // WHITESPACE
            case ' ':
            case '\t':
            case '\r':
                break;
            case '\n':
                this.Line++;
                break;
            // ERROR
            default:
                SL_Error.Error(this.Line, "Unexpected Character");
                break;
        }
    }

    public static string[] Tokenize(string source)
    {
        return source.Split(' ');
    }

    public bool Match(char expected)
    {
        if(this.IsAtEnd())
        {
            return false;
        }
        else if(this.Source[this.Current] != expected)
        {
            return false;
        }

        this.Current++;
        return true;
    }

    public char Peek()
    {
        if(this.IsAtEnd())
        {
            return '\0';
        }
        else
        {
            return this.Source[this.Current];
        }
    }
}