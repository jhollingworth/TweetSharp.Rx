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

#if(!SILVERLIGHT)
using System.Drawing.Imaging;
using System.IO;
using System.Xml.Linq;
using System;
using System.Text.RegularExpressions;
using Hammock;
using TweetSharp.Core.Extensions;
#endif

namespace TweetSharp.Twitter.Fluent.Services
{
#if !SILVERLIGHT
    [Serializable]
    internal static class SendPhotoService
    {
        public static string SendPhoto(SendPhotoServiceProvider provider, 
                                       ImageFormat format, 
                                       string path, 
                                       string username, 
                                       string password)
        {
            RestResponse response;
            switch (provider)
            {
                case SendPhotoServiceProvider.TwitPic:
                    // http://twitpic.com/api.do
                    response = SendPhoto(format, path, "http://twitpic.com/api", "upload", username, password);
                    break;
                case SendPhotoServiceProvider.YFrog:
                    // http://yfrog.com/upload_and_post.html
                    response = SendPhoto(format, path, "http://yfrog.com/api", "upload", username, password);
                    break;
                case SendPhotoServiceProvider.TwitGoo:
                    // http://twitgoo.com/upload_and_post.html
                    response = SendPhoto(format, path, "http://twitgoo.com/api", "upload", username, password);
                    break;
                default:
                    throw new NotSupportedException("Unknown photo service provider specified");
            }

            if (response.Content == null || !response.Content.Contains("mediaurl"))
            {
                return null;
            }

            var match = Regex.Match(response.Content, "<mediaurl[^>]*>(.*?)</mediaurl>");
            var mediaUrl = XElement.Parse(match.Value).Value;

            return mediaUrl;
        }

        public static RestResponse SendPhoto(ImageFormat format, 
                                       string path, 
                                       string url,
                                       string endpoint,
                                       string username, 
                                       string password)
        {
            var contentType = format.ToContentType();
            var fileName = Path.GetFileNameWithoutExtension(path);
            var client = new RestClient { Authority = url, Path = endpoint };
            client.AddFile("media", fileName, path, contentType);
            client.AddField("username", username);
            client.AddField("password", password);

            var response = client.Request();
            return response;
        }
    }
#endif
}