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
using System.Runtime.Serialization;
using TweetSharp.Model;
using Newtonsoft.Json;

namespace TweetSharp.Twitter.Model
{
    /* {"relationship": {
            "source": {
                "id": 123,
                "screen_name": "bob",
                "following": true,
                "followed_by": false,
                "notifications_enabled": false
     *      }
     *      ,
            "target": {
                "id": 456,
                "screen_name": "jack",
                "following": false,
                "followed_by": true,
                "notifications_enabled": null
     *      }
     *   }
     * }
     */

#if !SILVERLIGHT
    /// <summary>
    /// A master node that wraps the contents of a <see cref="TwitterRelationship" /> result.
    /// </summary>
    [Serializable]
#endif

    [JsonObject(MemberSerialization.OptIn)]
    public class TwitterFriendship : PropertyChangedBase, ITwitterModel
    {
        private TwitterRelationship _relationship;

#if !Smartphone
        /// <summary>
        /// Gets or sets the relationship.
        /// </summary>
        /// <value>The relationship.</value>
        [DataMember]
#endif
        [JsonProperty("relationship")]
        public virtual TwitterRelationship Relationship
        {
            get { return _relationship; }
            set
            {
                if (_relationship == value)
                {
                    return;
                }

                _relationship = value;
                OnPropertyChanged("Relationship");
            }
        }

#if !Smartphone
        /// <summary>
        /// The source content used to deserialize the model entity instance.
        /// Can be XML or JSON, depending on the endpoint used.
        /// </summary>
        [DataMember]
#endif
        public virtual string RawSource { get; set; }
    }
}