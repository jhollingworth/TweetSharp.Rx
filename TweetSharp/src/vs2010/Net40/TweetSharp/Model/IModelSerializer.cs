using Newtonsoft.Json;

namespace TweetSharp.Model
{
    /// <summary>
    /// Defines the contract for a serializer for model classes.
    /// </summary>
    public interface IModelSerializer
    {
        /// <summary>
        /// Gets the JSON.NET serializer settings.
        /// </summary>
        /// <value>The settings.</value>
        JsonSerializerSettings Settings { get; }
    }
}