namespace Sat.Recruitment.DAL.utils
{
    public static class Helper
    {
        public static string Capitalize(string value)
        {
            string lower = value.ToLower();
            return $"{lower[0].ToString().ToUpper()}{lower.Substring(1)}";
        }
    }
}
