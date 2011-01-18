// Type: System.ObservableExtensions
// Assembly: System.Reactive, Version=1.0.2838.104, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: C:\src\Personal\TweetSharp.Rx\TweetSharp.Rx\lib\System.Reactive.dll

namespace System
{
    public static class ObservableExtensions
    {
        public static IDisposable Subscribe<TSource>(this IObservable<TSource> source);
        public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext);

        public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext,
                                                     Action<Exception> onError);

        public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext,
                                                     Action onCompleted);

        public static IDisposable Subscribe<TSource>(this IObservable<TSource> source, Action<TSource> onNext,
                                                     Action<Exception> onError, Action onCompleted);
    }
}
