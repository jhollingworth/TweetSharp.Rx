﻿#region License

// Dimebrain TweetSharp
// (www.dimebrain.com)
// 
// The MIT License
// 
// Copyright (c) 2010 Daniel Crenna & Jason Diller
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 

#endregion
using System;
using System.Diagnostics;

#if SILVERLIGHT
using Hammock.Silverlight.Compat;
#endif

namespace TweetSharp
{
    internal static class ExternalServiceFactory
    {
        public static T GetExternalService<T>() where T:IExternalService
        {
            if ( typeof(T) == typeof(IFacebook))
            {
                return (T)GetFacebookImpl(); 
            }
            if (typeof(T) == typeof(IMySpace))
            {
                return (T)GetMySpaceImpl();
            }
            throw new InvalidOperationException("Unknown type");
        }

        private static IFacebook GetFacebookImpl()
        {
            var t = Type.GetType("TweetSharp.Twitter.Extras.Core.Facebook.FacebookConnector,TweetSharp.Twitter.Extras");
            if ( t != null )
            {
                return (IFacebook)Activator.CreateInstance(t);
            }
            //fallback to stub version
            Trace.WriteLine("Warning: Returning stub implementation of the IFacebook interface");
            return new FacebookStub(); 
        }

        private static IMySpace GetMySpaceImpl()
        {
            var t = Type.GetType("TweetSharp.Twitter.Extras.Core.MySpace.MySpaceConnector,TweetSharp.Twitter.Extras");
            if (t != null)
            {
                return (IMySpace)Activator.CreateInstance(t);
            }
            //fallback to stub version
            Trace.WriteLine("Warning: Returning stub implementation of the IMySpace interface");
            return new MySpaceStub();
        }
    }
}