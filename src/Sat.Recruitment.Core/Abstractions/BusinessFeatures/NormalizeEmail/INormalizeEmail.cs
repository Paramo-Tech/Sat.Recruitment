namespace Sat.Recruitment.Core.Abstractions.BusinessFeatures.NormalizeEmail
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
    public interface INormalizeEmail
    {
        /// <summary>
        /// Normalizes an email executing the logic that corresponds
        /// according to the domain of the email.
        /// </summary>
        string Normalize(string email);
    }
}