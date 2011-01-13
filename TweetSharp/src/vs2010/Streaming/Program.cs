using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hammock;
using Hammock.Authentication.OAuth;
using Hammock.Streaming;
using Hammock.Web;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using TweetSharp.Twitter.Service;

namespace Streaming
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Why hello there!");

            var service = new TwitterService();


            service.AuthenticateWith(
                "OZdYbHX78jRuUScFGg0Wg",
                "icdkaBn7IGV614FZIL4P82Qly6cwSYBcfC6sXEvj3Xk",
                "6525972-NZqSVjKjj5pXsI80wI9a0HH4IjKfxSysL2T3JcYikv",
                "b10jbAu0T1K6Ii7cwKl3ZPGSMzLKgw2cFHioEQovaA"
            );


            service.StreamResult += service_StreamResult;
            var stream = service
                .BeginStreamUser(new TimeSpan(1, 0, 0));


            var request = new RestRequest
                              {
                                  Credentials = new OAuthCredentials
                                                    {
                                                        ConsumerKey = "OZdYbHX78jRuUScFGg0Wg",
                                                        ConsumerSecret = "icdkaBn7IGV614FZIL4P82Qly6cwSYBcfC6sXEvj3Xk",
                                                        Token = "6525972-NZqSVjKjj5pXsI80wI9a0HH4IjKfxSysL2T3JcYikv",
                                                        TokenSecret = "b10jbAu0T1K6Ii7cwKl3ZPGSMzLKgw2cFHioEQovaA"
                                                    },
                                  ExpectContentType = "application/json",
                                  StreamOptions = new StreamOptions
                                                      {
                                                          Duration = new TimeSpan(1, 0, 0),
                                                          ResultsPerCallback = 100
                                                      },
                                  Method = WebMethod.Get
                              };


            var uri = new Uri("https://userstream.twitter.com/2/user.json");

            request.Path = string.Format("{0}://{1}{2}{3}{4}",
                                uri.Scheme,
                                uri.Host.ToLower(),
                                (uri.Scheme == "http" && uri.Port != 80 ||
                                 uri.Scheme == "https" && uri.Port != 443)
                                    ? ":" + uri.Port
                                    : "",
                                uri.AbsolutePath.ToLower(),
                                uri.Query); // Don't lowercase the query; otherwise this would be one ToLower() call

            var client = new RestClient();
            var wait = client.BeginRequest(request);


            WaitHandle.WaitAll(new[] { wait.AsyncWaitHandle });



            WaitHandle.WaitAll(new[] { stream.AsyncWaitHandle });

            Console.WriteLine("Goodbye!");
        }

        private static void service_StreamResult(object sender, TwitterStreamResultEventArgs e)
        {
            foreach (var status in e.Statuses)
            {
                Console.WriteLine(status.Text);
            }
        }
    }
}