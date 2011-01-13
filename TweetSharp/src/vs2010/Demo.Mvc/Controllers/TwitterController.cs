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

using System.Web.Mvc;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharpMvc.Models;
using TweetSharpMvc.Security;

namespace TweetSharpMvc.Controllers
{
    public class TwitterController : Controller
    {
        [AuthorizeAgainstUserManager]
        public ActionResult UpdateStatus(string statusUpdate)
        {
            if (statusUpdate.Length < 3)
                return Json(new AjaxResponse(AjaxResponse.Code.Error, "Status text too short", new object()));

            var twitter = FluentTwitter.CreateRequest()
                .AuthenticateAs(UserManager.Username, UserManager.Password)
                .Statuses()
                .Update(statusUpdate)
                .AsJson();

            var response = twitter.Request();

            if (response == null)
                return
                    Json(new AjaxResponse(AjaxResponse.Code.Error,
                                          "The Twitter service failed to respond properly, please check your login information",
                                          new object()));

            var status = response.AsStatus();
            return Json(new AjaxResponse(AjaxResponse.Code.OK, "OK", status));
        }

        [AuthorizeAgainstUserManager]
        public ActionResult GetTimeline()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .AuthenticateAs(UserManager.Username, UserManager.Password)
                .Statuses().OnFriendsTimeline()
                .Take(25)
                .AsJson();

            var response = twitter.Request().AsStatuses();

            if (response == null)
                return
                    Json(new AjaxResponse(AjaxResponse.Code.Error,
                                          "The Twitter service failed to respond properly, please check your login information",
                                          new object()));

            return Json(new AjaxResponse(AjaxResponse.Code.OK, "OK", response));
        }

        [AuthorizeAgainstUserManager]
        public ActionResult GetTimelineForUser(string forUser)
        {
            if (string.IsNullOrEmpty(forUser)) forUser = UserManager.Username;

            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .AuthenticateAs(UserManager.Username, UserManager.Password)
                .Statuses()
                .OnUserTimeline()
                .For(forUser)
                .AsJson();

            var response = twitter.Request().AsStatuses();

            if (response == null)
                return
                    Json(new AjaxResponse(AjaxResponse.Code.Error,
                                          "The Twitter service failed to respond properly, please check your login information",
                                          new object()));

            return Json(new AjaxResponse(AjaxResponse.Code.OK, "OK", response));
        }

        [AuthorizeAgainstUserManager]
        public ActionResult GetMentions()
        {
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .AuthenticateAs(UserManager.Username, UserManager.Password)
                .Statuses()
                .Mentions()
                .Take(25)
                .AsJson();

            var response = twitter.Request().AsStatuses();

            if (response == null)
                return
                    Json(new AjaxResponse(AjaxResponse.Code.Error,
                                          "The Twitter service failed to respond properly, please check your login information",
                                          new object()));

            return Json(new AjaxResponse(AjaxResponse.Code.OK, "OK", response));
        }

        [AuthorizeAgainstUserManager]
        public ActionResult GetFollowers()
        {
            //TODO only returns 100 ... why?
            var twitter = FluentTwitter.CreateRequest()
                .Configuration.UseGzipCompression()
                .AuthenticateAs(UserManager.Username, UserManager.Password)
                .Users()
                .GetFollowers()
                .AsJson();

            var response = twitter.Request().AsUsers();

            if (response == null)
                return
                    Json(new AjaxResponse(AjaxResponse.Code.Error,
                                          "The Twitter service failed to respond properly, please check your login information",
                                          new object()));

            return Json(new AjaxResponse(AjaxResponse.Code.OK, "OK", response));
        }
    }
}