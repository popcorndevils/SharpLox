using System.Collections.Generic;

public class SL_Scanner
{
    private readonly string Source;
    private readonly char[] Chars;
    public List<Token> Tokens = new List<Token>();

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
        string text = this.Source.Substring(this.Start, this.Current - this.Start);
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
                    // it's important not to consume the \n character, so the scanner can pick it up on the next pass.
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
            // ignore most whitespace
            case ' ':
            case '\t':
            case '\r':
                break;
            // increment line counter when reading a new line.
            case '\n':
                this.Line++;
                break;
            // LITERALS
            case '"':
                var _string = this.GetString();
                if(_string == null)
                {
                    SL_Error.Error(this.Line, "Unterminated String");
                }
                else
                {
                    this.AddToken(TokenType.STRING, _string);
                }
                break;
            // ERROR
            default:
                SL_Error.Error(this.Line, "Unexpected Character");
                break;
        }
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

    public string? GetString()
    {
        while(this.Peek() != '"' && !this.IsAtEnd())
        {
            if(this.Peek() == '\n')
            {
                // this is normally handled in ScanToken, but this is easier when allowing multiline strings.
                this.Line++;
            }
            this.Advance();
        }
        
        if(this.IsAtEnd())
        {
            return null;
        }

        // advance to consume closing "
        this.Advance(); 

        if(this.Current <= this.Start)
        {
            return "";
        }
        else
        {
            // strip off apostrophes from the actual string value for the literal
            int _start = this.Start + 1;
            int _end = this.Current - 1;
            return this.Source.Substring(_start, _end - _start);
        }
    }
}