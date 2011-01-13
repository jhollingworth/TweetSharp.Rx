using System;
using Newtonsoft.Json;
using TweetSharp.Model;
using TweetSharp.Yammer.Model.Converters;

namespace TweetSharp.Yammer.Model
{    
    internal class YammerModelSerializer : IModelSerializer
    {
        private static readonly JsonSerializerSettings _settings =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Include,
                DefaultValueHandling = DefaultValueHandling.Include,
                Converters = (new JsonConverter[]
                                      {
                                          new YammerDateConverter(),
                                          new YammerXmlNodeConverter(),
                                      })
            };

        public JsonSerializerSettings Settings
        {
            get { return _settings; }
        }
    }
}
