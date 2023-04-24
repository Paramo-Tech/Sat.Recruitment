using System.Text.RegularExpressions;

namespace Sat.Recruitment.Services.Commands.Imp
{
    public class NormalizeEmailCommand : INormalizeEmailCommand
    {
        private const string RegexRemoveDots = @"\.(?=[^@]*@)";
        private const string RegexRemovePlusSymbolAndText = @"\+.*(?=@)";

        public string Execute(string email)
        {
            email = email.ToLower().Trim();

            // Remove any dots before the "@" symbol
            email = Regex.Replace(email, RegexRemoveDots, string.Empty);

            // Remove any plus signs and text after the "+" symbol
            return Regex.Replace(email, RegexRemovePlusSymbolAndText, string.Empty);
        }
    }
}