public static class NumberFormatter
{
    public static string FormatNumber(float num)
    {
        if (num >= 1_000_000_000)
            return (num / 1_000_000_000).ToString("0.##") + "B";
        else if (num >= 1_000_000)
            return (num / 1_000_000).ToString("0.##") + "M";
        else if (num >= 1_000)
            return (num / 1_000).ToString("0.##") + "K";
        else
            return num.ToString();
    }
}
