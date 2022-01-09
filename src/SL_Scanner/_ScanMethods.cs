public partial class SL_Scanner
{
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
}