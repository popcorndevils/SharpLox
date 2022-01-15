public partial class SL_Scanner
{
    /// <summary>
    /// Return the character at the current position and increment the current variable by 1.
    /// </summary>
    /// <returns>char object at current position.</returns>
    private char Advance()
    {
        // this logic is susceptible to error if the calling function does not check if the file is at the end first.
        return this.Chars[this.Current++];
    }
    
    public char Peek(int dist = 0)
    {
        if(this.IsAtEnd(dist))
        {
            return '\0';
        }
        else
        {
            return this.Source[this.Current + dist];
        }
    }
    
    public bool Match(char expected, int dist = 0)
    {
        if(this.IsAtEnd(dist))
        {
            return false;
        }
        else if(this.Source[this.Current + dist] != expected)
        {
            return false;
        }

        this.Current++;
        return true;
    }
}