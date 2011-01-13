#region License

// TweetSharp
// Copyright (c) 2010 Daniel Crenna and Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TweetSharp.Core.Extensions;
using TweetSharp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TweetSharp.Twitter.Core;
using TweetSharp.Twitter.Model;

#if SILVERLIGHT
using Hammock.Silverlight.Compat; // Trace
#endif

namespace TweetSharp.Twitter.Extensions
{
    public static partial class TwitterExtensions
    {
        static TwitterExtensions()
        {
            SerializationExtensions.Serializer = new TwitterModelSerializer();
        }

        internal static string ToTwitterResponseString(this string value)
        {
            if (value.IsNullOrBlank())
            {
                return string.Empty;
            }

            if (value.First() == '"')
            {
                value = value.Substring(1);
            }

            if (value.Last() == '"')
            {
                value = value.Substring(0, value.Length - 1);
            }

            return value;
        }

        internal static string ToTwitterDateString(this DateTime date)
        {
            // Tue, 27 Mar 2007 22:55:48 GMT
            var result = date.ToString(
                "ddd, dd MMM yyyy H':'mm':'ss 'GMT'".UrlEncodeRelaxed(), CultureInfo.InvariantCulture
                );
            return result;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterUser" /> 
        /// collection instance. 
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static IEnumerable<TwitterUser> AsUsers(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            JContainer collection = null;
            if (result.Response.StartsWith("["))
            {
                collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
            }
            else
            {
                var jobj = JObject.Parse(result.Response);
                var usersJson = jobj.FindSingleChildProperty("users");
                if (usersJson != null)
                {
                    collection =
                        (JContainer) JsonConvert.DeserializeObject(usersJson.Children().ToArray()[0].ToString());
                }
            }
            if (collection == null)
            {
                return null;
            }

            var users = DeserializeChildren<TwitterUser>(collection);

            return users;
        }

        private static IEnumerable<T> DeserializeChildren<T>(JToken collection) 
            where T : class, IModel 
        {
            var children = new List<T>(0);

            foreach (var item in collection.Children())
            {
                try
                {
                    var json = item.ToString();
                    var child = json.Deserialize<T>();
                    if (child != null)
                    {
                        children.Add(child);
                        child.RawSource = json;
                    }
                    else
                    {
                        Trace.WriteLine("Failed to deserialize child:\n" + json);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine("Exception encountered during deserialization:\n" + ex.Message);
                    return null;
                }
            }

            return children;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterUser" />
        /// instance.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static TwitterUser AsUser(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var user = result.Response.Deserialize<TwitterUser>();
            return user;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterList" />
        /// instance.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A user instance, or null if the input cannot cast to a list</returns>
        public static TwitterList AsList(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var list = result.Response.Deserialize<TwitterList>();
            return list;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterList" /> collection
        /// instance. 
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static IEnumerable<TwitterList> AsLists(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var jObject = JObject.Parse(result.Response);
                var listsJson = jObject.FindSingleChildProperty("lists");
                if (listsJson != null)
                {
                    collection = (JContainer) JsonConvert.DeserializeObject(listsJson.Children().ToArray()[0].ToString());
                }

                var lists = DeserializeChildren<TwitterList>(collection);
                return lists;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterStatus" /> collection
        /// instance. 
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns>A status collection instance, or null if the input cannot cast to a status collection</returns>
        public static IEnumerable<TwitterStatus> AsStatuses(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

            // Streaming collector
            if (result.IsFromStream)
            {
                var responses = result.StreamedResponses;
                return new List<TwitterStatus>(
                    responses.Select(
                    response =>
                        {
                            var jObject = JObject.Parse(response);
                            var deleted = jObject.FindSingleChildProperty("delete");
                            if (deleted != null)
                            {
                                // {"delete":{"status":{"id":14365426301,"user_id":23169084}}}
                                return null;
                            }
                            var status = new TwitterResult { Response = response }.AsStatus();
                            return status;
                        }).
                    Where(status => status != null)
                );
            }

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var statuses = DeserializeChildren<TwitterStatus>(collection);
                return statuses;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        /// <summary>
        /// Casts the result into a collection of Where On Earth (WOE) locations.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static IEnumerable<WhereOnEarthLocation> AsWhereOnEarthLocations(this TwitterResult result)
        {
            if (result.IsFailWhale)
            {
                return null;
            }

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var locations = DeserializeChildren<WhereOnEarthLocation>(collection);

                return locations;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }


        /// <summary>
        /// Casts the result as a single <see cref="TwitterStatus" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static TwitterStatus AsStatus(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var status = result.Response.Deserialize<TwitterStatus>();
            return status;
        }

        /// <summary>
        /// Casts the result as a single <see cref="TwitterSavedSearch" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static TwitterSavedSearch AsSavedSearch(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var savedSearch = result.Response.Deserialize<TwitterSavedSearch>();
            return savedSearch;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterSavedSearch" /> collection
        /// instance.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static IEnumerable<TwitterSavedSearch> AsSavedSearches(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            try
            {
                var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return null;
                }

                var savedSearches = DeserializeChildren<TwitterSavedSearch>(collection);
                return savedSearches;
            }
            catch (Exception /*ex*/)
            {
                return null;
            }
        }

        /// <summary>
        /// Casts the result as a single <see cref="TwitterFriendship" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static TwitterFriendship AsFriendship(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var friendship = result.Response.Deserialize<TwitterFriendship>();
            return friendship;
        }

        /// <summary>
        /// Casts the result as a collection of <see cref="TwitterDirectMessage" /> instances.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static IEnumerable<TwitterDirectMessage> AsDirectMessages(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var collection = (JContainer) JsonConvert.DeserializeObject(result.Response);
            if (collection == null)
            {
                return null;
            }

            var dms = DeserializeChildren<TwitterDirectMessage>(collection);
            return dms;
        }

        /// <summary>
        /// Casts the result as a single <see cref="TwitterDirectMessage" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static TwitterDirectMessage AsDirectMessage(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            var dm = result.Response.Deserialize<TwitterDirectMessage>();
            return dm;
        }

        /// <summary>
        /// Casts the result as a single <see cref="TwitterRateLimitStatus" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static TwitterRateLimitStatus AsRateLimitStatus(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            string response;
            if (result.Response.IsXml(out response))
            {
                return DeserializeRateLimitStatusXml(response);
            }
#endif

            var limit = result.Response.Deserialize<TwitterRateLimitStatus>();
            return limit;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterError" />
        /// instance. If this method is not successful, null is returned.
        /// 
        /// Twitter is now returning two inconsistent schemas for errors.
        /// One is a collection. In this case, only the first element is returned,
        /// because we have only seen single entry collections to date.
        /// 
        /// At some point, we assume the API will change across the board, and we
        /// will change this deserialization method to follow suit.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static TwitterError AsError(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            string response;
            if (result.Response.IsXml(out response))
            {
                return DeserializeErrorXml(response);
            }
#endif
            // New schema handling
            try
            {
                var collection = (JContainer)JsonConvert.DeserializeObject(result.Response);
                if (collection == null)
                {
                    return AsOldError(result);
                }

                var jObject = JObject.Parse(result.Response);
                var errorsJson = jObject.FindSingleChildProperty("errors");
                if (errorsJson != null)
                {
                    collection = (JContainer)JsonConvert.DeserializeObject(errorsJson.Children().ToArray()[0].ToString());
                }
                else
                {
                    return AsOldError(result);
                }

                var errors = DeserializeChildren<TwitterError>(collection);
                return errors.FirstOrDefault();
            }
            catch (Exception /*ex*/)
            {
                return AsOldError(result);
            }
        }

        // Normal flow from old version of the error schema
        private static TwitterError AsOldError(TweetSharpResult result)
        {
            var error = result.Response.Deserialize<TwitterError>(false /* traceError */);
            if (error == null || (error.ErrorMessage == null && error.Request == null))
            {
                return null;
            }
            return error;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a <see cref="TwitterSearchResult" />
        /// instance. If this method is not successful, null is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static TwitterSearchResult AsSearchResult(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var searchResult = result.Response.Deserialize<TwitterSearchResult>();
            return searchResult;
        }

#if !Mono
        // [DC] This method causes the Mono compiler to throw a stack overflow exception

        /// <summary>
        /// This method attempts to cast JSON string into a <see cref="TwitterSearchTrends" /> instance. 
        /// </summary>
        /// <param name="result">The JSON input to convert</param>
        /// <returns></returns>
        public static TwitterSearchTrends AsSearchTrends(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif

            //FIX: This isn't pretty, but it gets this stuff working
            //The problem is that the json returned from the search api has a 
            //dynamically named property (a date/time) that we can't know ahead
            //of time. That property is also the parent of the trend objects, 
            //so we can't ignore it either.  This removes that property from the
            //json and reparents the trend objects to the "trends" property 
            //before passing it off to the deserializer to get turned into 
            //objects in our model. 
            var fullResponseObject = JObject.Parse(result.Response);

            JProperty trendsProp;

            // get the outer scoping trends object
            var trendsProperties = from JProperty p
                                       in fullResponseObject.Properties()
                                   where p.Name.ToLower() == "trends"
                                   select p;

            var coalescedArray = new JArray();

            if (trendsProperties.Count() > 0)
            {
                trendsProp = trendsProperties.ToArray()[0];
                if (trendsProp.Value.Type != JTokenType.Array)
                {
                    var trendsObject = (JObject) trendsProp.Value;
                    foreach (var trendsArray in trendsObject.Properties())
                    {
                        DateTime trendingDate;
#if Smartphone 
                        bool parsedDate = true; 
                        try
                        {
                            trendingDate = DateTime.Parse(trendsArray.Name);
                        }
                        catch( FormatException /*ex*/ )
                        {
                            parsedDate = false; 
                            trendingDate = DateTime.MinValue;
                        }
#else
                        var parsedDate = DateTime.TryParse(trendsArray.Name, out trendingDate);
#endif
                        if (parsedDate)
                        {
                            foreach (JObject child in trendsArray.Value)
                            {
                                child.Add("trending_as_of",
                                          new JValue(TwitterDateTime.ConvertFromDateTime(trendingDate,
                                                                                         TwitterDateFormat.TrendsCurrent)));
                                coalescedArray.Add(child);
                            }
                        }
                    }
                    trendsProp.Value = coalescedArray;
                }
            }

            //convert the as_of date
            var asOf = fullResponseObject.FindSingleChildProperty("as_of");
            if (asOf != null)
            {
                var val = asOf.Value;
                if (val.Type == JTokenType.Integer)
                {
                    var date = new DateTime(1970, 1, 1).AddSeconds((int) val);
                    asOf.Value = new JValue(TwitterDateTime.ConvertFromDateTime(date, TwitterDateFormat.TrendsCurrent));
                }
            }

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            fullResponseObject.WriteTo(writer);
            writer.Close();
            stringWriter.Dispose();
            result.Response = sb.ToString();
            var trends = result.Response.Deserialize<TwitterSearchTrends>();
            return trends;
        }
#endif

        /// <summary>
        /// Casts the result as a single <see cref="TwitterLocalTrends" />.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static TwitterLocalTrends AsLocalTrends(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            // [DC]: Always in array form for some reason (global trends does not return multiple items)
            var trends = JArray.Parse(result.Response).FirstOrDefault();
            return trends == null
                       ? null
                       : trends.ToString().Deserialize<TwitterLocalTrends>();
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into a list of IDs.
        /// If this method is not successful, an empty list is returned.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static List<long> AsIds(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var ids = new List<long>();
            var jobj = JObject.Parse(result.Response);

            var prop = jobj.FindSingleChildProperty("ids");
            if (prop != null)
            {
                ids = prop.Value.ToString().DeserializeAny<List<long>>();
            }
            return ids;
        }

        /// <summary>
        /// This method attempts to cast an XML or JSON string into an arbitrary class instance.
        /// </summary>
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static T As<T>(this TwitterResult result) where T : class
        {
            if (result.IsFailWhale) return default(T);
#if !SILVERLIGHT
            result.Response = PreProcessXml(result.Response);
#endif
            var instance = result.Response.DeserializeAny<T>();
            return instance;
        }

        /// <summary>
        /// This method attempts to cast a string response into an <see cref="OAuthToken" />.
        /// </summary>
        /// <param name="result">The XML or JSON result to convert</param>
        /// <returns></returns>
        public static OAuthToken AsToken(this TwitterResult result)
        {
            return result.IsFailWhale ? null : result.Response.AsToken();
        }

        /// This method attempts to cast an XML or JSON string into an <see cref="long"/> to be used with the paging of friend/follower ids. 
        /// If this method is not successful, 0 is returned.
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static long? AsNextCursor(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

            try
            {
#if !SILVERLIGHT
                result.Response = PreProcessXml(result.Response);
#endif
                var jobj = JObject.Parse(result.Response);
                const string cursor = "next_cursor";
                return FetchCursor(jobj, cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                return null;
            }
        }

        /// This method attempts to cast an XML or JSON string into an <see cref="long"/> to be used with the paging of friend/follower ids. 
        /// If this method is not successful, 0 is returned.
        /// <param name="result">The XML or JSON input to convert</param>
        /// <returns></returns>
        public static long? AsPreviousCursor(this TwitterResult result)
        {
            if (result.IsFailWhale) return null;

            try
            {
#if !SILVERLIGHT
                result.Response = PreProcessXml(result.Response);
#endif
                var jobj = JObject.Parse(result.Response);
                const string cursor = "previous_cursor";
                return FetchCursor(jobj, cursor);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error encountered trying to deserialize JSON: {0}", ex);
                return null;
            }
        }

        private static long? FetchCursor(JToken token, string cursor)
        {
            long? result = null;

            var property = token.FindSingleChildProperty(cursor);
            long val;

#if !Smartphone
            if (long.TryParse(property.Value.ToString().Replace("\"", ""), out val))
            {
                result = val;
            }
#else
            try
            {
                var propertyValue = property.Value.ToString().Replace("\"", "").ToString();
                val = Convert.ToInt64(propertyValue, CultureInfo.InvariantCulture);
                result = val; 
            }
            catch (Exception)
            {
                throw;
            }
#endif
            return result;
        }
    }
}