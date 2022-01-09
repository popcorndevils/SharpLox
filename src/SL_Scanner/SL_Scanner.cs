using System.Collections.Generic;

public partial class SL_Scanner
{
    /// <summary>
    /// Source as a string for substring capabilities.
    /// </summary>
    private readonly string Source;

    /// <summary>
    /// Source as a character array.
    /// </summary>
    private readonly char[] Chars;

    /// <summary>
    /// List of tokens generated as source is scanned.
    /// </summary>
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
}