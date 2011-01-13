using Newtonsoft.Json;
using TweetSharp.Model;
using TweetSharp.Twitter.Model.Converters;

namespace TweetSharp.Twitter.Model
{
    /// <summary>
    /// A class that manages serialization and deserialization of Twitter API entities.
    /// </summary>
    public class TwitterModelSerializer : IModelSerializer
    {
        private static readonly JsonSerializerSettings _settings =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                Converters = (new JsonConverter[]
                                      {
                                          new TwitterDateTimeConverter(),
                                          new TwitterWonkyBooleanConverter(),
                                          new TwitterGeoConverter()
                                      })
            };

        /// <summary>
        /// Gets the JSON.NET serializer settings.
        /// </summary>
        /// <value>The settings.</value>
        public JsonSerializerSettings Settings
        {
            get { return _settings; }
        }
    }
}
