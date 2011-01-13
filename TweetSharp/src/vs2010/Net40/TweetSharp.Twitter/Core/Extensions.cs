using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetSharp.Model;
using TweetSharp.Core.Extensions;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Model.Converters;

namespace TweetSharp.Twitter.Core
{
    ///<summary>
    /// Core extensions shared between friendly assemblies.
    ///</summary>
    public static class Extensions
    {
        internal static OAuthToken AsToken(this TwitterResult result)
        {
            return result.Response.AsToken();
        }

        internal static JProperty FindSingleChildProperty(this JToken startToken, string propertyName)
        {
            if(!startToken.HasValues)
            {
                return null;
            }
            JProperty ret = null;
            var props = from JProperty p in startToken.Children().OfType<JProperty>()
                        where p.Name.ToLower() == propertyName
                        select p;

            if (!props.Any())
            {
                foreach (var token in startToken.Children())
                {
                    ret = FindSingleChildProperty(token, propertyName);
                    if (ret != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                ret = props.ToArray()[0];
            }
            return ret;
        }

        internal static JObject FindSingleChildObject(this JToken startToken, string objectName)
        {
            JObject ret = null;
            var props = from JObject o
                            in startToken.Children().OfType<JObject>()
                        where o["Name"].Value<string>() == objectName
                        select o;

            if (!props.Any())
            {
                foreach (var token in startToken.Children())
                {
                    ret = FindSingleChildObject(token, objectName);
                    if (ret != null)
                    {
                        break;
                    }
                }
            }
            else
            {
                ret = props.ToArray()[0];
            }
            return ret;
        }

        /// <summary>
        /// Converts an <see cref="IModel"/> into a JSON string.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string ToJson(this IModel instance)
        {
            var json = JsonConvert.SerializeObject(instance,
                                                   new TwitterDateTimeConverter(),
                                                   new TwitterWonkyBooleanConverter());

            return json;
        }

        /// <summary>
        /// Converts an <see cref="IModel"/> collection into a JSON string.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public static string ToJson(this IEnumerable<IModel> collection)
        {
            var json = JsonConvert.SerializeObject(collection,
                                                   new TwitterDateTimeConverter(),
                                                   new TwitterWonkyBooleanConverter());

            return json;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Converts a <see cref="TwitterResult" /> into a JSON string.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static string ToJson(this TwitterResult result)
        {
            return TwitterExtensions.PreProcessXml(result.Response);
        }
#endif
    }
}
