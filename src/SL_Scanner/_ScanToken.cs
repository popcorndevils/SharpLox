
public partial class SL_Scanner
{
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
                this.AddToken(Token.Type.LEFT_PAREN);
                break;
            case ')':
                this.AddToken(Token.Type.RIGHT_PAREN);
                break;
            case '{':
                this.AddToken(Token.Type.LEFT_BRACE);
                break;
            case '}':
                this.AddToken(Token.Type.RIGHT_BRACE);
                break;
            case ',':
                this.AddToken(Token.Type.COMMA);
                break;
            case '.':
                this.AddToken(Token.Type.DOT);
                break;
            case '-':
                this.AddToken(Token.Type.MINUS);
                break;
            case '+':
                this.AddToken(Token.Type.PLUS);
                break;
            case ';':
                this.AddToken(Token.Type.SEMICOLON);
                break;
            case '*':
                this.AddToken(Token.Type.STAR);
                break;
            // TWO CHARACTER LEXEMES
            case '!':
                this.AddToken(this.Match('=') ? Token.Type.BANG_EQUAL : Token.Type.BANG);
                break;
            case '=':
                this.AddToken(this.Match('=') ? Token.Type.EQUAL_EQUAL : Token.Type.EQUAL);
                break;
            case '<':
                this.AddToken(this.Match('=') ? Token.Type.GREATER_EQUAL : Token.Type.GREATER);
                break;
            case '>':
                this.AddToken(this.Match('=') ? Token.Type.LESS_EQUAL : Token.Type.LESS);
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
                    this.AddToken(Token.Type.SLASH);
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
                    this.AddToken(Token.Type.STRING, _string);
                }
                break;
            // ERROR
            default:
                if(c.IsDigit())
                {
                    var _number = this.GetNumber();
                    this.AddToken(Token.Type.NUMBER, _number);
                }
                else if(c.IsAlpha())
                {
                    Token.Type _token;

                    string _identifier = this.GetIdentifier();

                    if(Token.Keywords.ContainsKey(_identifier))
                    {
                        _token = Token.Keywords[_identifier];
                    }
                    else
                    {
                        _token = Token.Type.IDENTIFIER;
                    }
                    
                    this.AddToken(_token);
                }
                else
                {
                    SL_Error.Error(this.Line, "Unexpected Character");
                }
                break;
        }
    }
}