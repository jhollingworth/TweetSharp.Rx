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

using System.Collections.Generic;
using TweetSharp.Model;
using TweetSharp.Yammer.Model;

namespace TweetSharp.Yammer.Fluent
{
    /// <summary>
    /// Allowable sort orders for groups
    /// </summary>
    public enum SortGroupsBy
    {
        /// <summary>
        /// Sort by number of messages
        /// </summary>
        Messages,
        /// <summary>
        /// Sort by number of users
        /// </summary>
        Members,
        /// <summary>
        /// Sort by group privacy
        /// </summary>
        Privacy,
        /// <summary>
        /// Sort by the group's created date
        /// </summary>
        Created_At,
        /// <summary>
        /// Sort by the group creator's name
        /// </summary>
        Creator
    }

    /// <summary>
    /// Allowable sort orders for users
    /// </summary>
    public enum SortUsersBy
    {
        /// <summary>
        /// Sort by number of messages
        /// </summary>
        Messages,
        /// <summary>
        /// Sort by number of followers
        /// </summary>
        Followers
    }

    /// <summary>
    /// Parameters used in Yammer queries
    /// </summary>
    public interface IFluentYammerParameters
    {
        /// <summary>
        /// Gets or sets the activity
        /// </summary>
        /// <value>The activity</value>
        string Activity { get; set; }
        /// <summary>
        /// Gets or sets the action
        /// </summary>
        /// <value>The Action</value>
        string Action { get; set; }
        /// <summary>
        /// Gets or sets the value for the OlderThan query parameter
        /// </summary>
        /// <value>The OlderThan Id</value>
        long? OlderThan { get; set; }
        /// <summary>
        /// Gets or sets the value for the NewerThan query parameter
        /// </summary>
        /// <value>The NewerThan Id</value>
        long? NewerThan { get; set; }
        /// <summary>
        /// Gets or sets the threaded flag
        /// </summary>
        /// <value>The flag</value>
        bool Threaded { get; set; }
        /// <summary>
        /// Gets or sets the thread Id
        /// </summary>
        /// <value>The thread Id</value>
        long? ThreadID { get; set; }
        /// <summary>
        /// Gets or sets the count
        /// </summary>
        /// <value>The count</value>
        int? Count { get; set; }
        /// <summary>
        /// Gets or sets the page number
        /// </summary>
        /// <value>The page number</value>
        int? Page { get; set; }
        /// <summary>
        /// Gets or sets the number of results per page
        /// </summary>
        /// <value>The desired number of results</value>
        int? ReturnPerPage { get; set; }
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        /// <value>the Id</value>
        long? Id { get; set; }
        /// <summary>
        /// Gets or sets the screen name
        /// </summary>
        /// <value>The screen name</value>
        string ScreenName { get; set; }
        /// <summary>
        /// Gets or sets the message body
        /// </summary>
        /// <value>The message body</value>
        string Body { get; set; }
        /// <summary>
        /// Gets or sets the email
        /// </summary>
        /// <value>The email address</value>
        string Email { get; set; }
        /// <summary>
        /// Gets or sets the user screen name
        /// </summary>
        /// <value>The user screen name</value>
        string UserScreenName { get; set; }
        /// <summary>
        /// Gets or sets the user Id
        /// </summary>
        /// <value>The user Id</value>
        long? UserId { get; set; }
        /// <summary>
        /// Gets or sets a flag indicating that the current user's Id should be used
        /// </summary>
        /// <value>the flag</value>
        bool UseCurrentAsUserId { get; set; }
        /// <summary>
        /// Gets or sets the Bot Id
        /// </summary>
        /// <value>The bot Id</value>
        long? BotID { get; set; }
        /// <summary>
        /// Gets or sets the group Id
        /// </summary>
        /// <value>The group Id.</value>
        long? GroupID { get; set; }
        /// <summary>
        /// Gets or sets the follow flag
        /// </summary>
        /// <value>The flag</value>
        bool? Follow { get; set; }
        /// <summary>
        /// Gets or sets the tag
        /// </summary>
        /// <value>The tag</value>  
        string Tag { get; set; }
        /// <summary>
        /// Gets or sets the Id of the user to whom to send a direct message
        /// </summary>
        /// <value>The user Id</value>
        long? DirectToUser { get; set; }
        /// <summary>
        /// Gets or sets the Id of the group to which to post a message
        /// </summary>
        /// <value>The group Id</value>
        long? ToGroupID { get; set; }
        /// <summary>
        /// Gets or sets the Id of the message to which to reply
        /// </summary>
        /// <value>The message Id</value>
        long? InReplyTo { get; set; }
        /// <summary>
        /// Gets or sets the message Id
        /// </summary>
        /// <value>The message Id</value>
        long? MessageId { get; set; }
        /// <summary>
        /// Gets or sets the starting string
        /// </summary>
        /// <value>The starting string</value>
        char? StartingWith { get; set; }
        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        /// <value>The group name</value>
        string GroupName { get; set; }
        /// <summary>
        /// Gets or sets the group sort order
        /// </summary>
        /// <value>The sort order</value>
        SortGroupsBy? SortGroupsBy { get; set; }
        /// <summary>
        /// Gets or sets the user sort order
        /// </summary>
        /// <value>The sort order</value>
        SortUsersBy? SortUsersBy { get; set; }
        /// <summary>
        /// Gets or sets the reverse flag
        /// </summary>
        /// <value>The reverse flag value</value>
        bool? Reverse { get; set; }
        /// <summary>
        /// Gets or sets the private flag
        /// </summary>
        /// <value>The private flag</value>
        bool? Private { get; set; }
        /// <summary>
        /// Gets the list of attachments
        /// </summary>
        IList<string> Attachments { get; }
        /// <summary>
        /// Gets or sets the user data
        /// </summary>
        /// <value>The user data</value>
        YammerUser UserData { get; set; }
        /// <summary>
        /// Gets or sets the colleague's name
        /// </summary>
        /// <value>The colleague's name</value>
        string Colleague { get; set; }
        /// <summary>
        /// Gets or sets the superior's name
        /// </summary>
        /// <value>The superior's name</value>
        string Superior { get; set; }
        /// <summary>
        /// Gets or sets the subordinate's name
        /// </summary>
        /// <value>The subordinate's name</value>
        string Subordinate { get; set; }
        /// <summary>
        /// Gets or sets the target Id
        /// </summary>
        /// <value>The target Id</value>
        long? TargetId { get; set; }
        /// <summary>
        /// Gets or sets the TargetType
        /// </summary>
        /// <value>The target type</value>
        string TargetType { get; set; }
        /// <summary>
        /// Gets or sets the prefix
        /// </summary>
        /// <value>The prefix</value> 
        string Prefix { get; set; }
        /// <summary>
        /// Gets or sets the search query
        /// </summary>
        /// <value>The search query</value>
        string Search { get; set; }
        /// <summary>
        /// Gets or sets the relationship type
        /// </summary>
        /// <value>The relationship type</value>
        OrgChartRelationshipType? RelationshipType { get; set; }
    }
}