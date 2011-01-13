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
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using Hammock;
using Hammock.Authentication;
using Hammock.Authentication.Basic;
using Hammock.Streaming;
using TweetSharp.Twitter.Model;
using Hammock.Tasks;
using Hammock.Web;
using TweetSharp.Core;
using TweetSharp.Core.Configuration;
using TweetSharp.Core.Extensions;
using TweetSharp.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
#if !SILVERLIGHT
using System.Text.RegularExpressions;
using System.Web;
using System.Security;

#endif

#if SILVERLIGHT
using Hammock.Silverlight.Compat;
#endif

#if SILVERLIGHT && !WindowsPhone
using HttpUtility = System.Windows.Browser.HttpUtility;
#endif

#if WindowsPhone
using System.Windows.Threading;
#endif

namespace TweetSharp.Twitter.Fluent
{
#if !SILVERLIGHT
    /// <summary>
    /// This is the main fluent class for building expressions
    /// bound for the Twitter API.
    /// </summary>
    [Serializable]
#endif
    public class FluentTwitter : 
        FluentBase<TwitterResult>, 
        IFluentTwitter
    {
        /// <summary>
        /// Gets the URL for the OAuth authority.
        /// </summary>
        /// <value>The URL O auth authority.</value>
        protected override string UrlOAuthAuthority
        {
            get { return "http://api.twitter.com/oauth/{0}"; }
        }

        // Authority
        private const string UrlAuthority = "http://api.twitter.com/1/";
        private const string UrlSearchAuthority = "http://search.twitter.com/";
        private const string UrlStreamingAuthority = "http://stream.twitter.com/1/";
        
        // Base
        private const string UrlActionBase = UrlAuthority + "{0}/{1}.{2}";
        private const string UrlActionIdBase = UrlAuthority + "{0}/{1}/{2}.{3}";
        private const string UrlBase = UrlAuthority + "{0}.{1}";
        private const string UrlSearchBase = UrlSearchAuthority + "{0}.{1}";
        private const string UrlStreamingBase = UrlStreamingAuthority + "statuses/{0}.{1}";
        
        // List Base
        private const string UrlListsBase = UrlAuthority + "{0}/lists.{1}";
        private const string UrlListsIdBase = UrlAuthority + "{0}/lists/{1}.{2}";
        private const string UrlListsActionBase = UrlAuthority + "{0}/lists/{1}/{2}.{3}";

        // ClientInfo Defaults
        private const string TwitterClientDefaultName = "tweetsharp";
        private const string TwitterClientDefaultUrl = "http://tweetsharp.com";
        private const string TwitterClientDefaultVersion = "1.0.0.0";
        private const int TwitterMaxUpdateLength = 140;

#if SILVERLIGHT
        // Proxy Request Headers
        private const string ProxyAcceptEncodingHeader = "X-Twitter-Accept";
        private const string ProxyAgentHeader = "X-Twitter-Agent";
        private const string ProxyAuthorizationHeader = "X-Twitter-Auth";
        private const string ProxyMethodHeader = "X-Twitter-Method";
        private const string ProxyQueryHeader = "X-Twitter-Query";

        // Proxy Response Headers
        private const string ProxyStatusCode = "X-Twitter-StatusCode";
        private const string ProxyStatusDescription = "X-Twitter-StatusDescription";
#endif

        static FluentTwitter()
        {
            Bootstrapper.Run();
        }

        private FluentTwitter(IClientInfo clientInfo)
        {
            var serializer = new TweetSharpSerializer<TwitterModelSerializer>();
            Client.Serializer = serializer;
            Client.Deserializer = serializer;
            ClientInfo = clientInfo;

            InitializeFluentParameters();

            // Set JSON as the default
            Format = WebFormat.Json;
        }

        private void InitializeFluentParameters()
        {
            Profile = new FluentTwitterProfile();
            Authentication = new FluentTwitterAuthentication(this);
            ExternalAuthentication = new Dictionary<AuthenticationMode, IExternalAuthenticationDetails>();
            Configuration = new FluentTwitterConfiguration(this);
            SearchParameters = new FluentTwitterSearchParameters();
            StreamingParameters = new FluentTwitterStreamingParameters();
            TrendsParameters = new FluentTwitterTrendsParameters();
            Parameters = new FluentTwitterParameters();
        }

        /// <summary>
        /// Gets the authentication pair used to authenticate to twitter.
        /// </summary>
        /// <value>The authentication pair, typically a username and password or a oauth token and tokensecret.</value>
        public override Pair<string, string> AuthenticationPair
        {
            get
            {
                if (Authentication == null || Authentication.Authenticator == null)
                {
                    return null;
                }

                return GetAuthPairFromAuthenticator(Authentication);
            }
        }

        /// <summary>
        /// Gets the authentication pair used to authenticate to 3rd party services such as image hosts
        /// </summary>
        /// <value>The authentication pair, typically a username and password or a oauth token and tokensecret.</value>
        public virtual Pair<string, string> SecondaryAuthenticationPair
        {
            get
            {
                if (ExternalAuthentication.ContainsKey(AuthenticationMode.Basic))
                {
                    var auth = (FluentBaseBasicAuth)ExternalAuthentication[AuthenticationMode.Basic];
                    return new Pair<string, string> { First = auth.Username, Second = auth.Password };
                }
                return null;
            }
        }

        private static Pair<string, string> GetAuthPairFromAuthenticator(IFluentAuthentication authentication)
        {
            if (authentication == null)
            {
                return null;
            }

            switch (authentication.Mode)
            {
                case AuthenticationMode.Basic:
                    return new Pair<string, string>
                               {
                                   First = ((FluentBaseBasicAuth) authentication.Authenticator).Username,
                                   Second = ((FluentBaseBasicAuth) authentication.Authenticator).Password
                               };
                case AuthenticationMode.OAuth:
                    return new Pair<string, string>
                               {
                                   First = ((FluentBaseOAuth) authentication.Authenticator).Token,
                                   Second = ((FluentBaseOAuth) authentication.Authenticator).TokenSecret
                               };
                default:
                    throw new NotSupportedException("Unknown authentication mode");
            }
        }
        
        /// <summary>
        /// Gets or sets the external authentication info used for third-party requests.
        /// </summary>
        /// <value>The external authentication.</value>
        public virtual Dictionary<AuthenticationMode, IExternalAuthenticationDetails> ExternalAuthentication { get; set; }

        /// <summary>
        /// Gets or sets the user profile query branch.
        /// </summary>
        /// <value>The profile.</value>
        public virtual IFluentTwitterProfile Profile { get; set; }
        
        /// <summary>
        /// Gets the search parameters branch.
        /// </summary>
        /// <value>The search parameters.</value>
        public virtual IFluentTwitterSearchParameters SearchParameters { get; private set; }

        /// <summary>
        /// Gets the query parameters branch.
        /// </summary>
        /// <value>The parameters.</value>
        public virtual IFluentTwitterParameters Parameters { get; private set; }

        /// <summary>
        /// Gets the streaming parameters branch.
        /// </summary>
        /// <value>The streaming parameters.</value>
        public virtual IFluentTwitterStreamingParameters StreamingParameters { get; private set; }

        /// <summary>
        /// Gets the trends parameters branch.
        /// </summary>
        /// <value>The trends parameters.</value>
        public virtual IFluentTwitterTrendsParameters TrendsParameters { get; private set; }

        /// <summary>
        /// Gets or sets the current rate limiting rule imposed by this query.
        /// </summary>
        /// <value>The rate limiting rule.</value>
        IRateLimitingRule<IRateLimitStatus> IFluentTwitter.RateLimitingRule
        {
            get
            {
                return RateLimitingRule;
            }
            set
            {
                RateLimitingRule = value; 
            }
        }

        /// <summary>
        /// Gets the internal callback.
        /// </summary>
        /// <value>The internal callback.</value>
        protected override Action<object, TwitterResult> InternalCallback
        {
            get { return InternalCallbackImpl; }
        }

        private void InternalCallbackImpl(object sender, TweetSharpResult result)
        {
            if (Callback != null)
            {
                Callback(sender, result as TwitterResult, null);
            }
        }

        /// <summary>
        /// Gets or sets the client info.
        /// </summary>
        /// <value>The client info.</value>
        TwitterClientInfo IFluentTwitter.ClientInfo
        {
            get { return ClientInfo as TwitterClientInfo; }
            set { ClientInfo = value; }
        }

        IFluentTwitterAuthentication IFluentTwitter.Authentication
        {
            get { return (IFluentTwitterAuthentication)Authentication; }
            set { Authentication = value; }
        }

        IFluentTwitterConfiguration IFluentTwitter.Configuration
        {
            get { return (FluentTwitterConfiguration)Configuration; }
        }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>The callback.</value>
        public TwitterWebCallback Callback { get; set; }

#if !SILVERLIGHT
        /// <summary>
        /// Makes a sequential call to Twitter to get the results of this query.
        /// </summary>
        /// <returns></returns>
        public override TwitterResult Request()
        {
            var request = InitializeRequest(false /* async */);
           
            var response = Client.Request(request);
            
            var result = BuildResult(response);

            if (Parameters.CopyTo != ExternalService.None)
            {
                PostToExternalServices(false /* async */);
            }

            return result;
        }
#endif

#if !WindowsPhone
        /// <summary>
        /// Starts the request asynchronously.
        /// </summary>
        /// <returns>The <see cref="IAsyncResult"/> handle for the request</returns>
        public override IAsyncResult BeginRequest()
        {
            return BeginRequest(null);
        }

        /// <summary>
        /// Starts the request asynchronously.
        /// </summary>
        /// <returns>The <see cref="IAsyncResult"/> handle for the request</returns>
        public override IAsyncResult BeginRequest(object userState)
        {
            var request = InitializeRequest(true /* async */);

            RestCallback callback = null;
            if (Callback != null)
            {
                callback = new RestCallback(
                    (req, resp, state) =>
                        {
                            var twitterResult = BuildResult(resp);

                            if (Callback != null)
                            {
                                Callback(this, twitterResult, state);
                            }
                        }
                    );
            }

#if SILVERLIGHT
            Client.HasElevatedPermissions = ClientInfo.HasElevatedPermissions;
            Client.SilverlightAcceptEncodingHeader = ClientInfo.SilverlightAcceptEncodingHeader;
            Client.SilverlightAuthorizationHeader = ClientInfo.SilverlightAuthorizationHeader;
            Client.SilverlightMethodHeader = ClientInfo.SilverlightMethodHeader;
            Client.SilverlightUserAgentHeader = ClientInfo.SilverlightAgentHeader;
#endif

            var response = Client.Request(request);

            var result = callback == null
                             ? Client.BeginRequest(request, userState)
                             : Client.BeginRequest(request, callback, userState);

            return result;
        }

        /// <summary>
        /// Completes the asynchronous request.
        /// </summary>
        /// <param name="asyncResult">The <see cref="IAsyncResult"/>handle returned from <see cref="FluentBase{TResult}.BeginRequest()"/></param>
        /// <returns></returns>
        public override TwitterResult EndRequest(IAsyncResult asyncResult)
        {
            var response = Client.EndRequest(asyncResult);
            var result = BuildResult(response);
            return result;
        }
#else

        public override void BeginRequest()
        {
            BeginRequest(null);
        }

        public override void BeginRequest(object userState)
        {
            var request = InitializeRequest(true /* async */);

            RestCallback callback = null;
            if (Callback != null)
            {
                callback = new RestCallback(
                    (req, resp, state) =>
                    {
                        var twitterResult = BuildResult(resp);

                        if (Callback != null)
                        {
                            Callback(this, twitterResult, state);
                        }
                    }
                    );
            }

            if(callback == null)
            {
                Client.BeginRequest(request, userState);
            }
            else
            {
                Client.BeginRequest(request, callback, userState);
            }
        }
#endif

        private RestRequest CreateRestRequest()
        {
            EnsureDefaultCache();

            var credentials = CreateRestCredentials();

            var request = new RestRequest
                              {
                                  Credentials = credentials,
                                  Method = Method,
                                  Path = AsUrl(),
                                  Cache = Configuration.CacheStrategy,
                                  CacheKeyFunction = () => CacheKey,
                                  Timeout = Configuration.RequestTimeout,
                                  DecompressionMethods = Configuration.CompressHttpRequests
                                                             ? DecompressionMethods.GZip |
                                                               DecompressionMethods.Deflate
                                                             : DecompressionMethods.None,
                              };
           
            SetRequestMeta(request);

            // [DC]: Support legacy client identities
            if (ClientInfo != null && !ClientInfo.ClientName.IsNullOrBlank() && Method == WebMethod.Post)
            {
                request.AddParameter("source", ClientInfo.ClientName);
            }

            return request;
        }

        private IWebCredentials CreateRestCredentials()
        {
            IWebCredentials credentials;
            switch (Authentication.Mode)
            {
                case AuthenticationMode.OAuth:
                    {
                        credentials = GetCredentialsFromOAuthAuthenticator();
                        break;
                    }
                case AuthenticationMode.Basic:
                    {
                        if (HasAuth)
                        {
                            var authToken = AuthenticationPair.First;
                            var authSecret = AuthenticationPair.Second;
                            credentials = new BasicAuthCredentials
                                              {
                                                  Username = authToken,
                                                  Password = authSecret
                                              };
                        }
                        else
                        {
                            credentials = new BasicAuthCredentials();
                        }
                    }
                    break;
                case AuthenticationMode.None:
                    credentials = new BasicAuthCredentials();
                    break;
                default:
                    throw new NotSupportedException("Only Basic Auth and OAuth authentication schemes are supported");
            }
            return credentials;
        }

        private static TwitterRateLimitStatus GetRateLimitStatus(WebResponse response)
        {
            if ( response == null )
            {
                return null; 
            }
            // [DC]: When using a SL proxy, the internal implementation
            // [DC]: is BrowserHttpWebResponse which does not implement
            // [DC]: the Headers collection; ergo, not supported in SL2
            // [DC]: (This will work with SL3 + and ClientHttp)
#if SL2
            return null;
#else
            var headers = response.Headers;

            // X-RateLimit-Limit, X-RateLimit-Remaining, X-RateLimit-Reset
            var limit = headers["X-RateLimit-Limit"];
            var remaining = headers["X-RateLimit-Remaining"];
            var reset = headers["X-RateLimit-Reset"];

            return !(new[] { limit, remaining, reset }).AreNullOrBlank()
                       ? new TwitterRateLimitStatus
                             {
                                 HourlyLimit = Convert.ToInt32(limit, CultureInfo.InvariantCulture),
                                 RemainingHits = Convert.ToInt32(remaining, CultureInfo.InvariantCulture),
                                 ResetTimeInSeconds = Convert.ToInt64(reset, CultureInfo.InvariantCulture),
                                 ResetTime = Convert.ToInt64(reset, CultureInfo.InvariantCulture).FromUnixTime()
                             }
                       : null;
#endif
        }

        private RestRequest InitializeRequest(bool async)
        {
            ValidateUpdateText();

            var isStreaming = Parameters.Activity != null && Parameters.Activity.Equals("stream");

            if (!async && isStreaming)
            {
                throw new TweetSharpException("You must use RequestAsync() when using the Streaming API.");
            }

            if (async && isStreaming && Callback == null)
            {
                throw new TweetSharpException("You must declare a callback when using the Streaming API.");
            }

            return CreateRestRequest();

            //if(async && isStreaming)
            //{
            //    var duration = StreamingParameters.Duration.HasValue
            //                       ? StreamingParameters.Duration.Value
            //                       : TimeSpan.Zero;

            //    var resultsPerCallback = StreamingParameters.ResultsPerCallback.HasValue
            //                          ? StreamingParameters.ResultsPerCallback.Value
            //                          : 10;

            //    var streamOptions = new StreamOptions
            //                            {
            //                                Duration = duration,
            //                                ResultsPerCallback = resultsPerCallback
            //                            };
            //    request.StreamOptions = streamOptions;
            //}

            //return request;
        }

        /// <summary>
        /// Creates a new composable query, using a specified client and a default platform.
        /// </summary>
        /// <param name="clientInfo">The client making the request</param>
        public static IFluentTwitter CreateRequest(TwitterClientInfo clientInfo)
        {
            return new FluentTwitter(clientInfo);
        }

        /// <summary>
        /// Creates a new composable query, using the default client and platform.
        /// </summary>
        public static IFluentTwitter CreateRequest()
        {
            if (_staticClientInfo == null)
            {
                lock (_clientInfoLock)
                {
                    _staticClientInfo = new TwitterClientInfo
                    {
                        ClientName = TwitterClientDefaultName,
                        ClientUrl = TwitterClientDefaultUrl,
                        ClientVersion = TwitterClientDefaultVersion
    #if SILVERLIGHT
                        , SilverlightAcceptEncodingHeader = ProxyAcceptEncodingHeader
                        , SilverlightAgentHeader = ProxyAgentHeader
                        , SilverlightAuthorizationHeader = ProxyAuthorizationHeader
                        , SilverlightMethodHeader = ProxyMethodHeader
    #endif
                    };
                }
            }
            return new FluentTwitter(_staticClientInfo);
        }

        /// <summary>
        /// Validates the update text.
        /// </summary>
        //[jd]Changed access to internal from protected as this is accessed from unit tests
        internal override void ValidateUpdateText()
        {
            if (Parameters.Text == null)
            {
                // non-participant
                return;
            }

            if (Parameters.Text.IsNullOrBlank())
            {
                throw new ArgumentException("Status text must contain at least one character");
            }

            var words = Parameters.Text.Split(' ').ToList();

            if (Parameters.Text.Length <= TwitterMaxUpdateLength)
            {
                // valid
                return;
            }

            switch (((IFluentTwitterConfiguration)Configuration).TruncateUpdates)
            {
                case true:
                    while (Parameters.Text.Length > TwitterMaxUpdateLength)
                    {
                        if (words.Count > 1)
                        {
                            var last = words.Last();
                            Parameters.Text = Parameters.Text.RemoveRange(Parameters.Text.LastIndexOf(last),
                                                                          Parameters.Text.Length);
                            words.Remove(last);
                        }
                        else
                        {
                            if (Parameters.Text.Length == 1)
                            {
                                throw new TweetSharpException("This shouldn't have happened");
                            }

                            Parameters.Text = Parameters.Text.Substring(0, Parameters.Text.Length - 1);
                        }
                    }

                    ValidateUpdateText();
                    break;
                default:
                    throw new TweetSharpException(
                        "Status length of {0} exceeds the maximum length of {1}".FormatWith(Parameters.Text.Length,
                                                                                            TwitterMaxUpdateLength));
            }
        }

        /// <summary>
        /// Builds a <see cref="TwitterResult" /> from a REST response from Hammock.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns></returns>
        protected override TwitterResult BuildResult(RestResponseBase response)
        {
            if(response == null)
            {
                throw new ArgumentNullException("response", "RestResponse was null");
            }

            var result = new TwitterResult
                             {
                                 SkippedDueToRateLimiting = response.SkippedDueToRateLimitingRule,
                                 RequestDate = response.RequestDate,
                                 ResponseDate = response.ResponseDate,
                                 Response = response.Content,
                                 ResponseType = response.ContentType,
                                 ResponseLength = response.ContentLength,
                                 RequestUri = response.RequestUri,
                                 ResponseUri = response.ResponseUri,
                                 ResponseHttpStatusCode = (int) response.StatusCode,
                                 ResponseHttpStatusDescription = response.StatusDescription,
                                 RateLimitStatus = GetRateLimitStatus(response.InnerResponse),
                                 RequestHttpMethod = response.RequestMethod,
                                 Exception = response.InnerException,
                                 Retries = response.TimesTried,
                                 WebResponse = response.InnerResponse, 
#if !SILVERLIGHT
                                 TimedOut = response.TimedOut,
#endif
                             };

#if !SILVERLIGHT
            if (!result.Response.IsNullOrBlank() && response.RequestKeptAlive)
            {
                // StringSplitOptions.RemoveEmptyEntries not supported on CE
                var lines = result.Response
                    .Split(new[] { '\r' }).Where(v => !v.IsNullOrBlank())
                    .ToArray();

                if (lines.Length > 1)
                {
                    result.StreamedResponses = lines;
                }
            }
#endif
            return result;
        }

        /// <summary>
        /// Returns the human-readable query to Twitter representing the current expression.
        /// If you are storing URLs for sending later, you can use <code>AsUrl()</code> to return
        /// a URL-encoded string instead.
        /// </summary>
        /// <returns>A URL-decoded string representing this expression's query to Twitter</returns>
        public override string ToString()
        {
            // human-readable; for storing urls, use AsUrl()
            return AsUrl().UrlDecode();
        }

        /// <summary>
        /// Gets the URL that was/will be used to perform the current query
        /// </summary>
        /// <param name="ignoreTransparentProxy">if true, the underlying service API URL is returned, otherwise the transparent proxy is returned.</param>
        /// <returns>The URL for the query</returns>
        public override string AsUrl(bool ignoreTransparentProxy)
        {
            var pre = Configuration.TransparentProxy;
            if (ignoreTransparentProxy)
            {
                Configuration.TransparentProxy = null;
            }

            var hasAuthAction = IsOAuthProcessCall;
            var hasAction = !Parameters.Action.IsNullOrBlank();
            var format = Format.ToLower();
            var activity = Parameters.Activity.IsNullOrBlank() ? "?" : Parameters.Activity;
            var action = hasAction ? Parameters.Action : "?";

            string url;

            // this is a direct call
            if (!Parameters.DirectPath.IsNullOrBlank())
            {
                url = BuildDirectQuery();
            }
            else
                // this is an oauth call
                if (hasAuthAction)
                {
                    url = BuildOAuthQuery();
                }
                else // this is a streaming api call
                    if (hasAction && activity.Equals("stream"))
                    {
                        url = BuildStreamingQuery(action, format);
                    }
                    else // this is a trends query
                        if (hasAction && activity.Equals("trends"))
                        {
                            url = BuildTrendsQuery(activity, action, format);
                        }
                        else // this is a search api call
                            if (hasAction && !activity.Equals("users") &&
                                (Equals(action, "search")
                                 || Equals(action, "trends")
                                 || Equals(action, "trends/current")
                                 || Equals(action, "trends/daily")
                                 || Equals(action, "trends/weekly")))
                            {
                                url = BuildSearchQuery(format);
                            }
                            else // this is a lists query
                                if (Equals(Parameters.Activity, "lists"))
                                {
                                    url = BuildListsQuery(format, action);
                                }
                                else // this is a rest api call
                                {
                                    url = BuildQuery(hasAction, format, activity, action);
                                }

            // [DC] Twitter has recently had issues with passing screen names in URLs with uppercase;
            // So we should lowercase all non-query elements of the URL automatically
            // todo determine if Uri does this on its own; if so, just wrap and fire
            if (Configuration.TransparentProxy.IsNullOrBlank() 
                && ((IFluentTwitterConfiguration)Configuration).UseHttps)
            {
                url = url.Replace("http://", "https://");
            }
            var uri = new Uri(url);
            
            url = string.Format("{0}://{1}{2}{3}{4}",
                                uri.Scheme,
                                uri.Host.ToLower(),
                                (uri.Scheme == "http" && uri.Port != 80 ||
                                 uri.Scheme == "https" && uri.Port != 443)
                                    ? ":" + uri.Port
                                    : "",
                                uri.AbsolutePath.ToLower(),
                                uri.Query); // Don't lowercase the query; otherwise this would be one ToLower() call

            Configuration.TransparentProxy = pre;
            return url;
        }

        /// <summary>
        /// Builds a URL from the specified fluent expression instance.
        /// </summary>
        /// <returns></returns>
        public override string AsUrl()
        {
            return AsUrl(false /* ignoreTransparentProxy */);
        }

        /// <summary>
        /// Builds the query.
        /// </summary>
        /// <param name="hasAction">if set to <c>true</c> the query uses a URL-based action.</param>
        /// <param name="format">The format.</param>
        /// <param name="activity">The activity.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        protected override string BuildQuery(bool hasAction, string format, string activity, string action)
        {
            var id = !Parameters.ScreenName.IsNullOrBlank()
                         ? Parameters.ScreenName
                         : !Parameters.Email.IsNullOrBlank()
                               ? Parameters.Email
                               : Parameters.Id.HasValue
                                     ? Parameters.Id.Value.ToString()
                                     : String.Empty;

            var isDisambiguated =
                Parameters.Activity.Equals("users") &&
                (!Parameters.Action.IsNullOrBlank() &&
                 Parameters.Action.Equals("show"));

            var hasId = !id.IsNullOrBlank() && !isDisambiguated;

            // Swap the authority if a transparent proxy is used
            var urlActionIdBase = UrlActionIdBase;
            var urlActionBase = UrlActionBase;
            var urlBase = UrlBase;

            var transparentProxy = Configuration.TransparentProxy ?? ClientInfo.TransparentProxy;
            if (!transparentProxy.IsNullOrBlank())
            {
                var authority = transparentProxy;
                urlActionIdBase = urlActionIdBase.Replace(UrlAuthority, authority);
                urlActionBase = urlActionBase.Replace(UrlAuthority, authority);
                urlBase = urlBase.Replace(UrlAuthority, authority);
            }

            var url = hasAction ? hasId ? urlActionIdBase : urlActionBase : urlBase;
            url = String.Format(url, hasAction
                                         ? hasId
                                               ? new object[] { activity, action, id, format }
                                               : new object[] { activity, action, format }
                                         : new object[] { activity, format });

            var resultUrl = BuildRestParameters(url);
            return resultUrl;
        }

        private string BuildRestParameters(string url)
        {
            var parameters = new List<string>(BuildRestParameters());

            return ConcatentateParameters(parameters, url);
        }

        private IEnumerable<string> BuildRestParameters()
        {
            foreach (var pagingParameter in BuildPagingParameters())
            {
                yield return pagingParameter;
            }

            if (Parameters.Cursor.HasValue)
            {
                yield return "cursor={0}".FormatWith(Parameters.Cursor.Value);
            }

            if (Parameters.ReturnPerPage.HasValue)
            {
                yield return string.Format("rpp={0}", Parameters.ReturnPerPage.Value);
            }

            if (!Parameters.Text.IsNullOrBlank())
            {
                var format = Parameters.Activity == "direct_messages"
                                 ? "text={0}"
                                 : "status={0}";

                var content = Parameters.Text.UrlEncodeStrict();
                yield return string.Format(format, content);
            }

            if (Parameters.InReplyToStatusId.HasValue)
            {
                yield return string.Format("in_reply_to_status_id={0}", Parameters.InReplyToStatusId.Value);
            }

            if (Parameters.UserId.HasValue)
            {
                string format;
                switch (Parameters.Activity)
                {
                    case "friendships":
                        format = "user_a={0}";
                        break;
                    case "users":
                    case "report_spam":
                        format = "user_id={0}";
                        break;
                    default:
                        format = "user={0}";
                        break;
                }

                yield return string.Format(format, Parameters.UserId.Value);
            }

            if (!Parameters.UserScreenName.IsNullOrBlank())
            {
                string format;
                switch (Parameters.Activity)
                {
                    case "friendships":
                        format = "user_a={0}";
                        break;
                    case "users":
                    case "report_spam":
                        format = "screen_name={0}";
                        break;
                    default:
                        format = "user={0}";
                        break;
                }

                yield return string.Format(format, Parameters.UserScreenName.UrlEncodeStrict());
            }

            if (Parameters.Follow.HasValue)
            {
                yield return string.Format("follow={0}", Parameters.Follow.ToString().ToLower());
            }

            if (Parameters.VerifyId.HasValue)
            {
                yield return string.Format("user_b={0}", Parameters.VerifyId.ToString().ToLower());
            }

            if (!Parameters.VerifyScreenName.IsNullOrBlank())
            {
                var format = Parameters.Activity == "friendships"
                                 ? "user_b={0}"
                                 : "user={0}";

                yield return string.Format(format, Parameters.VerifyScreenName.UrlEncodeStrict());
            }

            if (!Profile.ProfileName.IsNullOrBlank())
            {
                yield return string.Format("name={0}", Profile.ProfileName.UrlEncodeStrict());
            }

            if (!Profile.ProfileLocation.IsNullOrBlank())
            {
                yield return string.Format("location={0}", Profile.ProfileLocation.UrlEncodeStrict());
            }

            if (!Profile.ProfileUrl.IsNullOrBlank())
            {
                yield return string.Format("url={0}", Profile.ProfileUrl.UrlEncodeStrict());
            }

            if (!Profile.ProfileDescription.IsNullOrBlank())
            {
                yield return string.Format("description={0}", Profile.ProfileDescription.UrlEncodeStrict());
            }

            if (Profile.ProfileDeliveryDevice.HasValue)
            {
                yield return string.Format("device={0}", Profile.ProfileDeliveryDevice.ToString().ToLower());
            }

            if (!Profile.ProfileBackgroundColor.IsNullOrBlank())
            {
                yield return string.Format("profile_background_color={0}", Profile.ProfileBackgroundColor.UrlEncodeStrict());
            }

            if (!Profile.ProfileTextColor.IsNullOrBlank())
            {
                yield return string.Format("profile_text_color={0}", Profile.ProfileTextColor.UrlEncodeStrict());
            }

            if (!Profile.ProfileLinkColor.IsNullOrBlank())
            {
                yield return string.Format("profile_link_color={0}", Profile.ProfileLinkColor.UrlEncodeStrict());
            }

            if (!Profile.ProfileSidebarFillColor.IsNullOrBlank())
            {
                yield return
                    string.Format("profile_sidebar_fill_color={0}", Profile.ProfileSidebarFillColor.UrlEncodeStrict());
            }

            if (!Profile.ProfileSidebarBorderColor.IsNullOrBlank())
            {
                yield return string.Format("profile_sidebar_border_color={0}", Profile.ProfileLinkColor.UrlEncodeStrict());
            }

            if (Parameters.GeoLocation != null)
            {
                yield return "lat={0}&long={1}".FormatWithInvariantCulture(
                                                                              Parameters.GeoLocation.Coordinates.Latitude,
                                                                              Parameters.GeoLocation.Coordinates.Longitude);
            }

            if (Parameters.SourceId.HasValue)
            {
                yield return string.Format("source_id={0}", Parameters.SourceId.Value);
            }

            if (Parameters.TargetId.HasValue)
            {
                yield return string.Format("target_id={0}", Parameters.TargetId.Value);
            }

            if (!Parameters.SourceScreenName.IsNullOrBlank())
            {
                yield return string.Format("source_screen_name={0}", Parameters.SourceScreenName);
            }

            if (!Parameters.TargetScreenName.IsNullOrBlank())
            {
                yield return string.Format("target_screen_name={0}", Parameters.TargetScreenName);
            }

            if (!Parameters.UserSearch.IsNullOrBlank())
            {
                yield return string.Format("q={0}", Parameters.UserSearch.UrlEncodeStrict());
            }

            if (Parameters.LookupScreenNames != null && Parameters.LookupScreenNames.Count() > 0)
            {
                yield return "screen_name={0}".FormatWith(Parameters.LookupScreenNames.ConcatenateWith(",", true));
            }

            if (Parameters.LookupUserIds != null && Parameters.LookupUserIds.Count() > 0)
            {
                yield return "user_id={0}".FormatWith(Parameters.LookupUserIds.ConcatenateWith(",", true));
            }

            // [DC]: Refactor to use ConcatenatWith as in above
            // Saved searches borrows the search builder
            if ( (Parameters.Activity != null && Parameters.Activity.Equals("saved_searches")) && (Parameters.Action != null && Parameters.Action.Equals("create")))
            {
                const string format = "query={0}";
                var sb = new StringBuilder();

                var searchOperators = BuildSearchOperators();
                var searchParameters = BuildSearchParameters();

                var total = searchOperators.Count();
                var count = 0;
                foreach (var searchOperator in searchOperators)
                {
                    sb.Append(searchOperator);
                    count++;

                    if (total < count)
                    {
                        sb.Append(" ");
                    }
                }

                total = searchParameters.Count();
                count = 0;
                foreach (var searchParameter in searchParameters)
                {
                    sb.Append(searchParameter);
                    count++;

                    if (total < count)
                    {
                        sb.Append(" ");
                    }
                }

                yield return format.FormatWith(sb);
            }
        }

        private string BuildDirectQuery()
        {
            var path = Parameters.DirectPath;
            if (path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            // Swap the authority if a transparent proxy is used
            var urlBase = UrlAuthority;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlBase = urlBase.Replace(UrlAuthority, authority);
            }

            var url = String.Concat(urlBase, path);
            var uri = url.AsUri();

            var query = uri.Query;
            if (query.IsNullOrBlank())
            {
                path = url;
            }
            else
            {
                // remove POST parameters 
                // NOTE: Duplicate from Core.Web.Query.OAuth.OAuthWebQuery.BuildPostOrPutWebRequest()
                path = uri.Scheme.Then("://")
#if !SILVERLIGHT
.Then(uri.Authority)
#else
                    .Then(uri.Host)
#endif
;
                if (uri.Port != 80)
                    path = path.Then(":" + uri.Port);
                path = path.Then(uri.AbsolutePath);

#if !SILVERLIGHT
                var parameters = HttpUtility.ParseQueryString(query);
#else
                var parameters = query.ParseQueryString();
#endif
                
#if !SILVERLIGHT
                var keys = parameters.AllKeys;
#else
                var keys = parameters.Keys; // Uses Compat
#endif
                var encodedQuery = "?" + string.Join("&", keys.Select(key => "{0}={1}".FormatWith(
                    HttpUtility.UrlEncode(key),
                    HttpUtility.UrlEncode(parameters[key]))).ToArray());

                path = path.Then(encodedQuery);
            }

            return path;
        }

        private string BuildStreamingQuery(string action, string format)
        {
            if(action.Equals("user"))
            {
                return "https://userstream.twitter.com/2/user.json";
            }

            var streamingBase = UrlStreamingBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                streamingBase = streamingBase.Replace(UrlStreamingAuthority, authority);
            }
            

            var url = String.Format(streamingBase, action, format);

            return BuildStreamingParameters(url);
        }

        private string BuildStreamingParameters(string url)
        {
            var parameters = new List<string>(0);
            // todo do filter methods need to be in the POST body?
            if (StreamingParameters.Count.HasValue && StreamingParameters.Count.Value != 0)
            {
                parameters.Add("count={0}".FormatWith(StreamingParameters.Count.Value));
            }

            if (StreamingParameters.Length.HasValue)
            {
                parameters.Add("delimited={0}".FormatWith(StreamingParameters.Length.Value));
            }

            if (StreamingParameters.UserIds != null && StreamingParameters.UserIds.Count() > 0)
            {
                var sb = new StringBuilder();
                var array = StreamingParameters.UserIds.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    sb.Append(array[i]);
                    if (i < array.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                parameters.Add("follow={0}".FormatWith(sb.ToString().UrlEncodeStrict()));
            }

            if (StreamingParameters.Locations != null && StreamingParameters.Locations.Count() > 0)
            {
                var sb = new StringBuilder();
                var array = StreamingParameters.Locations.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    var box = array[i];
                    var pair = "{0},{1}".FormatWith(box.Coordinates.Longitude, box.Coordinates.Latitude);
                    sb.Append(pair);
                    if (i < array.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                parameters.Add("locations={0}".FormatWith(sb.ToString().UrlEncodeStrict()));
            }

            if (StreamingParameters.Keywords != null && StreamingParameters.Keywords.Count() > 0)
            {
                var sb = new StringBuilder();
                var array = StreamingParameters.Keywords.ToArray();

                for (var i = 0; i < array.Length; i++)
                {
                    sb.Append(array[i]);
                    if (i < array.Length - 1)
                    {
                        sb.Append(",");
                    }
                }

                parameters.Add("track={0}".FormatWith(sb.ToString().UrlEncodeStrict()));
            }

            return ConcatentateParameters(parameters, url);
        }

        private static string ConcatentateParameters(IList<string> parameters, string url)
        {
            var sb = new StringBuilder(url);
            for (var i = 0; i < parameters.Count(); i++)
            {
                sb.Append(i > 0 ? "&" : "?");
                sb.Append(parameters[i]);
            }

            return sb.ToString();
        }

        private string BuildTrendsQuery(string activity, string action, string format)
        {
            var trendsBase = UrlActionBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                trendsBase = trendsBase.Replace(UrlStreamingAuthority, authority);
            }

            var hasId = TrendsParameters.WoeId.HasValue;

            var url = hasId
                          ? String.Format(trendsBase, activity, TrendsParameters.WoeId, format)
                          : String.Format(trendsBase, activity, action, format);

            return BuildTrendsParameters(url);
        }

        private string BuildTrendsParameters(string url)
        {
            var parameters = new List<string>(0);

            if (TrendsParameters.OrderLocation != null)
            {
                parameters.Add("lat={0}".FormatWith(TrendsParameters.OrderLocation.Coordinates.Latitude.ToString().UrlEncodeStrict()));
                parameters.Add("long={0}".FormatWith(TrendsParameters.OrderLocation.Coordinates.Longitude.ToString().UrlEncodeStrict()));
            }

            return ConcatentateParameters(parameters, url);
        }

        private string BuildListsQuery(string format, string action)
        {
            var id = Parameters.ListSlug.IsNullOrBlank()
                         ? Parameters.ListId.HasValue
                               ? Parameters.ListId.Value.ToString()
                               : null
                         : Parameters.ListSlug;

            var hasAction = !Parameters.Action.IsNullOrBlank();
            var urlBase = hasAction
                              ? UrlListsActionBase
                              : id != null ? UrlListsIdBase : UrlListsBase;

            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlBase = urlBase.Replace(UrlAuthority, authority);
            }

            var user = (Parameters.UserScreenName ?? "").UrlEncodeStrict();
            var url = hasAction
                          ? urlBase.FormatWith(user, id, action, format)
                          : id != null
                                ? urlBase.FormatWith(user, id, format)
                                : urlBase.FormatWith(user, format);

            if (!Parameters.Action.IsNullOrBlank() &&
                (Parameters.Action.Equals("members") || Parameters.Action.Equals("subscribers")))
            {
                url = url.Replace("lists/", "");
            }

            if (!Parameters.Action.IsNullOrBlank() &&
                (Parameters.Action.Equals("members_id") || Parameters.Action.Equals("subscribers_id")))
            {
                // http://api.twitter.com/1/user/list_id/members/id.format
                // http://api.twitter.com/1/user/list_id/subscribers/id.format
                url = url.Replace("lists/", "");
                if (Parameters.UserId != null)
                {
                    url = url.Replace("members_id", string.Format("members/{0}", Parameters.UserId.Value));
                    url = url.Replace("subscribers_id", string.Format("subscribers/{0}", Parameters.UserId.Value));
                }
            }

            url = url.Replace("//", "/").Replace("http:/", "http://").Replace("https:/", "https://");

            var resultUrl = BuildListsParameters(url);
            return resultUrl;
        }

        private string BuildListsParameters(string url)
        {
            var parameters = new List<string>(BuildListsParameters());

            return ConcatentateParameters(parameters, url);
        }

        private string BuildSearchQuery(string format)
        {
            // Swap the authority if a transparent proxy is used
            var urlSearchBase = UrlSearchBase;
            if (!Configuration.TransparentProxy.IsNullOrBlank())
            {
                var authority = Configuration.TransparentProxy;
                urlSearchBase = urlSearchBase.Replace(UrlSearchAuthority, authority);
            }

            var searchUrl = urlSearchBase.FormatWith(Parameters.Action, format);
            var searchBuilder = new StringBuilder(searchUrl);

            // [Issue 2] Distinguish between parameters and operators
            var searchOperators = new List<string>(BuildSearchOperators());
            for (var i = 0; i < searchOperators.Count(); i++)
            {
                searchBuilder.Append(i > 0 ? "+" : "?q=");
                searchBuilder.Append(searchOperators[i]);
            }

            var hasOperators = searchOperators.Count > 0;
            var searchParameters = new List<string>(BuildSearchParameters());
            for (var i = 0; i < searchParameters.Count(); i++)
            {
                searchBuilder.Append(i > 0 || hasOperators ? "&" : "?");
                searchBuilder.Append(searchParameters[i]);
            }

            var result = searchBuilder.ToString();
            return result;
        }

        private IEnumerable<string> BuildSearchOperators()
        {
            if (!SearchParameters.SearchPhrase.IsNullOrBlank())
            {
                yield return SearchParameters.SearchPhrase.UrlEncodeStrict();
            }

            if (!SearchParameters.SearchWithoutPhrase.IsNullOrBlank())
            {
                yield return "-{0}".FormatWith(SearchParameters.SearchWithoutPhrase).UrlEncodeStrict();
            }

            // operators below phrase

            if (SearchParameters.SearchSince.HasValue)
            {
                var date = SearchParameters.SearchSince.Value;
                yield return
                    string.Format("since:{0}-{1}-{2}", date.Year,
                                  date.Month.ToString("00"),
                                  date.Day.ToString("00")).UrlEncodeStrict();
            }

            if (SearchParameters.SearchSinceUntil.HasValue)
            {
                var date = SearchParameters.SearchSinceUntil.Value;
                yield return
                    string.Format("until:{0}-{1}-{2}",
                                  date.Year,
                                  date.Month.ToString("00"),
                                  date.Day.ToString("00")).UrlEncodeStrict();
            }

            if (!SearchParameters.SearchFromUser.IsNullOrBlank())
            {
                yield return "from:{0}".FormatWith(SearchParameters.SearchFromUser).UrlEncodeStrict();
            }

            if (!SearchParameters.SearchToUser.IsNullOrBlank())
            {
                yield return string.Format("to:{0}", SearchParameters.SearchToUser).UrlEncodeStrict();
            }

            if (!SearchParameters.SearchHashTag.IsNullOrBlank())
            {
                yield return string.Format("#{0}", SearchParameters.SearchHashTag.Replace("#", "")).UrlEncodeStrict();
            }

            if (!SearchParameters.SearchReferences.IsNullOrBlank())
            {
                yield return string.Format("@{0}", SearchParameters.SearchReferences).UrlEncodeStrict();
            }

            if (!SearchParameters.SearchNear.IsNullOrBlank())
            {
                yield return string.Format("near:{0}", SearchParameters.SearchNear).UrlEncodeStrict();
            }

            if (SearchParameters.SearchNegativity.HasValue &&
                SearchParameters.SearchNegativity.Value)
            {
                yield return ":(".UrlEncodeStrict();
            }

            if (SearchParameters.SearchPositivity.HasValue &&
                SearchParameters.SearchPositivity.Value)
            {
                yield return ":)".UrlEncodeStrict();
            }

            if (SearchParameters.SearchQuestion.HasValue &&
                SearchParameters.SearchQuestion.Value)
            {
                yield return "?".UrlEncodeStrict();
            }

            if (SearchParameters.SearchContainingLinks.HasValue &&
                SearchParameters.SearchContainingLinks.Value)
            {
                yield return "filter:links".UrlEncodeStrict();
            }
        }

        private IEnumerable<string> BuildSearchParameters()
        {
            if (Parameters.SinceId.HasValue)
            {
                yield return string.Format("since_id={0}", Parameters.SinceId.Value);
            }

            if (Parameters.MaxId.HasValue)
            {
                yield return string.Format("max_id={0}", Parameters.MaxId.Value);
                // Note: Although it isn't documented, it is set in the result's next_page
            }

            if (Parameters.ReturnPerPage.HasValue)
            {
                yield return "rpp={0}".FormatWith(Parameters.ReturnPerPage.Value);
            }

            if (Parameters.Page.HasValue)
            {
                yield return "page={0}".FormatWith(Parameters.Page.Value);
            }

            // root parameters above

            if (!SearchParameters.SearchLanguage.IsNullOrBlank())
            {
                yield return "lang={0}".FormatWith(SearchParameters.SearchLanguage.Substring(0, 2)).ToLower();
            }

            if (!SearchParameters.SearchLocale.IsNullOrBlank())
            {
                yield return "locale={0}".FormatWith(SearchParameters.SearchLocale.Substring(0, 2)).ToLower();
            }

            // within can be "within:" or "geocode?"
            if (SearchParameters.SearchMiles.HasValue && SearchParameters.SearchMiles.Value != 0)
            {
                if (SearchParameters.SearchGeoLatitude.HasValue && SearchParameters.SearchGeoLongitude.HasValue)
                {
                    var lat = SearchParameters.SearchGeoLatitude.Value;
                    var lon = SearchParameters.SearchGeoLongitude.Value;
                    var mi = SearchParameters.SearchMiles.Value;

                    // todo confirm precision of units
                    yield return "geocode={0}"
                        .FormatWithInvariantCulture("{0},{1},{2}mi".FormatWithInvariantCulture(lat, lon, mi)
                                                        .UrlEncodeStrict());
                }
                else
                {
                    var miles = Convert.ToInt32(SearchParameters.SearchMiles.Value);
                    yield return string.Format("within:{0}mi", miles).UrlEncodeStrict();

                    // [Issue 1] Can't use the "near:" + "within:" operator with arbitrary locations 
                    throw new NotSupportedException(
                        "You must specify a geo location with Of(latitude, longitude) when using Within(double miles)");
                }
            }

            if (SearchParameters.SearchShowUser.HasValue)
            {
                yield return "show_user={0}".FormatWith(SearchParameters.SearchShowUser.Value.ToString().ToLower());
            }

            if(SearchParameters.SearchResultType.HasValue)
            {
                yield return "result_type={0}".FormatWith(SearchParameters.SearchResultType.Value.ToLower());
            }

            // trend parameters below 

            if (SearchParameters.SearchExcludesHashtags.HasValue)
            {
                yield return "exclude=hashtags";
            }
            if (SearchParameters.SearchDate.HasValue)
            {
                yield return string.Format("date={0}", SearchParameters.SearchDate.Value.ToString("yyyy-MM-dd"));
            }
        }

        private IEnumerable<string> BuildPagingParameters()
        {
            if (Parameters.SinceId.HasValue)
            {
                yield return string.Format("since_id={0}", Parameters.SinceId.Value);
            }

            if (Parameters.MaxId.HasValue)
            {
                yield return string.Format("max_id={0}", Parameters.MaxId.Value);
            }

            if (Parameters.Count.HasValue)
            {
                // Newer API methods use per_page, not count, when they don't use cursors
                var token = Parameters.Activity.Equals("lists")
                            || (Parameters.Activity.Equals("users") &&
                                !Parameters.Action.IsNullOrBlank() &&
                                Parameters.Action.Equals("search"))
                                ? "per_page"
                                : "count";

                yield return string.Format("{0}={1}", token, Parameters.Count.Value);
            }

            if (Parameters.Page.HasValue)
            {
                yield return string.Format("page={0}", Parameters.Page.Value);
            }
        }

        private IEnumerable<string> BuildListsParameters()
        {
            foreach (var pagingParameter in BuildPagingParameters())
            {
                yield return pagingParameter;
            }

            if (Parameters.ListMemberId.HasValue)
            {
                yield return "id={0}".FormatWith(Parameters.ListMemberId.Value);
            }

            if (Parameters.Cursor.HasValue)
            {
                yield return "cursor={0}".FormatWith(Parameters.Cursor.Value);
            }

            if (!Parameters.ListName.IsNullOrBlank())
            {
                yield return "name={0}".FormatWith(Parameters.ListName.UrlEncodeStrict());
            }

            if (!Parameters.ListMode.IsNullOrBlank())
            {
                yield return "mode={0}".FormatWith(Parameters.ListMode.UrlEncodeStrict());
            }

            if (!Parameters.ListDescription.IsNullOrBlank())
            {
                yield return "description={0}".FormatWith(Parameters.ListDescription.UrlEncodeStrict());
            }
        }

       
        private void PostToExternalServices(bool async)
        {
            var actions = new List<Action>(); 
            if ((Parameters.CopyTo & ExternalService.Facebook) == ExternalService.Facebook)
            {
                var fb = ExternalServiceFactory.GetExternalService<IFacebook>(); 
                Action action = () => 
                fb.PostStatusToFacebook(Parameters.Text, (FacebookAuthentication) ExternalAuthentication[AuthenticationMode.Facebook]);
                actions.Add(action);
            }
            if ((Parameters.CopyTo & ExternalService.MySpace) == ExternalService.MySpace)
            {
                var fb = ExternalServiceFactory.GetExternalService<IMySpace>();
                Action action = () =>
                fb.PostStatusToMySpace(Parameters.Text, (MySpaceAuthentication)ExternalAuthentication[AuthenticationMode.MySpace]);
                actions.Add(action);
            }

            foreach (var action in actions)
            {
                if ( async )
                {
                    action.BeginInvoke(null, null); 
                }
                else
                {
                    action();
                }
            }
        }
    }
}