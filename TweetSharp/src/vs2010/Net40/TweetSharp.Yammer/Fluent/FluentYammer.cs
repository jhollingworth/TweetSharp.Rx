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
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using Hammock;
using Hammock.Authentication;
using Hammock.Web;
using TweetSharp.Core.Extensions;
using TweetSharp.Fluent;
using TweetSharp.Model;
using TweetSharp.Yammer.Model;

#if !SILVERLIGHT

#endif

namespace TweetSharp.Yammer.Fluent
{
#if !SILVERLIGHT
    /// <summary>
    /// This is the main fluent class for building expressions
    /// bound for the Yammer API.
    /// </summary>
    [Serializable]
#endif
    public sealed class FluentYammer : 
        FluentBase<YammerResult>, 
        IFluentYammer
    {
        private const string YammerClientDefaultName = "tweetsharp";
        private const string YammerClientDefaultUrl = "http://tweetsharp.com";
        private const string YammerClientDefaultVersion = "1.0.0.0";
        private const int YammerMaxUpdateLength = int.MaxValue;

        private const string UrlActionBase = "https://www.yammer.com/api/v1/{0}/{1}.{2}";
        private const string UrlActionIdBase = "https://www.yammer.com/api/v1/{0}/{1}/{2}.{3}";
        private const string UrlBase = "https://www.yammer.com/api/v1/{0}.{1}";
        private const string UrlIdBase = "https://www.yammer.com/api/v1/{0}/{1}.{2}";

        /// <summary>
        /// Base URL for OAuth operations
        /// </summary>
        protected override string UrlOAuthAuthority
        {
            get { return "https://www.yammer.com/oauth/{0}"; }
        }

        private FluentYammer(IClientInfo clientInfo)
        {
            var serializer = new TweetSharpSerializer<YammerModelSerializer>();
            Client.Serializer = serializer;
            Client.Deserializer = serializer;

#if !SILVERLIGHT && !Smartphone
            SetLibraryWebPermissions();
#endif
            ClientInfo = clientInfo;
            Authentication = new FluentYammerAuthentication(this);
            Configuration = new FluentYammerConfiguration(this);
            Parameters = new FluentYammerParameters();

            // http://groups.google.com/group/twitter-development-talk/browse_thread/thread/7c67ff1a2407dee7
            ServicePointManager.Expect100Continue = false;
        }

        /// <summary>
        /// Gets the authentication pair used to authenticate to yammer
        /// </summary>
        /// <value>The authentication pair, typically a username and password or a oauth token and tokensecret.</value>
        public override Pair<string, string> AuthenticationPair
        {
            get
            {
                if (Authentication == null)
                {
                    return null;
                }

                var authenticator = Authentication.Authenticator;
                if (authenticator == null)
                {
                    return null;
                }

                switch (Authentication.Mode)
                {
                    case AuthenticationMode.OAuth:
                        return new Pair<string, string>
                                   {
                                       First = ((FluentBaseOAuth) authenticator).Token,
                                       Second = ((FluentBaseOAuth) authenticator).TokenSecret
                                   };
                    default:
                        throw new NotSupportedException("Unsupported authentication mode");
                }
            }
        }

        /// <summary>
        /// The parameters for the instance
        /// </summary>
        public IFluentYammerParameters Parameters { get; private set; }

        /// <summary>
        /// Sets the client info.
        /// </summary>
        /// <param name="clientInfo">The client info.</param>
        public static void SetClientInfo(YammerClientInfo clientInfo)
        {
            _staticClientInfo = clientInfo;
        }

        /// <summary>
        /// Gets the internal callback.
        /// </summary>
        /// <value>The internal callback.</value>
        protected override Action<object, YammerResult> InternalCallback
        {
            get { return InternalCallbackImpl; }
        }

        IFluentYammerAuthentication IFluentYammer.Authentication
        {
            get { return (IFluentYammerAuthentication) Authentication; }
            set { Authentication = value; }
        }

        IFluentYammerConfiguration IFluentYammer.Configuration
        {
            get { return (IFluentYammerConfiguration) Configuration; }
        }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        /// <value>The callback.</value>
        public YammerWebCallback Callback { get; set; }

        private void InternalCallbackImpl(object sender, TweetSharpResult result)
        {
            if (Callback != null)
            {
                Callback(sender, result as YammerResult, null);
            }
        }

#if !SILVERLIGHT
        /// <summary>
        /// Makes a sequential call to the service to get the results of this query.
        /// </summary>
        /// <returns></returns>
        public override YammerResult Request()
        {
            var files = ValidateAttachments();
            var request = CreateRestRequest(files);
            var response = Client.Request(request);
            var result = BuildResult(response);
            // Default cache
            EnsureDefaultCache();
            

            return result;
        }

        private IEnumerable<string> ValidateAttachments()
        {
            foreach (var file in Parameters.Attachments)
            {
                if (File.Exists(file))
                {
                    yield return file;
                }
                else
                {
                    throw new TweetSharpException("Attachment file {0} was not found".FormatWith(file));
                }
            }
        }
#endif

        YammerClientInfo IFluentYammer.ClientInfo
        {
            get { return ClientInfo as YammerClientInfo; }
            set { ClientInfo = value; }
        }
        
        //[jd]Changed access to internal from protected as this is accessed from unit tests
        internal override void ValidateUpdateText()
        {
            if (Parameters == null || Parameters.Body == null)
            {
                // non-participant
                return;
            }

            if (Parameters.Body.Length <= YammerMaxUpdateLength)
            {
                // valid
                return;
            }
        }

        /// <summary>
        /// Creates a new composable query, using the default client and platform.
        /// </summary>
        public static IFluentYammer CreateRequest()
        {
            if (_staticClientInfo == null)
            {
                _staticClientInfo = new YammerClientInfo
                                  {
                                      ClientName = YammerClientDefaultName,
                                      ClientUrl = YammerClientDefaultUrl,
                                      ClientVersion = YammerClientDefaultVersion
                                  };
            }

            return new FluentYammer(_staticClientInfo);
        }

        /// <summary>
        /// Builds the result
        /// </summary>
        /// <param name="response">The result from yammer</param>
        /// <returns>The result</returns>
        protected override YammerResult BuildResult(RestResponseBase response)
        {
            if (response == null)
            {
                throw new ArgumentNullException("response", "RestResponse was null");
            }

            var result = new YammerResult
            {
                RequestDate = response.RequestDate,
                ResponseDate = response.ResponseDate,
                Response = response.Content,
                ResponseType = response.ContentType,
                ResponseLength = response.ContentLength,
                RequestUri = response.RequestUri,
                ResponseUri = response.ResponseUri,
                ResponseHttpStatusCode = (int)response.StatusCode,
                ResponseHttpStatusDescription = response.StatusDescription,
                RequestHttpMethod = response.RequestMethod,
                Exception = response.InnerException,
                WebResponse = response.InnerResponse
            };

            return result;
        }

#if !SILVERLIGHT && !Smartphone
        private static void SetLibraryWebPermissions()
        {
            var permissions = new WebPermission();
            var baseUrl = new Regex(@"http://yammer\.com/.*");
            var apiUrl = new Regex(@"http://api.yammer\.com/.*");

            permissions.AddPermission(NetworkAccess.Connect, baseUrl);
            permissions.AddPermission(NetworkAccess.Connect, apiUrl);
            try
            {
                permissions.Demand();
            }
            catch (SecurityException)
            {
                var message =
                    "You cannot use TweetSharp in partial trust without a policy that allows connecting to API endpoints." +
                    Environment.NewLine +
                    "The following policy information (or equivalent) must be added to your trust policy:" +
                    Environment.NewLine +
                    permissions.ToXml();

                throw new SecurityException(message);
            }
        }
#endif

        /// <summary>
        /// Converts this query node into an API URL representation, ignoring any transparent proxy settings
        /// </summary>
        /// <returns></returns>
        public override string AsUrl(bool ignoreTransparentProxy)
        {
            var hasAuthAction = IsOAuthProcessCall;
            var hasAction = !Parameters.Action.IsNullOrBlank();
            var format = Format.ToLower();
            var activity = Parameters.Activity.IsNullOrBlank() ? "?" : Parameters.Activity;
            var action = hasAction ? Parameters.Action : "?";

            // this is an oauth call
            if (hasAuthAction)
            {
                return BuildOAuthQuery();
            }

            // this is a rest api call
            return BuildQuery(hasAction, format, activity, action);
        }

        /// <summary>
        /// Converts this query node into an API URL representation.
        /// </summary>
        /// <returns></returns>
        public override string AsUrl()
        {
            return AsUrl(false /* ignoreTransparentProxy */);
        }

        /// <summary>
        /// Completes the asynchronous request.
        /// </summary>
        /// <param name="asyncResult">The <see cref="IAsyncResult"/>handle returned from <see cref="FluentBase{TResult}.BeginRequest()"/></param>
        /// <returns></returns>
        public override YammerResult EndRequest(IAsyncResult asyncResult)
        {
            var response = Client.EndRequest(asyncResult);
            var result = BuildResult(response);
            return result;
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
            var id = Parameters.Id.HasValue
                         ? Parameters.Id.Value.ToString()
                         : String.Empty;


            var hasId = !id.IsNullOrBlank() || Parameters.UseCurrentAsUserId;
            object idOrCurrent = Parameters.UseCurrentAsUserId ? "current" : id;
            string url;
            if (hasAction)
            {
                url = hasId ? UrlActionIdBase : UrlActionBase;
                url = string.Format(url, hasId
                                             ? new[] {activity, action, idOrCurrent, format}
                                             : new object[] {activity, action, format});
            }
            else
            {
                url = hasId ? UrlIdBase : UrlBase;
            }

            url = String.Format(url, hasId
                                         ?
                                             new[] {activity, idOrCurrent, format}
                                         : new object[] {activity, format});

            var sb = new StringBuilder(url);

            var parameters = new List<string>(BuildParameters());
            for (var i = 0; i < parameters.Count(); i++)
            {
                sb.Append(i > 0 ? "&" : "?");
                sb.Append(parameters[i]);
            }

            var resultUrl = sb.ToString();
            //hack
            var split = resultUrl.Split('?');
            resultUrl = split[0].Replace(".none", "") + '?';
            for (var j = 1; j < split.Length; j++)
            {
                resultUrl += split[j];
            }


            return resultUrl;
        }

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
            var files = ValidateAttachments();
            var request = CreateRestRequest(files);
            RestCallback callback = null;
            if (Callback != null)
            {
                callback = new RestCallback(
                    (req, resp, state) =>
                    {
                        var yammerResult = BuildResult(resp);
                        Callback(this, yammerResult, state);
                    }
                    );
            }
            var result = callback == null
                             ? Client.BeginRequest(request, userState)
                             : Client.BeginRequest(request, callback, userState);

            return result;
        }

        private RestRequest CreateRestRequest(IEnumerable<string> files)
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
            
            files.ForEach(f => request.AddFile(Path.GetFileNameWithoutExtension(f), Path.GetFileName(f), f));
            
            SetRequestMeta(request);

            return request; 
        }

        private IWebCredentials CreateRestCredentials()
        {
            switch (Authentication.Mode)
            {
                case AuthenticationMode.OAuth:
                    return GetCredentialsFromOAuthAuthenticator();
                default:
                    throw new NotSupportedException("Only OAuth is supported for Yammer authentication");
            }
        }

        private IEnumerable<string> BuildParameters()
        {
            if (!Parameters.Body.IsNullOrBlank())
            {
                const string format = "body={0}";

                var content = Parameters.Body.UrlEncodeRelaxed();
                yield return string.Format(format, content);
            }

            if (Parameters.InReplyTo.HasValue)
            {
                yield return string.Format("replied_to_id={0}", Parameters.InReplyTo.Value);
            }

            if (Parameters.ToGroupID.HasValue)
            {
                yield return string.Format("group_id={0}", Parameters.ToGroupID.Value);
            }

            if (Parameters.GroupID.HasValue)
            {
                yield return string.Format("group_id={0}", Parameters.GroupID.Value);
            }

            if (Parameters.DirectToUser.HasValue)
            {
                yield return string.Format("direct_to_id={0}", Parameters.DirectToUser.Value);
            }

            if (Parameters.MessageId.HasValue)
            {
                yield return string.Format("message_id={0}", Parameters.MessageId.Value);
            }

            if (Parameters.SortGroupsBy.HasValue)
            {
                yield return string.Format("sort_by={0}", Parameters.SortGroupsBy.Value.ToLower());
            }

            if (Parameters.SortUsersBy.HasValue)
            {
                yield return string.Format("sort_by={0}", Parameters.SortUsersBy.Value.ToLower());
            }

            if (Parameters.StartingWith.HasValue)
            {
                yield return string.Format("letter={0}", Parameters.StartingWith.Value);
            }

            if (Parameters.Page.HasValue)
            {
                yield return string.Format("page={0}", Parameters.Page.Value);
            }

            if (!string.IsNullOrEmpty(Parameters.Email))
            {
                yield return string.Format("email={0}", Parameters.Email);
            }

            if (Parameters.Reverse.HasValue && Parameters.Reverse.Value)
            {
                yield return string.Format("reverse=true");
            }
            if (!string.IsNullOrEmpty(Parameters.GroupName))
            {
                yield return string.Format("name={0}", Parameters.GroupName);
            }
            if (Parameters.Private.HasValue)
            {
                yield return string.Format("private={0}", Parameters.Private.Value);
            }
            if (!string.IsNullOrEmpty(Parameters.Subordinate))
            {
                yield return string.Format("subordinate={0}", Parameters.Subordinate);
            }
            if (!string.IsNullOrEmpty(Parameters.Superior))
            {
                yield return string.Format("superior={0}", Parameters.Superior);
            }
            if (!string.IsNullOrEmpty(Parameters.Colleague))
            {
                yield return string.Format("colleague={0}", Parameters.Colleague);
            }
            if (Parameters.RelationshipType.HasValue)
            {
                yield return string.Format("type={0}", Parameters.RelationshipType.Value);
            }

            if (Parameters.TargetId.HasValue)
            {
                yield return string.Format("target_id={0}", Parameters.TargetId.Value);
            }
            if (!string.IsNullOrEmpty(Parameters.TargetType))
            {
                yield return string.Format("target_type={0}", Parameters.TargetType);
            }
            if (!string.IsNullOrEmpty(Parameters.Prefix))
            {
                yield return string.Format("prefix={0}", Parameters.Prefix);
            }
            if (!string.IsNullOrEmpty(Parameters.Search))
            {
                yield return string.Format("search={0}", Parameters.Search);
            }
            if (Parameters.UserData != null)
            {
                var userParams = ParamaterizeUserData(Parameters.UserData);
                foreach (var s in userParams)
                {
                    yield return s;
                }
            }
        }

        private static IEnumerable<string> ParamaterizeUserData(YammerUser user)
        {
            if (user.ContactInfo != null && user.ContactInfo.EmailAddresses != null)
            {
                var primaryAddresses =
                    user.ContactInfo.EmailAddresses.Where(a => a.EmailType.ToLower() == "primary");
                var address = primaryAddresses.Any()
                                  ? primaryAddresses.First()
                                  : user.ContactInfo.EmailAddresses.FirstOrDefault();
                if (address != null)
                {
                    yield return string.Format("email={0}", address.Address);
                }
            }
            if (!string.IsNullOrEmpty(user.FullName))
            {
                yield return string.Format("full_name={0}", user.FullName);
            }
            if (!string.IsNullOrEmpty(user.JobTitle))
            {
                yield return string.Format("job_title={0}", user.JobTitle);
            }
            if (!string.IsNullOrEmpty(user.Location))
            {
                yield return string.Format("location={0}", user.Location);
            }
            if (!string.IsNullOrEmpty(user.Interests))
            {
                yield return string.Format("interests={0}", user.Interests);
            }
            if (!string.IsNullOrEmpty(user.Summary))
            {
                yield return string.Format("summary={0}", user.Summary);
            }
            if (!string.IsNullOrEmpty(user.Expertise))
            {
                yield return string.Format("expertise={0}", user.Expertise);
            }
            if (user.ContactInfo != null)
            {
                if (user.ContactInfo.Im != null)
                {
                    if (!string.IsNullOrEmpty(user.ContactInfo.Im.Provider))
                    {
                        yield return string.Format("im_provider={0}", user.ContactInfo.Im.Provider);
                    }
                    if (!string.IsNullOrEmpty(user.ContactInfo.Im.UserName))
                    {
                        yield return string.Format("im_username={0}", user.ContactInfo.Im.UserName);
                    }
                }
                if (user.ContactInfo.PhoneNumbers != null && user.ContactInfo.PhoneNumbers.Any())
                {
                    var workNumber =
                        user.ContactInfo.PhoneNumbers.Where(n => n.NumberType.ToLower() == "work").FirstOrDefault();
                    if (workNumber != null)
                    {
                        yield return string.Format("work_telephone={0}", workNumber.Number);
                    }
                    var mobileNumber =
                        user.ContactInfo.PhoneNumbers.Where(n => n.NumberType.ToLower() == "mobile").FirstOrDefault();
                    if (mobileNumber != null)
                    {
                        yield return string.Format("mobile_telephone={0}", mobileNumber.Number);
                    }
                }
                if (!string.IsNullOrEmpty(user.Interests))
                {
                    yield return string.Format("interests={0}", user.Interests);
                }
                if (!string.IsNullOrEmpty(user.Summary))
                {
                    yield return string.Format("summary={0}", user.Summary);
                }
                if (!string.IsNullOrEmpty(user.Expertise))
                {
                    yield return string.Format("expertise={0}", user.Expertise);
                }

                if (!string.IsNullOrEmpty(user.KidsNames))
                {
                    yield return string.Format("kids_names={0}", user.KidsNames);
                }

                if (!string.IsNullOrEmpty(user.SignificantOther))
                {
                    yield return string.Format("significant_other={0}", user.SignificantOther);
                }
                foreach (var school in user.Schools)
                {
                    yield return
                        string.Format("education[]={0},{1},{2},{3},{4}", school.School, school.Degree,
                                      school.Description, school.StartYear, school.EndYear);
                }
                foreach (var company in user.PreviousCompanies)
                {
                    yield return
                        string.Format("previous_companies[]={0},{1},{2},{3},{4}", company.Employer, company.Position,
                                      company.Description, company.StartYear, company.EndYear);
                }
            }
        }
    }
}