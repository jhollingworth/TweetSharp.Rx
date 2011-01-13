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
using System.Threading;
using NUnit.Framework;
using Twintimidator.Results;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class ResultsControllerTests
    {
        [Test]
        public void Can_add_error_messages()
        {
            var resultsController = new ResultsController();
            resultsController.AddError("test", "failed like 90");
            resultsController.AddError("test", "ate too much");
            resultsController.AddError("test", "my tummy hurts");
            Assert.IsTrue(resultsController.Errors.Count() == 3);
        }

        [Test]
        public void Can_assign_expected_results_count()
        {
            var resultsController = new ResultsController {ExpectedNumberOfResults = 50};
            Assert.AreEqual(50, resultsController.ExpectedNumberOfResults);
        }

        [Test]
        public void Can_create()
        {
            var resultsController = new ResultsController();
            Assert.IsNotNull(resultsController);
        }

        [Test]
        public void Can_increment_both_count_safely()
        {
            var resultsController = new ResultsController();
            const int THREADS = 50;
            var sem = new Semaphore(0, THREADS);
            var block = THREADS;
            for (var i = 0; i < THREADS; i++)
            {
                var t = new Thread(failure =>
                                       {
                                           var fail = (bool) failure;
                                           sem.WaitOne(Timeout.Infinite);
                                           if (fail)
                                           {
                                               resultsController.IncrementSuccessCount();
                                           }
                                           else
                                           {
                                               resultsController.IncrementFailureCount();
                                           }
                                           block--;
                                       });
                t.Start((i%2 == 0));
            }
            sem.Release(THREADS);
            while (block > 0)
            {
            }

            Assert.AreEqual(THREADS/2, resultsController.Successes);
            Assert.AreEqual(THREADS/2, resultsController.Failures);
            Assert.AreEqual(THREADS, resultsController.NumberOfResultsRecieved);
        }

        [Test]
        public void Can_increment_fail_count_safely()
        {
            var resultsController = new ResultsController();
            const int THREADS = 10;
            var sem = new Semaphore(0, THREADS);
            var block = THREADS;
            for (var i = 0; i < THREADS; i++)
            {
                var t = new Thread(() =>
                                       {
                                           sem.WaitOne(Timeout.Infinite);
                                           resultsController.IncrementFailureCount();
                                           block--;
                                       });
                t.Start();
            }
            sem.Release(THREADS);
            while (block > 0)
            {
            }

            Assert.AreEqual(THREADS, resultsController.Failures);
            Assert.AreEqual(THREADS, resultsController.NumberOfResultsRecieved);
        }

        [Test]
        public void Can_increment_success_count_safely()
        {
            var resultsController = new ResultsController();
            const int THREADS = 10;
            var sem = new Semaphore(0, THREADS);
            var block = THREADS;
            for (var i = 0; i < THREADS; i++)
            {
                var t = new Thread(() =>
                                       {
                                           sem.WaitOne(Timeout.Infinite);
                                           resultsController.IncrementSuccessCount();
                                           block--;
                                       });
                t.Start();
            }
            sem.Release(THREADS);
            while (block > 0)
            {
            }

            Assert.AreEqual(THREADS, resultsController.Successes);
            Assert.AreEqual(THREADS, resultsController.NumberOfResultsRecieved);
        }
    }
}