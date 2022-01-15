using System.Collections.Generic;

public partial class Token
{
    public readonly Type TokenType;
    public readonly string Lexeme;
    public readonly object? Literal;
    public readonly int Line;

    public enum Type
    {
        // single-character tokens
        LEFT_PAREN,
        RIGHT_PAREN,
        LEFT_BRACE,
        RIGHT_BRACE,
        COMMA,
        DOT,
        MINUS,
        PLUS,
        SEMICOLON,
        SLASH,
        STAR,

        // one-or-two-character tokens
        BANG,
        BANG_EQUAL,
        EQUAL,
        EQUAL_EQUAL,
        GREATER,
        GREATER_EQUAL,
        LESS,
        LESS_EQUAL,
        
        // literals
        IDENTIFIER,
        STRING,
        NUMBER,
        
        // keywords
        AND,
        CLASS,
        ELSE,
        FALSE,
        FUN,
        FOR,
        IF,
        NIL,
        OR,
        PRINT,
        RETURN,
        SUPER,
        THIS,
        TRUE,
        VAR,
        WHILE,

        EOF
    }
    
    public static readonly Dictionary<string, Type> Keywords = new Dictionary<string, Type>() {
        {"and", Type.AND},
        {"class", Type.CLASS},
        {"else", Type.ELSE},
        {"false", Type.FALSE},
        {"for", Type.FOR},
        {"fun", Type.FUN},
        {"if", Type.IF},
        {"nil", Type.NIL},
        {"or", Type.OR},
        {"print", Type.PRINT},
        {"super", Type.SUPER},
        {"this", Type.THIS},
        {"true", Type.TRUE},
        {"var", Type.VAR},
        {"while", Type.WHILE},
    };

    public Token(Type type, string lexeme, object? literal, int line)
    {
        this.TokenType = type;
        this.Lexeme = lexeme;
        this.Literal = literal;
        this.Line = line;
    }

    public override string ToString()
    {
        string _lexeme = this.Lexeme.ToString() == "" ? "null" : this.Lexeme.ToString();
        return $"{this.TokenType} {_lexeme} ({this.Literal ?? "null"})";
    }
}