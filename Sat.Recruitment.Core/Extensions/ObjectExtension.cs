using System.Text.Json;

namespace Sat.Recruitment.Core.Extensions
{
    /// <summary>
    /// Extension for objects
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// Deep copy using serialization
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="objSource">Source object</param>
        /// <returns>Copy of source</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T CopyObject<T>(this object objSource)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                JsonSerializer.Serialize(stream, objSource);
                stream.Position = 0;
                return JsonSerializer.Deserialize<T>(stream)?? throw new ArgumentNullException(nameof(objSource));
            }
        }
    }
}
