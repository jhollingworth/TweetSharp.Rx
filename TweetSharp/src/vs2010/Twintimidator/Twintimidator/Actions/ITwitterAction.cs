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
using TweetSharp.Twitter.Fluent;
using TweetSharp.Twitter.Model;
using Twintimidator.Accounts;

namespace Twintimidator.Actions
{
    public interface ITwitterAction
    {
        string Name { get; set; }
        IUserAccount User { get; set; }
        Func<ITwitterLeafNode> QueryMethod { get; set; }
        IFluentTwitter GetBaseQuery();
        ITwitterActionResult Execute();
        ITwitterActionResult ExecuteAs(IUserAccount user);
        void ExecuteAsync(Action<ITwitterActionResult> callback);
        void ExecuteAsyncAs(IUserAccount user, Action<ITwitterActionResult> callback);
    }

    public interface ITwitterAction<T> where T : class
    {
        Func<ITwitterActionResult<T>, bool> EvaluateConvertedReturnValueMethod { get; set; }
        Func<TwitterResult, T> ConvertReturnValueMethod { get; set; }
        ITwitterActionResult<T> Execute();
        ITwitterActionResult<T> ExecuteAs(IUserAccount user);
        void ExecuteAsync(Action<ITwitterActionResult<T>> callback);
        void ExecuteAsyncAs(IUserAccount user, Action<ITwitterActionResult<T>> callback);
    }
}