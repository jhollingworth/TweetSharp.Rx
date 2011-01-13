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

namespace TweetSharp.Extensions
{
    /// <summary>
    /// Extension methods working with <see cref="TimeSpan"/> and <see cref="DateTime"/> values
    /// </summary>
    public static partial class TimeExtensions
    {

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of ticks
        /// </summary>
        /// <param name="value">the length of the desired <see cref="TimeSpan"/> in ticks</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Ticks(this long value)
        {
            return TimeSpan.FromTicks(value);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of years
        /// </summary>
        /// <param name="value">the length of the desired <see cref="TimeSpan"/> in years</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Years(this int value)
        {
            var now = DateTime.Now;
            var then = now.AddYears(value);
            return then - now; 
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of months
        /// </summary>
        /// <param name="value">the length of the desired <see cref="TimeSpan"/> in months</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Months(this int value)
        {
            var now = DateTime.Now;
            var then = now.AddMonths(value);
            return then - now; 
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of weeks
        /// </summary>
        /// <param name="value">the length of the desired <see cref="TimeSpan"/> in weeks</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Weeks(this int value)
        {
            return TimeSpan.FromDays(value*7);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of days
        /// </summary>
        /// <param name="days">the length of the desired <see cref="TimeSpan"/> in days</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Days(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of days
        /// </summary>
        /// <param name="days">the length of the desired <see cref="TimeSpan"/> in months</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Day(this int days)
        {
            return new TimeSpan(days, 0, 0, 0);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of hours
        /// </summary>
        /// <param name="hours">the length of the desired <see cref="TimeSpan"/> in hours</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Hours(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of hours
        /// </summary>
        /// <param name="hours">the length of the desired <see cref="TimeSpan"/> in hours</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Hour(this int hours)
        {
            return new TimeSpan(0, hours, 0, 0);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of minutes
        /// </summary>
        /// <param name="minutes">the length of the desired <see cref="TimeSpan"/> in minutes</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Minutes(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of minutes
        /// </summary>
        /// <param name="minutes">the length of the desired <see cref="TimeSpan"/> in minutes</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Minute(this int minutes)
        {
            return new TimeSpan(0, 0, minutes, 0);
        }

        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of seconds
        /// </summary>
        /// <param name="seconds">the length of the desired <see cref="TimeSpan"/> in seconds</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Second(this int seconds)
        {
            return new TimeSpan(0, 0, 0, seconds);
        }
        
        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of seconds
        /// </summary>
        /// <param name="seconds">the length of the desired <see cref="TimeSpan"/> in seconds</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Seconds(this int seconds)
        {
            return new TimeSpan(0, 0, 0, seconds);
        }
       
        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of milliseconds
        /// </summary>
        /// <param name="milliseconds">the length of the desired <see cref="TimeSpan"/> in milliseconds</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Millisecond(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }
       
        /// <summary>
        /// Converts the provided value unto a <see cref="TimeSpan"/> of the specified number of milliseconds
        /// </summary>
        /// <param name="milliseconds">the length of the desired <see cref="TimeSpan"/> in milliseconds</param>
        /// <returns>A <see cref="TimeSpan"/> of the specified length</returns>
        public static TimeSpan Milliseconds(this int milliseconds)
        {
            return new TimeSpan(0, 0, 0, 0, milliseconds);
        }

        /// <summary>
        /// Given a <see cref="TimeSpan"/>, returns a <see cref="DateTime"/> in the past 
        /// equivalent to the provided TimeSpan subtracted from the current time. (Eg. 3.Minutes().Ago())
        /// </summary>
        /// <param name="value">the timespan to subtract from the current time</param>
        /// <returns>
        /// the <see cref="DateTime"/> in the past. 
        /// </returns>
        public static DateTime Ago(this TimeSpan value)
        {
            return DateTime.UtcNow.Add(value.Negate());
        }

        /// <summary>
        /// Given a <see cref="TimeSpan"/>, returns a <see cref="DateTime"/> in the future
        /// equivalent to the provided TimeSpan added to the current time. (Eg. 3.Minutes().FromNow())
        /// </summary>
        /// <param name="value">the timespan to add to the current time</param>
        /// <returns>
        /// the <see cref="DateTime"/> in the future
        /// </returns>
        public static DateTime FromNow(this TimeSpan value)
        {
            return new DateTime((DateTime.Now + value).Ticks);
            //return new DateTime((DateTime.UtcNow + value).Ticks);
        }

        /// <summary>
        /// Converts a value representing Unix Time (elapsed seconds since 1/1/1970) to a <see cref="DateTime"/>
        /// </summary>
        /// <param name="seconds">The number of elapsed seconds since the Unix Epoch time (00:00:00) 1/1/1970)</param>
        /// <returns>The corresponding <see cref="DateTime"/></returns>
        public static DateTime FromUnixTime(this long seconds)
        {
            var time = new DateTime(1970, 1, 1);
            time = time.AddSeconds(seconds);

            return time.ToLocalTime();
        }

        /// <summary>
        /// Converts a <see cref="DateTime"/> to a value representing Unix Time (elapsed seconds since 1/1/1970) 
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to convert</param>
        /// <returns>The number of elapsed seconds since the Unix Epoch time (00:00:00) 1/1/1970)</returns>
        public static long ToUnixTime(this DateTime dateTime)
        {
            var timeSpan = (dateTime - new DateTime(1970, 1, 1));
            var timestamp = (long) timeSpan.TotalSeconds;

            return timestamp;
        }
    }
}