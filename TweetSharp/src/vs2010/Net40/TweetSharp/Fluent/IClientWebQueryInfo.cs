using Hammock.Web;

namespace TweetSharp.Fluent
{
    /// <summary>
    /// Contract describing the client used to make web queries.
    /// This contract defines meta-data about the consuming application, such 
    /// as its name, build version, homepage, and Twitter OAuth tokens.
    /// </summary>
    public interface IClientWebQueryInfo : IWebQueryInfo
    {
        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client version.
        /// </summary>
        /// <value>The client version.</value>
        string ClientVersion { get; set; }

        /// <summary>
        /// Gets or sets the client URL.
        /// </summary>
        /// <value>The client URL.</value>
        string ClientUrl { get; set; }

        /// <summary>
        /// Gets or sets the OAuth consumer key.
        /// </summary>
        /// <value>The consumer key.</value>
        string ConsumerKey { get; set; }

        /// <summary>
        /// Gets or sets the OAuth consumer secret.
        /// </summary>
        /// <value>The consumer secret.</value>
        string ConsumerSecret { get; set; }

#if SILVERLIGHT
        string TransparentProxy { get; set; }
#endif
    }

    /// <summary>
    /// Data structure describing the client used to make web queries.
    /// This class holds meta-data about the consuming application, such 
    /// as its name, build version, homepage, and Twitter OAuth tokens.
    /// </summary>
    public class ClientWebQueryInfo : IClientWebQueryInfo
    {
        /// <summary>
        /// Gets or sets the name of the client. 
        /// </summary>
        public virtual string ClientName { get; set; }
        /// <summary>
        /// Gets or sets the version of the client
        /// </summary>
        public virtual string ClientVersion { get; set; }
        /// <summary>
        /// Gets or sets the homepage url for the client
        /// </summary>
        public virtual string ClientUrl { get; set; }
        /// <summary>
        /// Gets or sets the OAuth Consumer key for the client.
        /// </summary>
        public virtual string ConsumerKey { get; set; }
        /// <summary>
        /// Gets or sets the OAuth Consumer secret for the client
        /// </summary>
        public virtual string ConsumerSecret { get; set; }

#if SILVERLIGHT
        ///<summary>
        /// Gets or sets the address of a transparent proxy server 
        /// through which to send all requests
        /// </summary>
        public virtual string TransparentProxy { get; set; }
#endif
    }
}