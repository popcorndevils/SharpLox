
public static class CharExtensions
{
    public static bool IsAlpha(this char c)
    {
        return (c >= 'a' && c <= 'z') ||
               (c >= 'A' && c <= 'Z') ||
               (c == '_');
    }

    public static bool IsDigit(this char c)
    {
        return c >= '0' && c <= '9';
    }
    
    public static bool IsAlphaDigit(this char c)
    {
        return c.IsAlpha() || c.IsDigit();
    }
}