using System;

namespace TweetSharp.Rx
{
    internal class ActionDisposable : IDisposable
    {
        private readonly Action _action;

        public ActionDisposable(Action action)
        {
            _action = action;
        }

        void IDisposable.Dispose()
        {
            _action();
        }
    }
}