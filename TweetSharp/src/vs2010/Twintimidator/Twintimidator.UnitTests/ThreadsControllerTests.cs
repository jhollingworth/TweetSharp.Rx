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
using NUnit.Framework;
using Twintimidator.Threads;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class ThreadsControllerTests
    {
        [Test]
        public void Can_instantiate_controller()
        {
            var controller = new ThreadsController();
            Assert.IsNotNull(controller);
        }

        [Test]
        public void Can_set_number_of_threads()
        {
            var controller = new ThreadsController {NumberOfThreadsToUse = 20};
            Assert.AreEqual(20, controller.NumberOfThreadsToUse);
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void Cant_set_number_of_threads_greater_than_max()
        {
            var controller = new ThreadsController();
            controller.NumberOfThreadsToUse = controller.MaximumThreads + 1;
        }

        [Test]
        [ExpectedException(typeof (ArgumentOutOfRangeException))]
        public void Cant_set_number_of_threads_less_than_min()
        {
            var controller = new ThreadsController();
            controller.NumberOfThreadsToUse = controller.MinimumThreads - 1;
        }
    }
}