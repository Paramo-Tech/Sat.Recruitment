namespace Sat.Recruitment.Application.Common.Extensions;

public static class StringExtensions
{
    public static string NormalizeEmail(this string email)
    {
        var aux = email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

        var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

        aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

        return string.Join("@", aux[0], aux[1]);
    }
}