public partial class SL_Scanner
{
    public double? GetNumber()
    {
        // consume all digits after the first
        while(this.IsDigit(this.Peek()))
        {
            this.Advance();
        }

        // when running out of digits, check for decimal with digit afterwards
        // only consume '.' if followed by a digit, and only the first time for each number Token
        if(this.Peek() == '.' && this.IsDigit(this.Peek(1)))
        {
            this.Advance();
            while(this.IsDigit(this.Peek()))
            {
                this.Advance();
            }
        }

        return double.Parse(this.Source.Substring(this.Start, this.Current - this.Start));
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

    public bool IsDigit(char c)
    {
        return c >= '0' && c <= '9';
    }
}