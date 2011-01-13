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
using Moq;
using Ninject;
using NUnit.Framework;
using Twintimidator.Accounts;
using Twintimidator.Actions;
using Twintimidator.Dispatcher;
using Twintimidator.Results;
using Twintimidator.Threads;

namespace Twintimidator.UnitTests
{
    [TestFixture]
    public class DispatcherTests
    {
        private static TaskDispatcher GetRunningDispatcher()
        {
            var threadsController = Mocking.Kernel.Get<IThreadsController>();
            var acctsController = Mocking.Kernel.Get<IUserAccountController>();
            var actionsController = Mocking.Kernel.Get<IActionsController>();
            var resultsController = Mocking.Kernel.Get<IResultsController>();

            var mockActionsController = Mock.Get(actionsController);
            mockActionsController.Setup(controller => controller.AllAvailableActions)
                .Returns(new List<ITwitterAction> {Mocking.Kernel.Get<ITwitterAction>()});
            mockActionsController.Setup(controller => controller.SelectedActions)
                .Returns(new List<ITwitterAction> {Mocking.Kernel.Get<ITwitterAction>()});
            var mockAcctsController = Mock.Get(acctsController);
            mockAcctsController.Setup(controller => controller.GetEnumerator())
                .Returns(new List<IUserAccount> {Mocking.Kernel.Get<IUserAccount>()}.GetEnumerator());

            actionsController.AddAction(Mocking.Kernel.Get<ITwitterAction>());
            actionsController.AllAvailableActions.ForEach(actionsController.AddAction);
            var dispatcher = new TaskDispatcher(acctsController, actionsController, threadsController, resultsController);
            Assert.IsFalse(dispatcher.IsRunning);

            dispatcher.Start();
            return dispatcher;
        }

        [Test]
        public void Can_start()
        {
            var dispatcher = GetRunningDispatcher();
            Assert.IsNotNull(dispatcher);
            Assert.IsTrue(dispatcher.IsRunning);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Throws_if_accounts_controller_null()
        {
            var threadsController = Mocking.Kernel.Get<IThreadsController>();
            var actionsController = Mocking.Kernel.Get<IActionsController>();
            var resultsController = Mocking.Kernel.Get<IResultsController>();
            var dispatcher = new TaskDispatcher(null, actionsController, threadsController, resultsController);
            Assert.IsFalse(dispatcher.IsRunning);
            dispatcher.Start();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Throws_if_actions_controller_null()
        {
            var threadsController = Mocking.Kernel.Get<IThreadsController>();
            var acctsController = Mocking.Kernel.Get<IUserAccountController>();
            var resultsController = Mocking.Kernel.Get<IResultsController>();
            var dispatcher = new TaskDispatcher(acctsController, null, threadsController, resultsController);
            Assert.IsFalse(dispatcher.IsRunning);
            dispatcher.Start();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Throws_if_results_controller_null()
        {
            var threadsController = Mocking.Kernel.Get<IThreadsController>();
            var acctsController = Mocking.Kernel.Get<IUserAccountController>();
            var actionsController = Mocking.Kernel.Get<IActionsController>();
            var dispatcher = new TaskDispatcher(acctsController, actionsController, threadsController, null);
            Assert.IsFalse(dispatcher.IsRunning);
            dispatcher.Start();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void Throws_if_threads_controller_null()
        {
            var acctsController = Mocking.Kernel.Get<IUserAccountController>();
            var actionsController = Mocking.Kernel.Get<IActionsController>();
            var resultsController = Mocking.Kernel.Get<IResultsController>();
            var dispatcher = new TaskDispatcher(acctsController, actionsController, null, resultsController);
            Assert.IsFalse(dispatcher.IsRunning);
            dispatcher.Start();
        }
    }
}