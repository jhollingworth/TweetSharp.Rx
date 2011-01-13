using System;
using System.IO;
using System.Text;
using Hammock.Serialization;
using Newtonsoft.Json;

namespace TweetSharp.Model
{
    // [DC]: This class must be public or it will not load correctly with MEF

    /// <summary>
    /// Handles internal deserialization of incoming models.
    /// This class is only public as a workaround for issues with MEF when loading
    /// assemblies with public key encryption and strong naming.
    /// </summary>
    /// <typeparam name="T">The type of model serializer used</typeparam>
    public class TweetSharpSerializer<T> : ISerializer, IDeserializer 
        where T : IModelSerializer, new()
    {
        private readonly JsonSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweetSharpSerializer&lt;T&gt;"/> class.
        /// </summary>
        public TweetSharpSerializer()
        {
            var serializer = new T();
            var settings = serializer.Settings;

            _serializer = new JsonSerializer
            {
                ConstructorHandling = settings.ConstructorHandling,
                ContractResolver = settings.ContractResolver,
                ObjectCreationHandling = settings.ObjectCreationHandling,
                MissingMemberHandling = settings.MissingMemberHandling,
                DefaultValueHandling = settings.DefaultValueHandling,
                NullValueHandling = settings.NullValueHandling
            };

            foreach (var converter in settings.Converters)
            {
                _serializer.Converters.Add(converter);
            }
        }

        #region IDeserializer Members

        /// <summary>
        /// Deserializes the specified content.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public virtual object Deserialize(string content, Type type)
        {
            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return _serializer.Deserialize(jsonTextReader, type);
                }
            }
        }

        /// <summary>
        /// Deserializes the specified content.
        /// </summary>
        /// <typeparam name="TContent">The type to deserialize.</typeparam>
        /// <param name="content">The content.</param>
        /// <returns></returns>
        public virtual TContent Deserialize<TContent>(string content)
        {
            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return _serializer.Deserialize<TContent>(jsonTextReader);
                }
            }
        }

        #endregion

        #region ISerializer Members

        /// <summary>
        /// Serializes the specified object instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public virtual string Serialize(object instance, Type type)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    jsonTextWriter.Formatting = Formatting.Indented;
                    jsonTextWriter.QuoteChar = '"';

                    _serializer.Serialize(jsonTextWriter, instance);

                    var result = stringWriter.ToString();
                    return result;
                }
            }
        }

        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <value>The type of the content.</value>
        public virtual string ContentType
        {
            get { return "application/json"; }
        }

        /// <summary>
        /// Gets the content encoding.
        /// </summary>
        /// <value>The content encoding.</value>
        public Encoding ContentEncoding
        {
            get { return Encoding.UTF8; }
        }

        #endregion
    }
}
