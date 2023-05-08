namespace Sat.Recruitment.Infrastructure.TextFile.Configuration
{
    /// <summary>
    /// Contract that defines the current configuration for the TextFile that will persist the information.
    /// </summary>
    public interface ITextFileConfiguration
    {
        /// <summary>
        /// Gets the file path where the text file should be located.
        /// </summary>
        /// <returns>String containing the full path to the file where the data will be persisted.</returns>
        string TextFilePath();
    }
}
