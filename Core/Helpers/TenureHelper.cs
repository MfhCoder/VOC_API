namespace Core.Helpers
{
    public static class TenureHelper
    {
        public static int ParseTenureToDays(string? tenure)
        {
            if (string.IsNullOrWhiteSpace(tenure)) return 0;
            int years = 0, months = 0, days = 0;
            var parts = tenure.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                if (part.EndsWith("Y")) years = int.Parse(part.TrimEnd('Y'));
                else if (part.EndsWith("M")) months = int.Parse(part.TrimEnd('M'));
                else if (part.EndsWith("D")) days = int.Parse(part.TrimEnd('D'));
            }
            return years * 365 + months * 30 + days;
        }
    }
}
