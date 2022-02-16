using Sat.Recruitment.Core.Abstractions.BusinessFeatures.NormalizeEmail;
using System;

namespace Sat.Recruitment.Core.BusinessRules.Features.NormalizeEmail
{
    /// <summary>
    /// Some mail providers add non-standard functionality to their nameusers,
    /// such as allowing them to use . interchangeably, or create labels with
    /// the + sign.
    /// 
    /// The responsibility of the implementations of this interface is to add
    /// the corresponding logic, according to each email provider, to keep a
    /// clean username, without labels or decorations, to avoid duplicates.
    /// </summary>
    internal class NormalizeEmail : INormalizeEmail
    {
        public string Normalize(string email)
        {
            #region Just comments for developers

            /* Initial comments:
             * 
             * After some quick research, I discovered that the use of the . and the + are
             * Gmail-specific implementations, and that there is no standard to support them,
             * so implementing the "Normalize Email" functionality to all email providers
             * could lead to the generation of invalid addresses.
             * 
             * An official source on Google Blog:
             *    https://gmail.googleblog.com/2008/03/2-hidden-ways-to-get-more-from-your.html
             *    
             * Then, the location of the dots matters for emails on Microsoft Outlook,
             * Yahoo Mail, and Apple iCloud, to mention some. Dots don’t matter for Facebook,
             * and they aren’t used at all for Twitter handles.
             */

            /* Strategy to solve the problem:
             * 
             * It seems that the functionality is completely applicable to mails that come
             * from gmail, but it is not applicable to the rest of the domains in an analogous
             * way. So, for now, the logic will be applied only to gmail emails, leaving the
             * original logic.
             * 
             * If new strategies start to appear that need to be applicable to different
             * providers, it will be necessary to rethink how to organize them (possibly use
             * a mediator between the different strategies for each email provider).
             * */

            #endregion // Just comments for developers

            // Ensure the email is in lowercase
            string lowercaseEmail = email.ToLower();

            /* Takes an string and splits it on the @ character, if any of the partitions
             * result in an empty array, it removes them from the result.
             * 
             * Important fact here: if the supplied string is not a string of type
             * somestring@anotherstring the algorithm will fail at runtime.
             */
            string[] splitEmail = lowercaseEmail.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);
            string username = splitEmail[0];
            string domain = splitEmail[1];

            if (domain == "gmail.com")
            {
                // Remove the dots in the username
                username = username.Replace(".", "");

                // Finds the location of the first occurrence of the + sign, and returns it
                // in with respect to a zero-based array. If the sign is not found, then return -1.
                int plusIndex = splitEmail[0].IndexOf("+", StringComparison.Ordinal);

                // If the string contains a + sign, then it removes everything to the right of
                // the + sign (including  the + sign)
                if (plusIndex != -1)
                {
                    username = username.Remove(plusIndex-1);
                }

                // Compose the email again with the two parts of the chain.
                return string.Join("@", new string[] { username, domain });
            }

            return email;
        }
    }
}
