using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TweetSharp.Model;

#if SILVERLIGHT
using Hammock.Silverlight.Compat;
#endif

namespace TweetSharp.Core.Extensions
{
    internal static class SerializationExtensions
    {
        public static IModelSerializer Serializer;
        
        internal static T Deserialize<T>(this string json) where T : class, IModel 
        {
            return json.Deserialize<T>(true);
        }

        internal static T DeserializeAny<T>(this string json) where T : class
        {
            return json.DeserializeAny<T>(true);
        }

        internal static T Deserialize<T>(this string json, bool traceError) where T : class, IModel
        {
            try
            {
                var deserialized = JsonConvert.DeserializeObject<T>(json, Serializer.Settings);
                deserialized.RawSource = json;
                return deserialized;
            }
            catch (JsonSerializationException jsEx)
            {
#if TRACE
                if (traceError)
                {
                    // Model mishap
                    Trace.WriteLine(jsEx.Message);
                    Trace.WriteLine(jsEx.StackTrace);
                    Trace.WriteLine("JSON:");
                    Trace.WriteLine(json);
                }
#endif
                return null;
            }
            catch (JsonReaderException jrEx)
            {
#if TRACE
                if (traceError)
                {
                    // Twitter might return a plain string
                    Trace.WriteLine(jrEx.Message);
                    Trace.WriteLine(jrEx.StackTrace);
                    Trace.WriteLine("JSON:");
                    Trace.WriteLine(json);
                }
#endif
                return null;
            }
            catch (Exception ex)
            {
#if TRACE
                if (traceError)
                {
                    // Collections have issues converting to singles
                    Trace.WriteLine(ex.Message);
                    Trace.WriteLine(ex.StackTrace);
                    Trace.WriteLine("JSON:");
                    Trace.WriteLine(json);
                }
#endif
                return null;
            }
        }

        internal static T DeserializeAny<T>(this string json, bool traceError) where T : class
        {
            try
            {
                var deserialized = JsonConvert.DeserializeObject<T>(json, Serializer.Settings);
                return deserialized;
            }
            catch (JsonSerializationException jsEx)
            {
#if TRACE
                if (traceError)
                {
                    // Model mishap
                    Trace.WriteLine(jsEx.Message);
                    Trace.WriteLine(jsEx.StackTrace);
                    Trace.WriteLine("JSON:");
                    Trace.WriteLine(json);
                }
#endif
                return null;
            }
            catch (JsonReaderException jrEx)
            {
#if TRACE
                if (traceError)
                {
                    // Twitter might return a plain string
                    Trace.WriteLine(jrEx.Message);
                    Trace.WriteLine(jrEx.StackTrace);
                    Trace.WriteLine("JSON:");
                    Trace.WriteLine(json);
                }
#endif
                return null;
            }
            catch (Exception ex)
            {
#if TRACE
                if (traceError)
                {
                    // Collections have issues converting to singles
                    Trace.WriteLine(ex.Message);
                    Trace.WriteLine(ex.StackTrace);
                    Trace.WriteLine("JSON:");
                    Trace.WriteLine(json);
                }
#endif
                return null;
            }
        }

        private static readonly Regex _isXml =
            new Regex("<\\?xml version=\"1\\.0\" encoding=\"UTF-8\"\\?>"
#if !SILVERLIGHT
                      , RegexOptions.Compiled);
#else
                      );
#endif

        internal static bool IsXml(this string json, out string xml)
        {
            // Since we only want a string, I don't want to incur exceptions thrown
            // each and every time in order to dictate branching; I also don't
            // want to validate each and every line of XML; the quickest way
            // for now is to just run a regex looking for the head tag
            const string head = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            var isXml = _isXml.IsMatch(json);
            xml = isXml ? json.Replace(head, String.Empty) : json;
            return isXml;
        }
    }
}
