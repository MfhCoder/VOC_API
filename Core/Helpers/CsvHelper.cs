namespace Core.Helpers
{
    public static class CsvHelper
    {
        public static string EscapeCsv(string? field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            if (field.Contains(',') || field.Contains('"'))
                return $"\"{field.Replace("\"", "\"\"")}\"";
            return field;
        }
    }
}
