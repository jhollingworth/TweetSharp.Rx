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

using System.Linq;
using NUnit.Framework;
using Twintimidator.Accounts;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class AccountSerializerTests
    {
        [Test]
        public void Can_deserialize_from_disk()
        {
            var serializer = new AccountListSerializer {FileName = "testusers.cache"};
            var accounts = serializer.DeserializeFromDisk();
            Assert.IsNotNull(accounts);
            Assert.IsTrue(accounts.Count() > 0);
        }

        [Test]
        public void Can_serialize_to_disk()
        {
            var controller = new UserAccountController();
            controller.CreateBasicAuthUser("Foo", "Bar");
            controller.CreateBasicAuthUser("Ralphie", "oo");
            controller.CreateBasicAuthUser("Speaker", "system");

            var serializer = new AccountListSerializer {FileName = "testusers.cache"};
            var success = serializer.SerializeToDisk(controller);
            Assert.IsTrue(success);
        }

        [Test]
        public void Handles_no_file_condition()
        {
            var serializer = new AccountListSerializer {FileName = "invalidfile.cache"};
            var accounts = serializer.DeserializeFromDisk();
            Assert.IsNull(accounts);
        }
    }
}