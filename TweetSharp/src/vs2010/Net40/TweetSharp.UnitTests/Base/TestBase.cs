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
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;

namespace TweetSharp.UnitTests.Base
{
    public abstract class TestBase
    {
        public XDocument Document { get; protected set; }

        [SetUp]
        [Test]
        public void Can_initialize_setup_authorization()
        {
            if (!File.Exists("setup.xml"))
            {
                throw new FileNotFoundException(
                    "You must provide an setup.xml file to run these unit tests");
            }

            LoadSetupFile();
        }

        protected virtual void LoadSetupFile()
        {
            Document = XDocument.Load("setup.xml");
            var setup = Document.Element("setup");
            var valid = true;

            if (setup == null)
            {
                valid = false;
            }

            if (!valid)
            {
                throw new Exception("Authorization could not be parsed from setup.xml");
            }
        }
    }
}