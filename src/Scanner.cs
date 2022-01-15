using System.Collections.Generic;

public class Scanner
{
    private int _Start = 0;
    private int _Current = 0;
    private int _Line = 1;

    private string _Source;
    private char[] _Chars;
    
    public List<Token> Tokens = new List<Token>();

    public Scanner(string code)
    {
        this._Source = code;
        this._Chars = code.ToCharArray();
        this.ScanCode();
    }

    public void ScanCode()
    {
        while(this._Current < this._Source.Length)
        {
            this._Start = this._Current;
            
            var _token = this.GetNextToken();

            if(_token != null)
            {
                this.Tokens.Add(_token);
            }
        }

        this.Tokens.Add(new Token(Token.Type.EOF, "", null, this._Line));
    }

    public Token? GetNextToken()
    {
        Token? _output = null;

        char c = this.Advance();

        switch(c)
        {
            case '(':
                _output = new Token(Token.Type.LEFT_PAREN, this.GetLexeme(), null, this._Line);
                break;
            case ')':
                _output = new Token(Token.Type.RIGHT_PAREN, this.GetLexeme(), null, this._Line);
                break;
            case '{':
                _output = new Token(Token.Type.LEFT_BRACE, this.GetLexeme(), null, this._Line);
                break;
            case '}':
                _output = new Token(Token.Type.RIGHT_BRACE, this.GetLexeme(), null, this._Line);
                break;
            case ',':
                _output = new Token(Token.Type.COMMA, this.GetLexeme(), null, this._Line);
                break;
            case '.':
                _output = new Token(Token.Type.DOT, this.GetLexeme(), null, this._Line);
                break;
            case '-':
                _output = new Token(Token.Type.MINUS, this.GetLexeme(), null, this._Line);
                break;
            case '+':
                _output = new Token(Token.Type.PLUS, this.GetLexeme(), null, this._Line);
                break;
            case ';':
                _output = new Token(Token.Type.SEMICOLON, this.GetLexeme(), null, this._Line);
                break;
            case '*':
                _output = new Token(Token.Type.STAR, this.GetLexeme(), null, this._Line);
                break;
            
            default:
                SL_Error.Error(this._Line, $"Unexpected character: '{c}'");
                break;
        }

        return _output;
    }

    public char Advance()
    {
        return this._Chars[this._Current++];
    }

    public string GetLexeme()
    {
        return this._Source.Substring(this._Start, this._Current - this._Start);
    }
}