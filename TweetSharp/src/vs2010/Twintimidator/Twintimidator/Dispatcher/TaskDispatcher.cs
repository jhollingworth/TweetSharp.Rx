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
using System.Linq;
using System.Threading;
using Twintimidator.Accounts;
using Twintimidator.Actions;
using Twintimidator.Results;
using Twintimidator.Threads;

namespace Twintimidator.Dispatcher
{
    public class TaskDispatcher
    {
        private readonly IUserAccountController _accountController;
        private readonly IActionsController _actionsController;
        private readonly IResultsController _resultsController;
        private readonly IThreadsController _threadsController;

        public TaskDispatcher(IUserAccountController accountController, IActionsController actionsController,
                              IThreadsController threadsController, IResultsController resultsController)
        {
            if (accountController == null)
            {
                throw new ArgumentNullException("accountController");
            }
            if (actionsController == null)
            {
                throw new ArgumentNullException("actionsController");
            }
            if (threadsController == null)
            {
                throw new ArgumentNullException("threadsController");
            }
            if (resultsController == null)
            {
                throw new ArgumentNullException("resultsController");
            }

            if (actionsController.SelectedActions.Count() == 0)
            {
                throw new InvalidOperationException("Must specify at least one action");
            }

            if (accountController.Count() == 0)
            {
                throw new InvalidOperationException("Must specify at least one account to use");
            }


            _threadsController = threadsController;
            _accountController = accountController;
            _actionsController = actionsController;
            _resultsController = resultsController;
        }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            IsRunning = true;
            DispatchThreads();
        }

        private void DispatchThreads()
        {
            var accountsEnumerator = _accountController.GetEnumerator();
            var actionsEnumerator = _actionsController.SelectedActions.GetEnumerator();
            if (_threadsController.ThreadType == ThreadType.RequestAsync)
            {
                DispatchAsyncTasks(accountsEnumerator, actionsEnumerator);
            }
            else
            {
                DispatchThreadedTasks(accountsEnumerator, actionsEnumerator);
            }
        }

        private void DispatchThreadedTasks(IEnumerator<IUserAccount> accountsEnumerator,
                                           IEnumerator<ITwitterAction> actionsEnumerator)
        {
            for (var i = 0; i < _threadsController.NumberOfThreadsToUse; i++)
            {
                if (!accountsEnumerator.MoveNext())
                {
                    accountsEnumerator = _accountController.GetEnumerator();
                    accountsEnumerator.MoveNext();
                }
                if (!actionsEnumerator.MoveNext())
                {
                    actionsEnumerator = _actionsController.SelectedActions.GetEnumerator();
                    actionsEnumerator.MoveNext();
                }
                var action = actionsEnumerator.Current;
                var account = accountsEnumerator.Current;
                var actionDelegate = BuildDelegate(action, account);
                if (_threadsController.ThreadType == ThreadType.ThreadPool)
                {
                    ThreadPool.QueueUserWorkItem(callback => actionDelegate());
                }
                else
                {
                    var t = new Thread(() => actionDelegate()) {IsBackground = true};
                    t.Start();
                }
            }
        }

        private void DispatchAsyncTasks(IEnumerator<IUserAccount> accountsEnumerator,
                                        IEnumerator<ITwitterAction> actionsEnumerator)
        {
            for (var i = 0; i < _threadsController.RepeatCount; i++)
            {
                actionsEnumerator.Reset();
                while (actionsEnumerator.MoveNext())
                {
                    if (!accountsEnumerator.MoveNext())
                    {
                        accountsEnumerator = _accountController.GetEnumerator();
                        accountsEnumerator.MoveNext();
                    }
                    var action = actionsEnumerator.Current;
                    var account = accountsEnumerator.Current;
                    action.ExecuteAsyncAs(account, result =>
                                                       {
                                                           if (result.Success)
                                                           {
                                                               _resultsController.IncrementSuccessCount();
                                                           }
                                                           else
                                                           {
                                                               _resultsController.IncrementFailureCount();
                                                               _resultsController.AddError(action.Name,
                                                                                           result.ErrorMessage);
                                                           }
                                                       });
                }
            }
        }

        private Action BuildDelegate(ITwitterAction action, IUserAccount account)
        {
            Action ret = () =>
                             {
                                 var result = action.ExecuteAs(account);
                                 if (result.Success)
                                 {
                                     _resultsController.IncrementSuccessCount();
                                 }
                                 else
                                 {
                                     _resultsController.IncrementFailureCount();
                                     _resultsController.AddError(action.Name, result.ErrorMessage);
                                 }
                             };
            return ret;
        }
    }
}