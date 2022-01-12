
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
                if(this.IsDigit(c))
                {
                    var _number = this.GetNumber();
                    this.AddToken(TokenType.NUMBER, _number);
                }
                else if(this.IsAlpha(c))
                {
                    TokenType _token;

                    string _identifier = this.GetIdentifier();

                    if(LoxDefinitions.Keywords.ContainsKey(_identifier))
                    {
                        _token = LoxDefinitions.Keywords[_identifier];
                    }
                    else
                    {
                        _token = TokenType.IDENTIFIER;
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