namespace Sat.Recruitment.Api.Logic.Business
{
    public static class Extensions
    {
        public static bool IsNullOrEmptyOrWhiteSpaces(this string value)
        {
            return string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);
        }

    }
}
