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
using System.IO;
using System.Net;
using System.Web;

namespace Demo.Silverlight.Web.Modules
{
    /// <summary>
    /// An <see cref="IHttpModule" /> that can forward TweetSharp queries from
    /// Silverlight to Twitter and return the results.
    /// </summary>
    public class TweetSharpProxyModule : IHttpModule
    {
        // Request Headers
        private const string ProxyAcceptHeader = "X-Twitter-Accept";
        private const string ProxyAgentHeader = "X-Twitter-Agent";
        private const string ProxyAuthorizationHeader = "X-Twitter-Auth";
        private const string ProxyMethodHeader = "X-Twitter-Method";
        private const string ProxyQueryHeader = "X-Twitter-Query";

        // Response Headers
        private const string ProxyStatusCode = "X-Twitter-StatusCode";
        private const string ProxyStatusDescription = "X-Twitter-StatusDescription";

        private static readonly ICollection<string>
            _skipHeaders
                = new[]
                      {
                          "Connection",
                          "Keep-Alive",
                          "Accept",
                          "Host",
                          "User-Agent",
                          "Content-Length",
                          "Content-Type",
                          "Accept-Encoding",
                          "Authorization",
                          "Referer",
                          ProxyMethodHeader,
                          ProxyAuthorizationHeader,
                          ProxyAcceptHeader,
                          ProxyAgentHeader,
                          ProxyQueryHeader
                      };

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += ContextBeginRequest;
        }

        #endregion

        private static void ContextBeginRequest(object sender, EventArgs e)
        {
            var context = (HttpApplication) sender;
            var request = context.Request;

            var path = request.Path.ToLowerInvariant();
            if (!path.StartsWith("/proxy/"))
            {
                return;
            }

            var url = context.Request.Headers[ProxyQueryHeader];

            Trace.WriteLine(String.Format("Proxy request handled for {0} from {1}.",
                                          request.Path,
                                          request.UserHostAddress));

            string response;

            try
            {
                // Set stock properties for the proxied call
                var proxyRequest = (HttpWebRequest) WebRequest.Create(url);

                // Set the HTTP method, preferring a proxy header
                var methodOverride = request.Headers[ProxyMethodHeader];
                proxyRequest.Method = methodOverride != null
                                          ? request.Headers[ProxyMethodHeader]
                                          : request.HttpMethod;

                // Set the user agent, preferring a proxy header
                var userAgent = request.Headers[ProxyAgentHeader];
                proxyRequest.UserAgent = userAgent != null
                                             ? request.Headers[ProxyAgentHeader]
                                             : request.UserAgent;

                // Set compression support, preferring a proxy header
                // Use compression if indicated
                var acceptEncoding =
                    request.Headers[ProxyAcceptHeader] ??
                    request.Headers["Accept-Encoding"];

                if (acceptEncoding != null)
                {
                    acceptEncoding = acceptEncoding.ToLower();
                    if (acceptEncoding.Contains("gzip"))
                    {
                        proxyRequest.AutomaticDecompression =
                            DecompressionMethods.GZip;
                    }
                    else if (acceptEncoding.Contains("deflate"))
                    {
                        proxyRequest.AutomaticDecompression =
                            DecompressionMethods.Deflate;
                    }
                }

                proxyRequest.ContentType = request.ContentType;
                proxyRequest.ContentLength = request.ContentLength;

                // Set referer, preferring a proxy header
                proxyRequest.Referer = request.UrlReferrer != null
                                           ? request.UrlReferrer.ToString()
                                           : null;
                proxyRequest.KeepAlive = true;

                if (request.Headers[ProxyAuthorizationHeader] != null)
                {
                    proxyRequest.Headers["Authorization"] =
                        request.Headers[ProxyAuthorizationHeader];
                }

                foreach (var header in request.Headers.Keys)
                {
                    var name = header.ToString();
                    if (_skipHeaders.Contains(name))
                    {
                        continue;
                    }

                    var value = request.Headers[name];
                    proxyRequest.Headers[name] = value;
                }

                var stream = context.Request.InputStream;
                var content = new byte[context.Request.InputStream.Length];
                stream.Read(content, 0, (int) context.Request.InputStream.Length);

                response = ProxyRequest(proxyRequest, content);
            }
            catch (WebException ex)
            {
                response = HandleWebException(ex);
                if (ex.Response is HttpWebResponse)
                {
                    var http = (HttpWebResponse) ex.Response;

                    // [DC]: AddHeader required for IIS Classic Mode
                    context.Response.AddHeader(ProxyStatusCode, ((int) http.StatusCode).ToString());
                    context.Response.AddHeader(ProxyStatusDescription, http.StatusDescription);
                }
            }

            context.Response.ClearContent();
            context.Response.Write(response);
            context.Response.End(); // calls flush
        }

        private static string ProxyRequest(WebRequest proxyRequest, byte[] content)
        {
            switch (proxyRequest.Method)
            {
                case "GET":
                case "DELETE":
                    using (var response = proxyRequest.GetResponse())
                    {
                        try
                        {
                            var stream = response.GetResponseStream();
                            using (var reader = new StreamReader(stream))
                            {
                                var results = reader.ReadToEnd();

                                return results;
                            }
                        }
                        catch (WebException ex)
                        {
                            return HandleWebException(ex);
                        }
                    }
                case "POST":
                case "PUT":
                    try
                    {
                        using (var stream = proxyRequest.GetRequestStream())
                        {
                            stream.Write(content, 0, content.Length);
                            stream.Close();

                            using (var response = proxyRequest.GetResponse())
                            {
                                using (var reader = new StreamReader(response.GetResponseStream()))
                                {
                                    var result = reader.ReadToEnd();

                                    return result;
                                }
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        return HandleWebException(ex);
                    }
                default:
                    throw new NotSupportedException();
            }
        }

        private static string HandleWebException(WebException ex)
        {
            if (ex.Response is HttpWebResponse && ex.Response != null)
            {
                using (var reader = new StreamReader(ex.Response.GetResponseStream()))
                {
                    var result = reader.ReadToEnd();

                    return result;
                }
            }

            throw ex;
        }
    }
}