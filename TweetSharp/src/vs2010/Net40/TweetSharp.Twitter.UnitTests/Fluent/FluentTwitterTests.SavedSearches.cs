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
using System.Threading;
using NUnit.Framework;
using TweetSharp.Twitter.Extensions;
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;

namespace TweetSharp.Twitter.UnitTests.Fluent
{
    partial class FluentTwitterTests
    {
        private TwitterSavedSearch DeleteSearch(TwitterSavedSearch savedSearch)
        {
            var retries = 5;
            TwitterResult response;
            TwitterSavedSearch deletedSearch;
            do
            {
                Thread.Sleep(750);
                var delete = FluentTwitter.CreateRequest()
                    .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                    .SavedSearches().Delete(savedSearch.Id)
                    .AsJson();

                Console.WriteLine(delete.ToString());
                response = delete.Request();
                Assert.IsNotNull(response, "Delete saved search got null response");
                deletedSearch = response.AsSavedSearch();
                if (deletedSearch != null)
                {
                    retries = 0;
                }
            } while (--retries > 0);
            return deletedSearch;
        }

        [Test]
        [Category("Saved Searches")]
        public void Can_create_new_saved_searches_with_fluent_search()
        {
            var query = FluentTwitter.CreateRequest()
                .Search().Query()
                .Containing("#burgers");

            var search = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SavedSearches().Create(query)
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var savedSearch = response.AsSavedSearch();
            Assert.IsNotNull(savedSearch);

            DeleteSearch(savedSearch);
        }

        [Test]
        [Category("Saved Searches")]
        public void Can_create_new_saved_searches_with_simple_phrase()
        {
            var query = Guid.NewGuid();

            var search = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SavedSearches().Create(query.ToString())
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var savedSearch = response.AsSavedSearch();
            Assert.IsNotNull(savedSearch);

            DeleteSearch(savedSearch);
        }

        [Test]
        [Category("Saved Searches")]
        public void Can_delete_saved_search()
        {
            var query = Guid.NewGuid();

            var create = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SavedSearches().Create(query.ToString())
                .AsJson();

            Console.WriteLine(create.ToString());
            var response = create.Request();
            Assert.IsNotNull(response, "Create saved search got null response");

            var savedSearch = response.AsSavedSearch();
            Assert.IsNotNull(savedSearch, "Saved Search parsing returned null");


            //sometimes twitter needs a few seconds to fully absorb the saved search
            //retry a few times before failing the test. 

            var deletedSearch = DeleteSearch(savedSearch);
            Assert.IsNotNull(deletedSearch, "Delete saved search parsing returned null");
        }

        [Test]
        [Category("Saved Searches")]
        public void Can_list_saved_searches()
        {
            var search = FluentTwitter.CreateRequest()
                .AuthenticateWith(OAUTH_CONSUMER_KEY, OAUTH_CONSUMER_SECRET, TWITTER_OAUTH.Token, TWITTER_OAUTH.TokenSecret)
                .SavedSearches().List()
                .AsJson();

            Console.WriteLine(search.ToString());

            var response = search.Request();
            IgnoreFailWhales(response);

            var results = response.AsSavedSearches();

            foreach (var result in results)
            {
                Console.WriteLine(result.Name);
            }
        }
    }
}